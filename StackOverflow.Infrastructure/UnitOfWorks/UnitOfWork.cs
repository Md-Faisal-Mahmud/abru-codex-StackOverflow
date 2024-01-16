﻿using NHibernate;
using StackOverflow.Application;
using StackOverflow.Infrastructure.Repositories;
using System.Data;

namespace StackOverflow.Infrastructure.UnitOfWorks
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ISessionFactory _sessionFactory;

        private readonly ITransaction _transaction;

        public ISession Session { get; private set; }

        public IPostRepository PostRepository { get; set; }

        public UnitOfWork(ISessionFactory sessionFactory, IPostRepository postRepository)
        {
            _sessionFactory = sessionFactory;

            Session = _sessionFactory.OpenSession();

            Session.FlushMode = FlushMode.Auto;

            _transaction = Session.BeginTransaction();
            PostRepository = postRepository;
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
