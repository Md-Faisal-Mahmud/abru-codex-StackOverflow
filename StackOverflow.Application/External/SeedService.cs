using Microsoft.AspNetCore.Identity;
using StackOverflow.Infrastructure.DataSeed;
using StackOverflow.Infrastructure.Membership.Entities;

namespace StackOverflow.Application.External
{
    public class SeedService : ISeedService
    {
        private readonly RoleManager<ApplicationRole> _roleManager;
        private readonly UserManager<ApplicationUser> _userManager;

        public SeedService(RoleManager<ApplicationRole> roleManager,
        UserManager<ApplicationUser> userManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
        }

        public async Task DataSeed()
        {
            await SeedRole();
            await SeedUser();
        }

        private async Task SeedUser()
        {
            var users = AdminSeed.GetUsers;

            foreach (var user in users)
            {
                var result = await _userManager.FindByNameAsync(user.NormalizedUserName);

                if (result == null)
                {
                    await _userManager.CreateAsync(user);
                    await _userManager.AddToRoleAsync(user, "ADMIN");
                }
            }
        }

        private async Task SeedRole()
        {
            var roles = RoleSeed.GetRoles;

            foreach (var role in roles)
            {
                var isExist = await _roleManager.RoleExistsAsync(role.NormalizedName);

                if (!isExist)
                    await _roleManager.CreateAsync(role);
            }
        }
    }
}
