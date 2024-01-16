using Microsoft.AspNetCore.Identity;

namespace StackOverflow.Infrastructure.Features.Membership
{
    public class ApplicationRole : IdentityRole<Guid>
	{
		public ApplicationRole()
			: base()
		{
		}
		
		public ApplicationRole(string roleName)
			: base(roleName)
		{
		}
	}
}
