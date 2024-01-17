using Microsoft.AspNetCore.Identity;
using StackOverflow.Infrastructure.Entity;

namespace StackOverflow.Infrastructure.Features.Membership
{
    public class ApplicationUser : IdentityUser<Guid>
    {
        public string DisplayName { get; set; }
    }
}
