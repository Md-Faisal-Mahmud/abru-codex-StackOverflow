using NHibernate;
using StackOverflow.Infrastructure.Repositories;

namespace StackOverflow.Infrastructure.UnitOfWorks
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ISessionFactory _sessionFactory;

        private readonly ITransaction _transaction;

        public ISession Session { get; private set; }

        public IPostRepository Post { get; private set; }
        public IUserRepository User { get; private set; }

        public ITagRepository Tag { get; private set; }

        public UnitOfWork(ISessionFactory sessionFactory, IPostRepository postRepository,
                          IUserRepository userRepository, ITagRepository tagRepository)
        {
            _sessionFactory = sessionFactory;

            Session = _sessionFactory.OpenSession();

            Session.FlushMode = FlushMode.Auto;

            _transaction = Session.BeginTransaction();

            Post = postRepository;
            User = userRepository;
            Tag = tagRepository;
        }

        public void Dispose()
        {
            if (Session.IsOpen)
            {
                Session.Close();
            }
        }

        public void Commit()
        {
            if (!_transaction.IsActive)
            {
                throw new InvalidOperationException("No active transaction");
            }

            _transaction.Commit();
        }

        public void Rollback()
        {
            if (_transaction.IsActive)
            {
                _transaction.Rollback();
            }
        }
    }
}
