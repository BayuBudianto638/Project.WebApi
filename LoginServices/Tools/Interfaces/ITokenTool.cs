using System.Security.Claims;

namespace LoginServices.Tools.Interfaces
{
    public interface ITokenTool
    {
        public string GenerateAccessToken(IEnumerable<Claim> claims);
        public string GenerateRefreshToken();
        public ClaimsPrincipal GetPrincipalFromExpiredToken(string token);
        public void InvalidateToken(string token);
        public bool IsTokenBlacklisted(string token);
    }
}
