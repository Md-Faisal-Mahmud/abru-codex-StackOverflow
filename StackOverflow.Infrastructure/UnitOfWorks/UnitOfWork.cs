using NHibernate;
using StackOverflow.Infrastructure.Repositories;

namespace StackOverflow.Infrastructure.UnitOfWorks
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ISession _session;
        private readonly ITransaction _transaction;

        private readonly IPostRepository _postRepository;
        private readonly ITagRepository _tagRepository;
        private readonly IUserRepository _userRepository;

        public UnitOfWork(ISession session,
            IPostRepository postRepository,
            ITagRepository tagRepository,
            IUserRepository userRepository)
        {
            _session = session;
            _transaction = _session.BeginTransaction();
            _postRepository = postRepository;
            _tagRepository = tagRepository;
            _userRepository = userRepository;
        }

        public IPostRepository Post => _postRepository;
        public ITagRepository Tag => _tagRepository;
        public IUserRepository User => _userRepository;

        public async Task BeginTransaction()
        {
            await Task.Run(() => _transaction.Begin());
        }

        public async Task Commit()
        {
            await Task.Run(() => _transaction.Commit());
        }

        public async Task Rollback()
        {
            await Task.Run(() => _transaction.Rollback());
        }

        public void Dispose()
        {
            _transaction.Dispose();
            _session.Dispose();
        }

    }
}
