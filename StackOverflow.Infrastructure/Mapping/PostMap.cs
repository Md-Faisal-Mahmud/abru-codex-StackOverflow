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

            Property(x => x.Title);
            Property(x => x.Description);
            Property(x => x.CreatedDate);

            ManyToOne(x => x.User, map =>
            {
                map.Column("UserId");
                map.Cascade(Cascade.None);
            });

            Bag(x => x.Tags, map =>
            {
                map.Table("TagPosts"); // Specify the join table
                map.Key(k => k.Column("PostId"));
                map.Cascade(Cascade.All | Cascade.DeleteOrphans);
                map.Inverse(true);
            }, relation => relation.ManyToMany(m => m.Column("TagId")));

            Bag(x => x.Answers, map =>
            {
                map.Key(k => k.Column("PostId"));
                map.Cascade(Cascade.All | Cascade.DeleteOrphans);
            }, relation => relation.OneToMany());
        }

    }
}
