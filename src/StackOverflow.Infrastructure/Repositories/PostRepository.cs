using NHibernate;
using StackOverflow.Infrastructure.Entity;

namespace StackOverflow.Infrastructure.Repositories
{
    public class PostRepository : Repository<Post, Guid>, IPostRepository
    {
        public PostRepository(ISession session) : base(session)
        {
        }
    }
}
