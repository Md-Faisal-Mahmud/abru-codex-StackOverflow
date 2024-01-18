using NHibernate;
using NHibernate.SqlCommand;
using NHibernate.Transform;
using StackOverflow.Infrastructure.Entity;
using StackOverflow.Infrastructure.Features.Membership;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StackOverflow.Infrastructure.Repositories
{
    public class PostRepository : Repository<Post,Guid>, IPostRepository
    {
        private ISession _session;
        public PostRepository(ISession session) : base(session)
        {
            _session = session;
        }
    }
}
