using System.Security.Claims;

namespace Ca.Scta.Account
{
    public interface IAppUserTokenService
    {
        string CreateTokenAsync(AppUser user);
        ClaimsPrincipal ValidateToken(string token);
    }
}