using Microsoft.AspNetCore.Identity;

namespace StackOverflow.Infrastructure.Features.Membership
{
    public class ApplicationUser : IdentityUser<Guid>
    {
        public string DisplayName { get; set; }
    }
}
