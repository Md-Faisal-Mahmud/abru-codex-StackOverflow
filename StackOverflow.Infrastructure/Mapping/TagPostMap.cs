using NHibernate;
using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;
using StackOverflow.Infrastructure.Entity;

namespace StackOverflow.Infrastructure.Mapping
{
    public class TagPostMap : ClassMapping<TagPost>
    {
        public TagPostMap()
        {
            Table("TagPosts");

            Id(x => x.Id, x =>
            {
                x.Generator(Generators.Guid);
                x.Type(NHibernateUtil.Guid);
                x.Column("Id");
                x.UnsavedValue(Guid.Empty);
            });

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
