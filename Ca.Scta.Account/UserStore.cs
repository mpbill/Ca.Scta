using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ca.Scta.Dal.Cqrs;
using Ca.Scta.Dal.Cqrs.Base;
using Microsoft.AspNet.Identity;

namespace Ca.Scta.Account
{
    
    class AppUserStore : IUserStore<AppUser,int>, IUserPasswordStore<AppUser, int>, IUserSecurityStampStore<AppUser,int>,IUserEmailStore<AppUser, int>
    {
        private readonly IQueryHandler<GetAppUserByEmailQuery, Dal.Models.AppUserModel> _getApUserByEmailQueryHandler;
        private Task<bool> _compleetedTask;

        public AppUserStore(IQueryHandler<GetAppUserByEmailQuery,Ca.Scta.Dal.Models.AppUserModel> getApUserByEmailQueryHandler)
        {
            _getApUserByEmailQueryHandler = getApUserByEmailQueryHandler;
            _compleetedTask = Task.FromResult(true);
        }
        public void Dispose()
        {
            
        }

        public Task CreateAsync(AppUser user)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(AppUser user)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(AppUser user)
        {
            throw new NotImplementedException();
        }

        public Task<AppUser> FindByIdAsync(int userId)
        {
            throw new NotImplementedException();
        }

        public Task<AppUser> FindByNameAsync(string userName)
        {
            return 
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
            var mapped = ToIAppUser(unMapped);
            return mapped;
        }

        private AppUser ToIAppUser(Ca.Scta.Dal.Models.AppUserModel appUserModel)
        {
            if(appUserModel==null)
                return null;
            var mapped = new AppUser
            {
                Email = appUserModel.Email,
                SecurityStamp = appUserModel.SecurityStamp,
                PasswordHash = appUserModel.PasswordHash,
                EmailConfirmed = appUserModel.EmailConfirmed,
                UserName = appUserModel.UserName
            };
            return mapped;
            
        }
    }
}
