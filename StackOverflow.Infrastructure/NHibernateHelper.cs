using NHibernate;
using NHibernate.Tool.hbm2ddl;

namespace StackOverflow.Infrastructure
{
    public class NHibernateHelper
    {
        //private readonly string _connectionString;

        //private ISessionFactory _sessionFactory;

        //public ISessionFactory SessionFactory
        //{
        //    get { return _sessionFactory ?? (_sessionFactory = CreateSessionFactory()); }
        //}

        //public NHibernateHelper(string connectionString)
        //{
        //    _connectionString = connectionString;
        //}

        //private ISessionFactory CreateSessionFactory()
        //{
        //    return Fluently.Configure()
        //        .Database(MsSqlCeConfiguration.Standard.ConnectionString(_connectionString))
        //        .Mappings(m => m.FluentMappings.AddFromAssembly(typeof(NHibernateHelper).Assembly))
        //        .ExposeConfiguration(cfg => new SchemaExport(cfg).Create(true, true))
        //        .BuildSessionFactory();
        //}
    }
}
