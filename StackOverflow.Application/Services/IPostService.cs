using StackOverflow.Infrastructure.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StackOverflow.Application.Services
{
    public interface IPostService
    {
        Task AddPost(Post entity);
        Task UpdatePost(Post entity);
        Task<Post?> GetById(Guid id);
        Task<IList<Post>> GetAllPost();
        Task<IList<Post>> GetUserPost(Guid userId);
        Task DeletePost(Guid id);
    }
}
