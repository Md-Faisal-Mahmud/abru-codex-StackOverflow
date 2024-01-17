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

            Id(x => x.Id, map => map.Generator(Generators.Identity));

            Property(x => x.TagName);
            Property(x => x.TagDescription);

            Bag(x => x.Posts, map =>
            {
                map.Table("TagPosts"); // Specify the join table
                map.Key(k => k.Column("TagId"));
                map.Cascade(Cascade.All | Cascade.DeleteOrphans);
                map.Inverse(true);
            }, relation => relation.ManyToMany(m => m.Column("PostId")));
        }
    }
}
