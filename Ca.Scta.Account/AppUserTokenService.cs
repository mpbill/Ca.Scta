using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using System.IdentityModel.Tokens.Jwt;
using System.Runtime.InteropServices;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;

namespace Ca.Scta.Account
{
    public class ValidTokenResult
    {
        private ValidTokenResult(bool isValid, ClaimsPrincipal claimsPrincipal, SecurityToken securityToken)
        {
            IsValid = isValid;
            ClaimsPrincipal = claimsPrincipal;
            SecurityToken = securityToken;
        }

        public static ValidTokenResult ValidToken(ClaimsPrincipal principal, SecurityToken token)
        {
            return new ValidTokenResult(true,principal,token);
        }

        public static ValidTokenResult InValidToken()
        {
            return new ValidTokenResult(false,null,null);
        }
        public bool IsValid { get; private set; }
        public ClaimsPrincipal ClaimsPrincipal { get; private set; }
        public SecurityToken SecurityToken { get; private set; }
    }
    public class AppUserTokenService : IAppUserTokenService
    {
        private readonly TokenValidationParameters _validationParameters;
        private readonly string _tokenIssuer;
        private readonly IAppUserClaimsIdentityFactory _claimsIdentityFactory;
        private readonly ISigningCredentialsFactory _signingCredentialsFactory;
        private readonly JwtSecurityTokenHandler _handler;
        public AppUserTokenService(
            IAppUserClaimsIdentityFactory claimsIdentityFactory,
            ISigningCredentialsFactory signingCredentialsFactory)
        {
            _claimsIdentityFactory = claimsIdentityFactory;
            _signingCredentialsFactory = signingCredentialsFactory;
            _handler=new JwtSecurityTokenHandler();
            _tokenIssuer= "Ca.Scta";
            _validationParameters = new TokenValidationParameters
            {
                IssuerSigningKey = _signingCredentialsFactory.GetKey(),
                ValidIssuer = _tokenIssuer,
                ValidateLifetime = true,
                ValidateIssuer = true,
                ValidateAudience = false

            };
        }

        public ValidTokenResult ValidateToken(string token)
        {
            
            SecurityToken validatedToken;
            ClaimsPrincipal principal;

            try
            {
                principal = _handler.ValidateToken(token, _validationParameters, out validatedToken);
                return ValidTokenResult.ValidToken(principal,validatedToken);
            }
            catch
            {
                return ValidTokenResult.InValidToken();
            }
            
        }
        public string CreateTokenAsync(AppUser user)
        {
            var token = CreateToken(user);
            var tokenString = _handler.WriteToken(token);
            return tokenString;
        }

        private JwtSecurityToken CreateToken(AppUser user)
        {
            var descriptor = CreateDescriptor(user);
            var token = _handler.CreateJwtSecurityToken(descriptor);
            return token;
        }

        private SecurityTokenDescriptor CreateDescriptor(AppUser user)
        {
            var now = DateTime.UtcNow;
            var descriptor = new SecurityTokenDescriptor();
            descriptor.Expires = now.AddHours(1);
            descriptor.IssuedAt = now;
            descriptor.NotBefore = now;
            descriptor.Subject = _claimsIdentityFactory.CreateClaimsIdentity(user);
            descriptor.SigningCredentials = _signingCredentialsFactory.GetSigningCredentials();
            descriptor.Issuer = _tokenIssuer;
            return descriptor;
        }


    }

}
