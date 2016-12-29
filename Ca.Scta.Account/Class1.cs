using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using System.IdentityModel.Tokens.Jwt;
using System.Runtime.InteropServices;
using System.Security.Principal;
using Microsoft.IdentityModel.Tokens;

namespace Ca.Scta.Account
{
    class AppUserIdentity : IIdentity
    {
        public AppUserIdentity(string userName)
        {
            Name = userName;
            AuthenticationType = "password";
            IsAuthenticated = true;
        }
        public string Name { get; private set; }
        public string AuthenticationType { get; private set; }
        public bool IsAuthenticated { get; private set; }
    }

    public class AppUserTokenService
    {
        private readonly IAppUserClaimsIdentityFactory _claimsIdentityFactory;

        public AppUserTokenService(IAppUserClaimsIdentityFactory claimsIdentityFactory)
        {
            _claimsIdentityFactory = claimsIdentityFactory;
        }

        public async Task<JwtSecurityToken> CreateTokenAsync(AppUser user)
        {
            var now = DateTime.UtcNow;
            JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();
            var description = new SecurityTokenDescriptor();
            description.Expires = now.AddHours(1);
            description.IssuedAt = now;
            description.NotBefore = now;
            description.Subject = _claimsIdentityFactory.CreateClaimsIdentity(user);
            description.EncryptingCredentials = new EncryptingCredentials();
            description.Issuer = "Ca.Scta";
            handler.CreateJwtSecurityToken()
        }


    }

}
