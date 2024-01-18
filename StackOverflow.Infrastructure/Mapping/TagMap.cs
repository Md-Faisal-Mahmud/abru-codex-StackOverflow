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
    public class TagMap : ClassMapping<Tag>
    {
        public TagMap()
        {
            Table("Tags");

            Id(x => x.Id, x =>
            {
                x.Generator(Generators.Guid);
                x.Type(NHibernateUtil.Guid);
                x.Column("Id");
                x.UnsavedValue(Guid.Empty);
            });

            Property(x => x.TagName);
            Property(x => x.TagDescription);

            Bag(x => x.Posts, map =>
            {
                map.Key(k => k.Column("TagId"));
                map.Cascade(Cascade.All | Cascade.DeleteOrphans);
            }, relation => relation.OneToMany());

        }
    }
}
