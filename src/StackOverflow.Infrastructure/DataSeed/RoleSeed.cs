using StackOverflow.Infrastructure.Membership.Entities;

namespace StackOverflow.Infrastructure.DataSeed
{
    public static class RoleSeed
    {
        public static ApplicationRole[] GetRoles
        {
            get
            {
                return new ApplicationRole[]
                {
                    new ApplicationRole
                    {
                        Id = Guid.Parse("433d2f27-cb9b-49fd-bf59-f86d95298a54"),
                        Name = "Admin",
                        NormalizedName = "ADMIN"
                    },
                    new ApplicationRole
                    {
                        Id = Guid.Parse("d50cfb61-04a2-4544-b2b5-4bbcc1f00a27"),
                        Name = "User",
                        NormalizedName = "USER"
                    }
                };
            }
        }
    }
}
