using StackOverflow.Infrastructure.Entity;
using StackOverflow.Infrastructure.UnitOfWorks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StackOverflow.Application.Services
{
    public class AnswerVoteService : IAnswerVoteService
    {
        private readonly IUnitOfWork _unitOfWork;

        public AnswerVoteService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task AddVote(AnswerVote entity)
        {
            await _unitOfWork.BeginTransaction();
            await _unitOfWork.AnswerVote.AddAsync(entity);
            await _unitOfWork.Commit();
        }

        public async Task DeleteVote(Guid voteId)
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

        public Task<AnswerVote?> GetByAnswerAndUser(Guid answerId, Guid userId)
        {
            return _unitOfWork.AnswerVote.GetSingleAsync(x=>x.Answer.Id==answerId && x.User.Id==userId);
        }


        public async Task UpdateVote(AnswerVote entity)
        {
            await _unitOfWork.BeginTransaction();
            await _unitOfWork.AnswerVote.UpdateAsync(entity);
            await _unitOfWork.Commit();
        }
    }
}
