using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;
using StackOverflow.Infrastructure.Entity;

namespace StackOverflow.Infrastructure.Mapping
{
    public class UserMap:ClassMapping<User>
    {
        public UserMap()
        {
            Table("ApplicationUsers"); // Adjust the table name if needed

            Id(x => x.Id, map => map.Generator(Generators.GuidComb));

            Property(x => x.UserName);
            Property(x => x.NormalizedUserName);
            Property(x => x.Email);
            Property(x => x.NormalizedEmail);
            Property(x => x.EmailConfirmed);
            Property(x => x.PasswordHash);
            Property(x => x.SecurityStamp);
            Property(x => x.ConcurrencyStamp);
            Property(x => x.PhoneNumber);
            Property(x => x.PhoneNumberConfirmed);
            Property(x => x.TwoFactorEnabled);

            Property(x => x.LockoutEnd, map =>
            {
                map.Column("LockoutEnd");
                map.Type<NHibernate.Type.DateTimeOffsetType>();
            });

            Property(x => x.LockoutEnabled);
            Property(x => x.AccessFailedCount);
            Property(x => x.DisplayName);

            Bag(x => x.Posts, map =>
            {
                map.Key(k => k.Column("UserId"));
                map.Cascade(Cascade.All | Cascade.DeleteOrphans);
            }, relation => relation.OneToMany());

            Bag(x => x.Answers, map =>
            {
                map.Key(k => k.Column("UserId"));
                map.Cascade(Cascade.All | Cascade.DeleteOrphans);
            }, relation => relation.OneToMany());
        }
    }
}
