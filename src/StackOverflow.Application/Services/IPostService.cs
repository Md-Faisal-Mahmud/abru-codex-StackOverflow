using StackOverflow.Infrastructure.Entity;
using System.Linq.Expressions;

namespace StackOverflow.Application.Services
{
    public interface IPostService
    {
        Task AddPost(Post entity);
        Task UpdatePost(Post entity);
        Task<Post?> GetById(Guid id);
        Task<IList<Post>> GetAllPost();
        Task<IList<Post>> GetUserPost(Guid userId);

        Task<(IList<Post> data, int total, int totalDisplay, int totalPages)> GetPaginatePost(Expression<Func<Post, bool>> filter = null!,
                            int pageIndex = 1,
                            int pageSize = 10);
        Task DeletePost(Guid id);
    }
}
