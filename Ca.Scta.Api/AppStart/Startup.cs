using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Formatting;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using Ca.Scta.Account;
using Ca.Scta.Api.Controllers;
using Ca.Scta.Api.Controllers.Account;
using Ca.Scta.Api.Middleware;
using Ca.Scta.Dal.Connection;
using Ca.Scta.Dal.Cqrs.AppUser.Commands;
using Ca.Scta.Dal.Cqrs.AppUser.Queries;
using Ca.Scta.Dal.Cqrs.Base;
using Ca.Scta.Dal.Models;
using Microsoft.AspNet.Identity;
using Owin;
using SimpleInjector;
using SimpleInjector.Diagnostics;
using SimpleInjector.Integration.WebApi;

namespace Ca.Scta.Api.AppStart
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            var iocContainer = GetIocContainer();

            HttpConfiguration config = new HttpConfiguration();
            config.DependencyResolver = new SimpleInjectorWebApiDependencyResolver(iocContainer);
            config.MapHttpAttributeRoutes();
            config.Formatters.Remove(config.Formatters.XmlFormatter);
            config.EnsureInitialized();

            app.Use<AppUserTokenAuthenticationMiddleware>(iocContainer.GetInstance<IAppUserTokenService>());
            app.UseWebApi(config);
            
            
        }
    }

    
}
