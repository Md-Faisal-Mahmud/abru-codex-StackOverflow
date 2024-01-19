using Autofac;
using StackOverflow.Application.Services;
using StackOverflow.Infrastructure.Entity;
using StackOverflow.Infrastructure.Enum;

namespace StackOverflow.Web.Models.VoteModel
{
    public class VoteModel
    {
        private IAnswerService _answerService;
        private IPostService _postService;
        private IVoteService _voteService;
        private IUserService _userService;
        public VoteModel()
        {

        }
        public VoteModel(IAnswerService answerService, IVoteService voteService,
            IUserService userService, IPostService postService)
        {
            _answerService = answerService;
            _voteService = voteService;
            _userService = userService;
            _postService = postService;
        }

        internal void ResolveDependency(ILifetimeScope scope)
        {
            _answerService = scope.Resolve<IAnswerService>();
            _postService = scope.Resolve<IPostService>();
            _voteService = scope.Resolve<IVoteService>();
            _userService = scope.Resolve<IUserService>();
        }

        internal async Task UpdateAnswerVote(Guid answerId, Guid userId, string voteType)
        {
            var answer = await _answerService.GetAnswerById(answerId);
            var currentUser = await _userService.GetByIdAsync(userId);

            if (answer == null || currentUser == null)
            {
                throw new InvalidOperationException("Answer or user not found");
            }

            var existingVote = await _voteService.GetByAnswerAndUser(answerId, userId);
            var newVoteType = Enum.Parse<VoteType>(voteType);

            if (existingVote == null)
            {
                var newVote = new AnswerVote
                {
                    Answer = answer,
                    User = currentUser,
                    VoteType = newVoteType
                };

                await _voteService.AddAnswerVote(newVote);
            }
            else
            {
                if (existingVote.VoteType == newVoteType)
                {
                    throw new InvalidOperationException("User has already voted for this answer.");
                }

                existingVote.VoteType = newVoteType;
                await _voteService.UpdateAnswerVote(existingVote);
            }
        }

        internal async Task UpdatePostVote(Guid postId, Guid userId, string voteType)
        {
            var post = await _postService.GetById(postId);
            var currentUser = await _userService.GetByIdAsync(userId);

            if (post == null || currentUser == null)
            {
                throw new InvalidOperationException("Post or user not found");
            }

            var existingVote = await _voteService.GetByPostAndUser(postId, userId);
            var newVoteType = Enum.Parse<VoteType>(voteType);

            if (existingVote == null)
            {
                var newVote = new PostVote
                {
                    Post = post,
                    User = currentUser,
                    VoteType = newVoteType
                };

                await _voteService.AddPostVote(newVote);
            }
            else
            {
                if (existingVote.VoteType == newVoteType)
                {
                    throw new InvalidOperationException("User has already voted for this post.");
                }

                existingVote.VoteType = newVoteType;
                await _voteService.UpdatePostVote(existingVote);
            }
        }
    }
}
