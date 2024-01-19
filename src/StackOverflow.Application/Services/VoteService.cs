using StackOverflow.Infrastructure.Entity;
using StackOverflow.Infrastructure.UnitOfWorks;

namespace StackOverflow.Application.Services
{
    public class VoteService : IVoteService
    {
        private readonly IUnitOfWork _unitOfWork;

        public VoteService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task AddAnswerVote(AnswerVote entity)
        {
            await _unitOfWork.BeginTransaction();
            await _unitOfWork.AnswerVote.AddAsync(entity);
            await _unitOfWork.Commit();
        }

        public async Task AddPostVote(PostVote entity)
        {
            await _unitOfWork.BeginTransaction();
            await _unitOfWork.PostVote.AddAsync(entity);
            await _unitOfWork.Commit();
        }

        public async Task DeleteAnswerVote(Guid voteId)
        {
            await _unitOfWork.BeginTransaction();

            var voteEntity = await _unitOfWork.AnswerVote.GetSingleAsync(voteId);

            if (voteEntity == null)
            {
                throw new InvalidOperationException("vote not found");
            }
            await _unitOfWork.AnswerVote.DeleteAsync(voteEntity);
            await _unitOfWork.Commit();
        }

        public Task DeletePostVote(Guid voteId)
        {
            throw new NotImplementedException();
        }

        public async Task<AnswerVote?> GetByAnswerAndUser(Guid answerId, Guid userId)
        {
            return await _unitOfWork.AnswerVote
                .GetSingleAsync(x => x.Answer.Id == answerId && x.User.Id == userId);
        }

        public async Task<PostVote?> GetByPostAndUser(Guid postId, Guid userId)
        {
            return await _unitOfWork.PostVote
                .GetSingleAsync(x => x.Post.Id == postId && x.User.Id == userId);
        }

        public async Task UpdateAnswerVote(AnswerVote entity)
        {
            await _unitOfWork.BeginTransaction();
            await _unitOfWork.AnswerVote.UpdateAsync(entity);
            await _unitOfWork.Commit();
        }

        public async Task UpdatePostVote(PostVote entity)
        {
            await _unitOfWork.BeginTransaction();
            await _unitOfWork.PostVote.UpdateAsync(entity);
            await _unitOfWork.Commit();
        }
    }
}
