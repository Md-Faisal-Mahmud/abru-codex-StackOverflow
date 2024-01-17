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
    public class TagPostMap : ClassMapping<TagPost>
    {
        public TagPostMap()
        {
            Table("TagPosts");

            Id(x => x.Id, map => map.Generator(Generators.Identity));

            ManyToOne(x => x.Tag, map =>
            {
                map.Column("TagId");
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
