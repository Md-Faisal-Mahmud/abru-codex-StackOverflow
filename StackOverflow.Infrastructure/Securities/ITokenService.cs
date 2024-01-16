using System.Security.Claims;

namespace DevTeamV2.Infrastructure.Securities
{
    public interface ITokenService
    {
        Task<string> GetJwtToken(IList<Claim> claims);
    }
}