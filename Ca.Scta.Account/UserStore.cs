using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ca.Scta.Dal.Cqrs;
using Ca.Scta.Dal.Cqrs.AppUser;
using Ca.Scta.Dal.Cqrs.AppUser.Commands;
using Ca.Scta.Dal.Cqrs.AppUser.Queries;
using Ca.Scta.Dal.Cqrs.Base;
using Ca.Scta.Dal.Models;
using Microsoft.AspNet.Identity;

namespace Ca.Scta.Account
{
    public class AppUserStore : IUserStore<AppUser,int>, IUserPasswordStore<AppUser, int>, IUserSecurityStampStore<AppUser,int>,IUserEmailStore<AppUser, int>
    {
        private readonly IQueryHandler<GetAppUserByEmailQuery,AppUserModel> _getApUserByEmailQueryHandler;
        private readonly IQueryHandler<GetAppUserByIdQuery, AppUserModel> _getAppUserByIdQueryHandler;
        private readonly IQueryHandler<GetAppUserByUserNameQuery, AppUserModel> _getAppUserByUserNameQueryHandler;
        private readonly ICommandHandler<DeleteAppUserCommand, bool> _deleteAppUserCommandHandler;
        private readonly ICommandHandler<AddAppUserCommand, int> _addAppUserCommandHandler;
        private readonly ICommandHandler<UpdateAppUserCommand, bool> _updateAppUserCommandHandler;
        private readonly Task<bool> _compleetedTask;

        public AppUserStore(
            DapperQueryHandler<GetAppUserByEmailQuery,AppUserModel> getApUserByEmailQueryHandler,
            DapperQueryHandler<GetAppUserByIdQuery,AppUserModel> getAppUserByIdQueryHandler,
            DapperQueryHandler<GetAppUserByUserNameQuery,AppUserModel> getAppUserByUserNameQueryHandler,
            DapperCommandHandler<DeleteAppUserCommand,bool> deleteAppUserCommandHandler,
            DapperCommandHandler<AddAppUserCommand,int> addAppUserCommandHandler,
            DapperCommandHandler<UpdateAppUserCommand,bool> updateAppUserCommandHandler)
        {
            _getApUserByEmailQueryHandler = getApUserByEmailQueryHandler;
            _getAppUserByIdQueryHandler = getAppUserByIdQueryHandler;
            _getAppUserByUserNameQueryHandler = getAppUserByUserNameQueryHandler;
            _deleteAppUserCommandHandler = deleteAppUserCommandHandler;
            _addAppUserCommandHandler = addAppUserCommandHandler;
            _updateAppUserCommandHandler = updateAppUserCommandHandler;
            _compleetedTask = Task.FromResult(true);
        }
        public void Dispose()
        {
            //nothing to dispose of.  
        }

        public async Task CreateAsync(AppUser user)
        {
            var command = new AddAppUserCommand(
                user.UserName,
                user.PasswordHash,
                user.SecurityStamp,
                user.Email,
                user.EmailConfirmed);
            int newId = await _addAppUserCommandHandler.HandleAsync(command);
            if (newId == 0)
            {
                //todo: add 0 id check and logging.  maybe exception?
            }
            else
            {
                //not sure if i should set this here...
                user.Id = newId;
            }
            
            
        }

        public async Task UpdateAsync(AppUser user)
        {
            var command = new UpdateAppUserCommand(
                user.Id,
                user.UserName,
                user.PasswordHash,
                user.SecurityStamp,
                user.Email,
                user.EmailConfirmed
                );
            var success = await _updateAppUserCommandHandler.HandleAsync(command);
            if (!success)
            {
                //todo: add success check and logging.
            }
        }

        public async Task DeleteAsync(AppUser user)
        {
            var command = new DeleteAppUserCommand(user.Id);
            var success = await _deleteAppUserCommandHandler.HandleAsync(command);
            if (!success)
            {
                //todo: add success check and logging.
            }


        }

        public async Task<AppUser> FindByIdAsync(int userId)
        {
            var query = new GetAppUserByIdQuery(userId);
            var appUserModel = await _getAppUserByIdQueryHandler.HandleAsync(query);
            var appUser = ToAppUser(appUserModel);
            return appUser;
        }

        public async Task<AppUser> FindByNameAsync(string userName)
        {
            var query = new GetAppUserByUserNameQuery(userName);
            var appUserModel = await _getAppUserByUserNameQueryHandler.HandleAsync(query);
            var appUser = ToAppUser(appUserModel);
            return appUser;
        }

        public Task SetPasswordHashAsync(AppUser user, string passwordHash)
        {
            user.PasswordHash = passwordHash;
            return _compleetedTask;
        }

        public Task<string> GetPasswordHashAsync(AppUser user)
        {
            return Task.FromResult(user.PasswordHash);
        }

        public Task<bool> HasPasswordAsync(AppUser user)
        {
            var hasPassword = string.IsNullOrEmpty(user.PasswordHash) == false;
            return _compleetedTask;
        }

        public Task SetSecurityStampAsync(AppUser user, string stamp)
        {
            user.SecurityStamp = stamp;
            return _compleetedTask;

        }

        public Task<string> GetSecurityStampAsync(AppUser user)
        {
            return Task.FromResult(user.SecurityStamp);
        }

        public Task SetEmailAsync(AppUser user, string email)
        {
            user.Email = email;
            return _compleetedTask;
        }

        public Task<string> GetEmailAsync(AppUser user)
        {
            return Task.FromResult(user.Email);
        }

        public Task<bool> GetEmailConfirmedAsync(AppUser user)
        {
            return Task.FromResult(user.EmailConfirmed);
        }

        public Task SetEmailConfirmedAsync(AppUser user, bool confirmed)
        {
            user.EmailConfirmed = confirmed;
            return _compleetedTask;
        }

        public async Task<AppUser> FindByEmailAsync(string email)
        {
            
            var query = new GetAppUserByEmailQuery(email);
            var unMapped = await _getApUserByEmailQueryHandler.HandleAsync(query);
            //todo:maybe add null check.
            var mapped = ToAppUser(unMapped);
            return mapped;
        }

        private AppUser ToAppUser(AppUserModel appUserModel)
        {
            if(appUserModel==null)
                return null;
            var mapped = new AppUser
            {
                Email = appUserModel.Email,
                SecurityStamp = appUserModel.SecurityStamp,
                PasswordHash = appUserModel.PasswordHash,
                EmailConfirmed = appUserModel.EmailConfirmed,
                UserName = appUserModel.UserName,
                Id = appUserModel.Id
            };
            return mapped;
            
        }
        private AppUserModel ToAppUserModel(AppUser appUser)
        {
            if (appUser == null)
                return null;
            var mapped = new AppUserModel
            {
                Email = appUser.Email,
                SecurityStamp = appUser.SecurityStamp,
                PasswordHash = appUser.PasswordHash,
                EmailConfirmed = appUser.EmailConfirmed,
                UserName = appUser.UserName,
                Id = appUser.Id
            };
            return mapped;

        }
    }
}
