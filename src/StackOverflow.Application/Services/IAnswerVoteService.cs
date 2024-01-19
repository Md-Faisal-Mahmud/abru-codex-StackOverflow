using StackOverflow.Infrastructure.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StackOverflow.Application.Services
{
    public interface IAnswerVoteService
    {
        Task AddVote(AnswerVote entity);
        Task UpdateVote(AnswerVote entity);
        Task<AnswerVote?> GetByAnswerAndUser(Guid answerId,Guid userId);
        Task DeleteVote(Guid voteId);
    }
}
