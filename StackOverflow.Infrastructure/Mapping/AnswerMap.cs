using NHibernate;
using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;
using StackOverflow.Infrastructure.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StackOverflow.Infrastructure.Mapping
{
    public class AnswerMap : ClassMapping<Answer>
    {
        public AnswerMap()
        {
            Table("Answers");

            Id(x => x.Id, x =>
            {
                x.Generator(Generators.Guid);
                x.Type(NHibernateUtil.Guid);
                x.Column("Id");
                x.UnsavedValue(Guid.Empty);
            });

            Property(x => x.AnswerText, map =>
            {
                map.NotNullable(true);
                map.Length(4000);
                map.Type(NHibernateUtil.String);
            });

            Property(x => x.AcceptedAnswer);

            Property(x => x.CreatedDate);

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
