using Microsoft.AspNetCore.Identity;

namespace StackOverflow.Infrastructure.Membership.Entities
{
    public class ApplicationUser : IdentityUser<Guid>
    {
        public virtual string DisplayName { get; set; }

        public virtual long? LockoutEndUnixTimeSeconds { get; set; }

        public override DateTimeOffset? LockoutEnd
        {
            get
            {
                if (!LockoutEndUnixTimeSeconds.HasValue)
                {
                    return null;
                }
                var offset = DateTimeOffset.FromUnixTimeSeconds(
                    LockoutEndUnixTimeSeconds.Value
                );
                return TimeZoneInfo.ConvertTime(offset, TimeZoneInfo.Local);
            }
            set
            {
                if (value.HasValue)
                {
                    LockoutEndUnixTimeSeconds = value.Value.ToUnixTimeSeconds();
                }
                else
                {
                    LockoutEndUnixTimeSeconds = null;
                }
            }
        }
    }
}
