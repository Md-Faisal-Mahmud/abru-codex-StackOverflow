using Autofac;
using StackOverflow.Application.Services;
using StackOverflow.Infrastructure.Entity;
using StackOverflow.Infrastructure.Enum;

namespace StackOverflow.Web.Models.PostModel
{
    public class PostVoteModel
    {
        private IPostService _postService;
        private IPostVoteService _postVoteService;
        private IUserService _userService;
        public PostVoteModel()
        {

        }
        public PostVoteModel(IPostService postService, IPostVoteService postVoteService,
            IUserService userService)
        {
            _postService = postService;
            _postVoteService = postVoteService;
            _userService = userService;
        }

        internal void ResolveDependency(ILifetimeScope scope)
        {
            _postService = scope.Resolve<IPostService>();
            _postVoteService = scope.Resolve<IPostVoteService>();
            _userService = scope.Resolve<IUserService>();
        }

        internal async Task UpdateVote(Guid postId, Guid userId, string voteType)
        {
            var post = await _postService.GetById(postId);

            var currentUser = await _userService.GetByIdAsync(userId);

            if (post == null && currentUser == null)
            {
                throw new InvalidOperationException("Post or user not found");
            }

            var existingVote = await _postVoteService.GetByPostAndUser(postId, userId);

            if (existingVote == null)
            {
                var newVote = new PostVote
                {
                    Post = post,
                    User = currentUser,
                    VoteType = Enum.Parse<VoteType>(voteType)
                };

                await _postVoteService.AddVote(newVote);
            }

            if (existingVote != null && existingVote.VoteType == Enum.Parse<VoteType>(voteType))
            {
                throw new InvalidOperationException("User has already voted for this post.");
            }

            if (existingVote != null && existingVote.VoteType != Enum.Parse<VoteType>(voteType))
            {
                existingVote.VoteType = Enum.Parse<VoteType>(voteType);
                await _postVoteService.UpdateVote(existingVote);
            }
        }
    }
}
