using NHibernate.Mapping.ByCode;
using NHibernate;
using NHibernate.Mapping.ByCode.Conformist;
using StackOverflow.Infrastructure.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NHibernate.Type;
using StackOverflow.Infrastructure.Enum;

namespace StackOverflow.Infrastructure.Mapping
{
    public class AnswerVoteMap : ClassMapping<AnswerVote>
    {
        public AnswerVoteMap() 
        {
            Table("AnswerVote");

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

            ManyToOne(x => x.Answer, map =>
            {
                map.NotNullable(true);
                map.Column("AnswerId");
                map.Cascade(Cascade.None);
            });
        }
    }
}
