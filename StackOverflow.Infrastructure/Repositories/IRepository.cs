using StackOverflow.Infrastructure.Entity;
using System.Linq.Expressions;

namespace StackOverflow.Infrastructure.Repositories
{
    public interface IRepository<T, in TKey>
    where T : class, IEntity<TKey>
    {
        Task AddAsync(T entity);

        Task UpdateAsync(T entity);

        Task DeleteAsync(T entity);

        Task AddOrUpdateAsync(T entity);

        Task<int> GetCountAsync(Expression<Func<T, bool>>? predicate = null!);

        Task<T?> GetSingleAsync(TKey id);

        Task<T?> GetSingleAsync(Expression<Func<T, bool>> predicate);

        Task<IList<T>> GetAllAsync();

        Task<IList<T>> FindAsync(Expression<Func<T, bool>>? predicate = null!);
        Task<(IList<T> data, int total, int totalDisplay, int totalPages)> GetByPagingAsync(Expression<Func<T, bool>> filter = null!, int pageIndex = 1, int pageSize = 10);
    }
}
