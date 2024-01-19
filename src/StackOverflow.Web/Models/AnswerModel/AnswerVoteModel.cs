using Autofac;
using StackOverflow.Application.Services;
using StackOverflow.Infrastructure.Entity;
using StackOverflow.Infrastructure.Enum;

namespace StackOverflow.Web.Models.AnswerModel
{
    public class AnswerVoteModel
    {
        private IAnswerService _answerService;
        private IAnswerVoteService _answerVoteService;
        private IUserService _userService;
        public AnswerVoteModel()
        {
            
        }
        public AnswerVoteModel(IAnswerService answerService, IAnswerVoteService answerVoteService,
            IUserService userService)
        {
            _answerService = answerService;
            _answerVoteService = answerVoteService;
            _userService = userService;
        }

        internal void ResolveDependency(ILifetimeScope scope)
        {
            _answerService = scope.Resolve<IAnswerService>();
            _answerVoteService = scope.Resolve<IAnswerVoteService>();
            _userService = scope.Resolve<IUserService>();
        }

        internal async Task UpdateVote(Guid answerId,Guid userId,string voteType)
        {
            var answer =await _answerService.GetAnswerById(answerId);

            var currentUser = await _userService.GetByIdAsync(userId);

            if(answer ==null && currentUser ==null)
            {
                throw new InvalidOperationException("Answer or user not found");
            }

            var existingVote =await _answerVoteService.GetByAnswerAndUser(answerId, userId);

            if(existingVote==null)
            {
                var newVote = new AnswerVote
                {
                    Answer = answer,
                    User = currentUser,
                    VoteType = Enum.Parse<VoteType>(voteType)
                };

                await _answerVoteService.AddVote(newVote);
            }
             
            if (existingVote!=null && existingVote.VoteType == Enum.Parse<VoteType>(voteType))
            {
                throw new InvalidOperationException("User has already voted for this answer.");
            }

            if(existingVote != null && existingVote.VoteType != Enum.Parse<VoteType>(voteType))
            {
                existingVote.VoteType = Enum.Parse<VoteType>(voteType);
                await _answerVoteService.UpdateVote(existingVote);
            }
        }
    }
}
