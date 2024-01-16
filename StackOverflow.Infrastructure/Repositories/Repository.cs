using NHibernate;
using System.Linq.Expressions;

namespace StackOverflow.Infrastructure.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly ISession _session;

        public Repository(ISession session)
        {
            _session = session;
        }

        #region Implementation of IRepository<T>

        public void Add(T entity)
        {
            _session.Save(entity);
        }

        public bool Add(IEnumerable<T> items)
        {
            foreach (T item in items)
            {
                _session.Save(item);
            }

            return true;
        }

        public bool Update(T entity)
        {
            _session.Update(entity);

            return true;
        }

        public bool Update(IEnumerable<T> items)
        {
            foreach (T item in items)
            {
                _session.Update(item);
            }

            return true;
        }

        public bool Delete(T entity)
        {
            _session.Delete(entity);

            return true;
        }

        public bool Delete(IEnumerable<T> entities)
        {
            foreach (T entity in entities)
            {
                _session.Delete(entity);
            }

            return true;
        }

        public IQueryable<T> All()
        {
            return _session.Query<T>();
        }

        public T FindBy(Expression<Func<T, bool>> expression)
        {
            return FilterBy(expression).Single();
        }

        public T FindBy(int id)
        {
            return _session.Get<T>(id);
        }

        public IQueryable<T> FilterBy(Expression<Func<T, bool>> expression)
        {
            return All().Where(expression).AsQueryable();
        }

        #endregion
    }
}
