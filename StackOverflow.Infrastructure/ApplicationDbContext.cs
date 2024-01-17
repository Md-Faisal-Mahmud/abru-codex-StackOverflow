using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using StackOverflow.Infrastructure.Features.Membership;

namespace StackOverflow.Infrastructure
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser,
        ApplicationRole, Guid,
        ApplicationUserClaim, ApplicationUserRole,
        ApplicationUserLogin, ApplicationRoleClaim,
        ApplicationUserToken>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var adminId = new Guid("2c1f8aef-591c-4545-8bb0-d48d3a54afc3");
            PasswordHasher<ApplicationUser> ph = new PasswordHasher<ApplicationUser>();
            var admin = new ApplicationUser
            {
                Id = adminId,
                DisplayName ="Super Admin",
                UserName = "admin@gmail.com",
                NormalizedUserName = "ADMIN@GMAIL.COM",
                Email = "admin@gmail.com",
                NormalizedEmail="ADMIN@GMAIL.COM",
                EmailConfirmed = true,
                LockoutEnabled = false,
                SecurityStamp = "BFCC7B453A8B4B6C8A4C93EE28A3B4A8"
            };
            admin.PasswordHash = ph.HashPassword(admin, "123456");

            modelBuilder.Entity<ApplicationUser>().HasData(admin);

            modelBuilder.Entity<ApplicationRole>().HasData(new List<ApplicationRole>()
            {
                new ApplicationRole
                {
                    Id = new Guid("a4607923-9239-4c6d-95cf-0d2ccf55c0d6"),
                    Name = "Admin",
                    NormalizedName = "ADMIN"
                },
                new ApplicationRole
                {
                    Id = new Guid("aa4e5374-e8b5-42a4-87b9-702f5f79a718"),
                    Name = "User",
                    NormalizedName = "USER"
                }
            });

            modelBuilder.Entity<ApplicationUserRole>().HasData(new ApplicationUserRole()
            {
                RoleId = new Guid("a4607923-9239-4c6d-95cf-0d2ccf55c0d6"),
                UserId = adminId
            });

            base.OnModelCreating(modelBuilder);
        }
    }
}