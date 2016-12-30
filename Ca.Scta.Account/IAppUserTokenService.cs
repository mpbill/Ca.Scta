using System.Security.Claims;

namespace Ca.Scta.Account
{
    public interface IAppUserTokenService
    {
        string CreateTokenAsync(AppUser user);
        ValidTokenResult ValidateToken(string token);
    }
}