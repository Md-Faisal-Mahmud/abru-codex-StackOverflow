using Microsoft.AspNetCore.Identity;
using StackOverflow.Infrastructure.Membership.Entities;

namespace StackOverflow.Infrastructure.DataSeed
{
    public static class UserSeed
    {
        public static ApplicationUser[] GetUsers
        {
            get
            {
                var passwordHasher = new PasswordHasher<ApplicationUser>();
                var user = new ApplicationUser()
                {
                    DisplayName = "Normal User",
                    Email = "abc@gmail.com",
                    UserName = "abc@gmail.com",
                    NormalizedEmail = "ABC@GMAIL.COM",
                    NormalizedUserName = "ABCMIN@GMAIL.COM",
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
