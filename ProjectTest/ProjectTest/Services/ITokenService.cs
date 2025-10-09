using System.Security.Claims;

namespace ProjectTest.Services
{
    public interface ITokenService
    {
        string GerenateAccessToken(IEnumerable<Claim> claims);
        string GerenateRefreshToken();
        ClaimsPrincipal GetPrincipalFromExpiredToken(string token);
    }
}
