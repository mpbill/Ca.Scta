using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Ca.Scta.Account;
using Microsoft.AspNet.Identity;
using Microsoft.Owin;
using Microsoft.Owin.Security;

namespace Ca.Scta.Api
{
    public class AppUserTokenAuthenticationMiddleware : OwinMiddleware
    {
        private readonly IAppUserTokenService _tokenService;
        public AppUserTokenAuthenticationMiddleware(
            OwinMiddleware next,
            IAppUserTokenService tokenService
            ) : base(next)
        {
            _tokenService = tokenService;
        }

        public override async Task Invoke(IOwinContext context)
        {
            const string authHeaderKey = "Authorization";
            try
            {
                var authHeaderValue = context.Request.Headers[authHeaderKey];
                if (authHeaderValue != null)
                {
                    var token = new string(authHeaderValue.Skip(7).ToArray());
                    var result = _tokenService.ValidateToken(token);
                    if (result.IsValid)
                    {
                        context.Authentication.SignIn(result.ClaimsPrincipal.Identities.ToArray());
                        context.Authentication.User = result.ClaimsPrincipal;
                        context.Authentication.Challenge(DefaultAuthenticationTypes.ExternalBearer);
                    }
                    
                }

                
                
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex);
            }
            finally
            {
                await Next.Invoke(context);
            }

            
        }
    }
}
