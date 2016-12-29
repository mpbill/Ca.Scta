using System.Security.Claims;

namespace Ca.Scta.Account
{
    public interface IAppUserClaimsIdentityFactory
    {
        ClaimsIdentity CreateClaimsIdentity(AppUser user);
    }
}