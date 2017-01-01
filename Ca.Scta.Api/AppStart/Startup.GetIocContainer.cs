using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ca.Scta.Account;
using Ca.Scta.Api.Controllers.Account;
using Ca.Scta.Dal.Connection;
using Ca.Scta.Dal.Cqrs.AppUser.Commands;
using Ca.Scta.Dal.Cqrs.AppUser.Queries;
using Ca.Scta.Dal.Cqrs.Base;
using Ca.Scta.Dal.Models;
using Microsoft.AspNet.Identity;
using SimpleInjector;
using SimpleInjector.Integration.WebApi;

namespace Ca.Scta.Api.AppStart
{
    public partial class Startup
    {
        public Container GetIocContainer()
        {
            var container = new Container();
            container.Options.DefaultScopedLifestyle=new WebApiRequestLifestyle();
            container.Register<UserManager<AppUser, int>,AppUserManager>(Lifestyle.Scoped);
            
            //controllers for test
            container.Register<IAccountController,AccountController>(Lifestyle.Scoped);
            container.Register<IAppUserClaimsIdentityFactory,AppUserClaimsIdentityFactory>(Lifestyle.Singleton);
            container.Register<ISigningCredentialsFactory,SigningCredentialsFactory>(Lifestyle.Singleton);
            container.Register<IAppUserTokenService,AppUserTokenService>(Lifestyle.Singleton);

            container.Register<DapperQueryHandler<GetAppUserByEmailQuery,AppUserModel>,GetAppUserByEmailQueryHandler>(Lifestyle.Scoped);
            container.Register<DapperQueryHandler<GetAppUserByIdQuery,AppUserModel>,GetAppUserByIdQueryHandler>(Lifestyle.Scoped);
            container.Register<DapperQueryHandler<GetAppUserByUserNameQuery,AppUserModel>,GetAppUserByUserNameQueryHandler>(Lifestyle.Scoped);
            container.Register<DapperCommandHandler<AddAppUserCommand,int>,AddAppUserCommandHandler>(Lifestyle.Scoped);
            container.Register<DapperCommandHandler<DeleteAppUserCommand,bool>,DeleteAppUserCommandHandler>(Lifestyle.Scoped);
            container.Register<DapperCommandHandler<UpdateAppUserCommand,bool>,UpdateAppUserCommandHandler>(Lifestyle.Scoped);
            container.Register<IUserStore<AppUser, int>, AppUserStore>(Lifestyle.Scoped);
            container.Register<IDbConnectionAsyncFactory,DbConnectionAsyncFactory>(Lifestyle.Singleton);
            
            container.Verify();
            return container;
        }
    }
}
