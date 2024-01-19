using StackOverflow.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StackOverflow.Infrastructure.UnitOfWorks
{
    public interface IUnitOfWork : IDisposable
    {
        Task BeginTransaction();

        Task Commit();

        Task Rollback();

        IPostRepository Post { get; }
        IUserRepository User { get; }
        ITagRepository Tag { get; }
        IAnswerRepository Answer { get; }
        IAnswerVoteRepository AnswerVote { get; }

    }
}
