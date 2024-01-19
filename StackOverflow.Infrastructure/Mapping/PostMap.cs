using NHibernate;
using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;
using StackOverflow.Infrastructure.Entity;

namespace StackOverflow.Infrastructure.Mapping
{
    public class PostMap : ClassMapping<Post>
    {
        public PostMap()
        {
            Table("Posts");

            Id(x => x.Id, x =>
            {
                x.Generator(Generators.Guid);
                x.Type(NHibernateUtil.Guid);
                x.Column("Id");
                x.UnsavedValue(Guid.Empty);
            });

            Property(x => x.Title, map =>
            {
                map.Length(100);
                map.NotNullable(true);
                map.Type(NHibernateUtil.String);
            });
            Property(x => x.Description, map =>
            {
                map.Length(4000);
                map.NotNullable(true);
                map.Type(NHibernateUtil.String);
            });

            Property(x => x.CreatedDate);

            ManyToOne(x => x.User, map =>
            {
                map.NotNullable(true);
                map.Column("UserId");
                map.Cascade(Cascade.None);
            });

            ManyToOne(x => x.Tag, map =>
            {
                map.NotNullable(true);
                map.Column("TagId");
                map.Cascade(Cascade.None);
            });

            Bag(x => x.Answers, map =>
            {
                map.Key(k => k.Column("PostId"));
                map.Cascade(Cascade.All | Cascade.DeleteOrphans);
            }, relation => relation.OneToMany());
        }

    }
}
