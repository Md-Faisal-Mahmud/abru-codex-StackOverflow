using StackOverflow.Infrastructure.Features.Membership;

namespace StackOverflow.Infrastructure.Entity
{
    public class Post
    {
        public virtual int Id { get; set; }
        public virtual string Title { get; set; }
        public virtual string Description { get; set; }
        public virtual DateTime CreatedDate { get; set; } = DateTime.UtcNow;
        public virtual IList<TagPost> Tags { get; set; }
        public virtual IList<Answer> Answers { get; set; } = new List<Answer>();
        public virtual User User { get; set; }
    }
}
