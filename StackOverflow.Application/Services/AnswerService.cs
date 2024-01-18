using StackOverflow.Infrastructure.Entity;
using StackOverflow.Infrastructure.UnitOfWorks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StackOverflow.Application.Services
{
    public class AnswerService : IAnswerService
    {
        private readonly IUnitOfWork _unitOfWork;

        public AnswerService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task AddAnswer(Answer entity)
        {
            await _unitOfWork.BeginTransaction();
            await _unitOfWork.Answer.AddAsync(entity);
            await _unitOfWork.Commit();
        }

        public async Task DeleteTag(Guid id)
        {
            await _unitOfWork.BeginTransaction();

            var answerEntity = await _unitOfWork.Answer.GetSingleAsync(id);

            if (answerEntity == null)
            {
                throw new InvalidOperationException("answer not found");
            }
            await _unitOfWork.Answer.DeleteAsync(answerEntity);
            await _unitOfWork.Commit();
        }
    }
}
