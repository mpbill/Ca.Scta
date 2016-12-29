using System.Collections.Generic;
using System.Security.Claims;

namespace Ca.Scta.Account
{
    public class AppUserClaimsIdentityFactory : IAppUserClaimsIdentityFactory
    {
        public ClaimsIdentity CreateClaimsIdentity(AppUser user)
        {
            var idClaim = new Claim("UserId",user.Id.ToString(),ClaimValueTypes.Integer32);
            var userNameClaim = new Claim("UserName", user.UserName);
            var identity = new AppUserIdentity(user.UserName);
            var claims = new List<Claim> {idClaim, userNameClaim};
            var ci = new ClaimsIdentity(identity, claims);
            return ci;

        }
    }
}