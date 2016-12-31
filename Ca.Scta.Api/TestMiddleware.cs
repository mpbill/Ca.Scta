using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Owin;
using Microsoft.Owin.Security;

namespace Ca.Scta.Api
{
    public class TestMiddleware : OwinMiddleware
    {
        public TestMiddleware(OwinMiddleware next) : base(next)
        {
        }

        public override async Task Invoke(IOwinContext context)
        {
            context.Authentication.SignIn(new ClaimsIdentity());
            Console.WriteLine("middleware");
            await Next.Invoke(context);
            
        }
    }
}
