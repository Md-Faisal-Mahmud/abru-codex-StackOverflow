using NHibernate;
using StackOverflow.Infrastructure.Entity;

namespace StackOverflow.Infrastructure.Repositories
{
    public class PostVoteRepository : Repository<PostVote, Guid>, IPostVoteRepository
    {
        public PostVoteRepository(ISession session) : base(session)
        {
        }
    }
}
