using StackOverflow.Infrastructure.Features.Membership;

namespace StackOverflow.Infrastructure.Entity
{
    public class Post
    {
        public virtual int Id { get; set; }
        public virtual string Title { get; set; }
        public virtual DateTime CreatedDate { get; set; } = DateTime.UtcNow;
        public virtual Guid CreatedByUserId { get; set; }
    }
}
