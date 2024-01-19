using NHibernate;
using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;
using NHibernate.Type;
using StackOverflow.Infrastructure.Entity;
using StackOverflow.Infrastructure.Enum;

namespace StackOverflow.Infrastructure.Mapping
{
    public class PostVoteMap : ClassMapping<PostVote>
    {
        public PostVoteMap()
        {
            Table("PostVote");

            Id(x => x.Id, x =>
            {
                x.Generator(Generators.Guid);
                x.Type(NHibernateUtil.Guid);
                x.Column("Id");
                x.UnsavedValue(Guid.Empty);
            });

            Property(x => x.VoteType, x =>
            {
                x.Type<EnumType<VoteType>>();
                x.NotNullable(true);
                x.Column("VoteType");
            });

            ManyToOne(x => x.User, map =>
            {
                map.NotNullable(true);
                map.Column("UserId");
                map.Cascade(Cascade.None);
            });

            ManyToOne(x => x.Post, map =>
            {
                map.NotNullable(true);
                map.Column("PostId");
                map.Cascade(Cascade.None);
            });
        }
    }
}
