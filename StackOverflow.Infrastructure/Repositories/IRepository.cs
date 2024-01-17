using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace StackOverflow.Infrastructure.Repositories
{
    public interface IRepository<T> where T : class
    {
        void Add(T entity);

        bool Add(IEnumerable<T> items);

        bool Update(T entity);

        bool Update(IEnumerable<T> items);

        void Delete(T entity);

        bool Delete(IEnumerable<T> entities);

        IQueryable<T> All();

        T FindBy(Expression<Func<T, bool>> expression);

        T FindBy(int id);

        IQueryable<T> FilterBy(Expression<Func<T, bool>> expression);
    }
}
