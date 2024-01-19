using StackOverflow.Infrastructure.Enum;

namespace StackOverflow.Infrastructure.Entity
{
    public class AnswerVote:IEntity<Guid>
    {
        public virtual Guid Id { get; set; }
        public virtual Answer Answer { get; set; }
        public virtual User User { get; set; }
        public virtual VoteType VoteType { get; set; }
    }
}
