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
            Id(x => x.Id, map =>
            {
                map.Column("Id");
                map.Generator(Generators.Identity);
            });
            Property(x => x.Title);
            Property(x => x.CreatedDate);
            Property(x => x.CreatedByUserId);

            // Other mappings...

            Table("Post"); // Specify your table name here
        }


    }
}
