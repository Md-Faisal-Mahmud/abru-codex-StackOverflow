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

            Property(x => x.AnswerText);
            Property(x => x.CreatedDate);

            ManyToOne(x => x.User, map =>
            {
                map.Column("UserId");
                map.Cascade(Cascade.None);
            });

            ManyToOne(x => x.Post, map =>
            {
                map.Column("PostId");
                map.Cascade(Cascade.None);
            });
        }
    }
}
