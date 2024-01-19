using Microsoft.AspNetCore.Identity;
using StackOverflow.Infrastructure.Membership.Entities;

namespace StackOverflow.Infrastructure.DataSeed
{
    public static class AdminSeed
    {
        public static ApplicationUser[] GetUsers
        {
            get
            {
                var passwordHasher = new PasswordHasher<ApplicationUser>();
                var user = new ApplicationUser()
                {
                    DisplayName = "Super Admin",
                    Email = "sadmin@gmail.com",
                    UserName = "sadmin@gmail.com",
                    NormalizedEmail = "SADMIN@GMAIL.COM",
                    NormalizedUserName = "SADMIN@GMAIL.COM",
                    LockoutEnabled = true,
                    SecurityStamp = Guid.NewGuid().ToString(),
                    EmailConfirmed = true,
                };

                var passwordHash = passwordHasher.HashPassword(user, "123456");
                user.PasswordHash = passwordHash;

                return new ApplicationUser[] { user };
            }
        }
    }
}
