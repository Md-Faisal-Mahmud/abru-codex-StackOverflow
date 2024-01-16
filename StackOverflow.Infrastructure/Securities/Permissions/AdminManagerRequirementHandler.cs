using Microsoft.AspNetCore.Authorization;

namespace DevTeamV2.Infrastructure.Securities.Permissions
{
    public class AdminManagerRequirementHandler : AuthorizationHandler<AdminManagerRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, AdminManagerRequirement requirement)
        {
            if (context.User.HasClaim(c =>
                (c.Type == "AdminAccess" && c.Value == "Administrator") ||
                (c.Type == "ManagerAccess" && c.Value == "Manager")))
            {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }
    }
}
