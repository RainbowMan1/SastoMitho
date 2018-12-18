using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SastoMithoMVC.DataAccess;

namespace SastoMithoMVC.UserStore
{
    public class MVCAppUserStore<TUser, TKey, TUserRole, TRole> : IUserLoginStore<TUser, TKey>, IUserTwoFactorStore<TUser,TKey>, IUserRoleStore<TUser, TKey>, IUserPasswordStore<TUser, TKey>, IUserSecurityStampStore<TUser, TKey>, IUserEmailStore<TUser, TKey>, IUserPhoneNumberStore<TUser, TKey>, IUserLockoutStore<TUser, TKey>, IUserStore<TUser, TKey>, IDisposable, IRoleStore<TRole, TKey>
        where TUser : MVCAppIdentityUser<TKey, TUserRole>
        where TUserRole : MVCAppUserRole<TKey>
        where TRole : MVCAppIdentityRole<TKey, TUserRole>, IRole <TKey>
        where TKey : IEquatable<TKey>


    {
        public async Task AddLoginAsync(TUser user, UserLoginInfo login)
        {
            await Task.Run(() => UserStoreDataAccess<TUser, TRole, TKey, TUserRole>.AddLoginAsync(user, login));
        }

        public async Task AddToRoleAsync(TUser user, string roleName)
        {
            await Task.Run(() => UserStoreDataAccess<TUser, TRole, TKey, TUserRole>.AddtoRoleAsync(user,roleName));
        }

        public async Task CreateAsync(TUser user)
        {
            await Task.Run(() => UserStoreDataAccess<TUser, TRole, TKey, TUserRole>.CreateUserAsync(user));
        }

        public Task CreateAsync(TRole role)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(TUser user)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(TRole role)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            
        }

        public async Task<TUser> FindAsync(UserLoginInfo login)
        {
            return await Task.Run(() => UserStoreDataAccess<TUser, TRole, TKey, TUserRole>.FindUserwithLoginAsync(login));
        }

        public async Task<TUser> FindByEmailAsync(string email)
        {
            TUser user =  await Task.Run(() => UserStoreDataAccess<TUser, TRole, TKey, TUserRole>.FindByEmailAsync(email));
            return user;
        }

        public async Task<TUser> FindByIdAsync(TKey userId)
        {
            TUser user = await Task.Run(() => UserStoreDataAccess<TUser, TRole, TKey, TUserRole>.FindByIdAsync(userId));
            return user;
        }

        public async Task<TUser> FindByNameAsync(string userName)
        {
            TUser user = await UserStoreDataAccess<TUser, TRole, TKey, TUserRole>.FindByNameAsync(userName);
            return user;
        }

        public async Task<int> GetAccessFailedCountAsync(TUser user)
        {
            int count = await Task.Run(() => UserStoreDataAccess<TUser, TRole, TKey, TUserRole>.GetAccessFailedCount(user));
            return count;
        }

        public async Task<string> GetEmailAsync(TUser user)
        {
            string Email = await Task.Run(() => UserStoreDataAccess<TUser, TRole, TKey, TUserRole>.GetEmail(user));
            return Email;
        }

        public Task<bool> GetEmailConfirmedAsync(TUser user)
        {
            throw new NotImplementedException();
        }


        public async Task<bool> GetLockoutEnabledAsync(TUser user)
        {
            bool enabled = await Task.Run(() => UserStoreDataAccess<TUser, TRole, TKey, TUserRole>.GetLockoutEnabled(user));
            return enabled;
        }

        public async Task<DateTimeOffset> GetLockoutEndDateAsync(TUser user)
        {
            DateTimeOffset enddate = await Task.Run(() => UserStoreDataAccess<TUser, TRole, TKey, TUserRole>.GetLockoutEndDate(user));
            return enddate;
        }

        public async Task<IList<UserLoginInfo>> GetLoginsAsync(TUser user)
        {
            IList<UserLoginInfo> loginInfo = await Task.Run(() => UserStoreDataAccess<TUser, TRole, TKey, TUserRole>.GetLogins(user));
            return loginInfo;
        }

        public async Task<string> GetPasswordHashAsync(TUser user)
        {
            string hash = await Task.Run(() => UserStoreDataAccess<TUser, TRole, TKey, TUserRole>.GetPasswordHash(user));
            return hash;
        }

        public async Task<string> GetPhoneNumberAsync(TUser user)
        {
            string phonenumber = await Task.Run(() => UserStoreDataAccess<TUser, TRole, TKey, TUserRole>.GetPhoneNumber(user));
            return phonenumber;
        }

        public async Task<bool> GetPhoneNumberConfirmedAsync(TUser user)
        {
            bool confirmed = await Task.Run(() => UserStoreDataAccess<TUser, TRole, TKey, TUserRole>.GetPhoneNumberConfirmed(user));
            return confirmed;
        }

        public async Task<IList<string>> GetRolesAsync(TUser user)
        {
            IList<string> roles = await Task.Run(() => UserStoreDataAccess<TUser, TRole, TKey, TUserRole>.GetRolesAsync(user));
            return roles;
        }

        public async Task<string> GetSecurityStampAsync(TUser user)
        {
            string stamp = await Task.Run(() => UserStoreDataAccess<TUser, TRole, TKey, TUserRole>.GetSecurityStamp(user));
            return stamp;
        }

        public async Task<bool> GetTwoFactorEnabledAsync(TUser user)
        {
            return false;
        }

        public async Task<bool> HasPasswordAsync(TUser user)
        {
            bool haspassword = await Task.Run(() => UserStoreDataAccess<TUser, TRole, TKey, TUserRole>.HasPassword(user));
            return haspassword;
        }

        public async Task<int> IncrementAccessFailedCountAsync(TUser user)
        {
            int accesscount = await Task.Run(() => UserStoreDataAccess<TUser, TRole, TKey, TUserRole>.IncrementAccessFailedCount(user));
            return accesscount;
        }

 
        public Task<bool> IsInRoleAsync(TUser user, string roleName)
        {
            throw new NotImplementedException();
        }

 
        public Task RemoveFromRoleAsync(TUser user, string roleName)
        {
            throw new NotImplementedException();
        }

        public Task RemoveLoginAsync(TUser user, UserLoginInfo login)
        {
            throw new NotImplementedException();
        }

        public async Task ResetAccessFailedCountAsync(TUser user)
        {
            await Task.Run(() => UserStoreDataAccess<TUser, TRole, TKey, TUserRole>.ResetAccessFailedCount(user));
        }

        public Task SetEmailAsync(TUser user, string email)
        {
            throw new NotImplementedException();
        }

        public Task SetEmailConfirmedAsync(TUser user, bool confirmed)
        {
            throw new NotImplementedException();
        }

        public async Task SetLockoutEnabledAsync(TUser user, bool enabled)
        {
            await Task.Run(() => UserStoreDataAccess<TUser, TRole, TKey, TUserRole>.SetLockoutEnabled(user, enabled));
        }

        public async Task SetLockoutEndDateAsync(TUser user, DateTimeOffset lockoutEnd)
        {
            await Task.Run(() => UserStoreDataAccess<TUser, TRole, TKey, TUserRole>.SetLockoutEndDate(user, lockoutEnd));
        }

        public async Task SetPasswordHashAsync(TUser user, string passwordHash)
        {
          await Task.Run(() => UserStoreDataAccess<TUser, TRole, TKey, TUserRole>.SetPasswordAsync(user, passwordHash));
        }

        public async Task SetPhoneNumberAsync(TUser user, string phoneNumber)
        {
            await Task.Run(() => UserStoreDataAccess<TUser, TRole, TKey, TUserRole>.SetPhoneNumber(user, phoneNumber));
        }

        public async Task SetPhoneNumberConfirmedAsync(TUser user, bool confirmed)
        {
            await Task.Run(() => UserStoreDataAccess<TUser, TRole, TKey, TUserRole>.SetPhoneNumberConfirmed(user, confirmed));
        }

        public async Task SetSecurityStampAsync(TUser user, string stamp)
        {
            await Task.Run(() => UserStoreDataAccess<TUser, TRole, TKey, TUserRole>.SetSecurityStamp(user, stamp));
        }

        public Task SetTwoFactorEnabledAsync(TUser user, bool enabled)
        {
            throw new NotImplementedException();
        }

        public async Task UpdateAsync(TUser user)
        {
            await Task.Run(() => UserStoreDataAccess<TUser, TRole, TKey, TUserRole>.UpdateUserAsync(user));
        }

        public Task UpdateAsync(TRole role)
        {
            throw new NotImplementedException();
        }

        Task<TRole> IRoleStore<TRole, TKey>.FindByIdAsync(TKey roleId)
        {
            throw new NotImplementedException();
        }

        Task<TRole> IRoleStore<TRole, TKey>.FindByNameAsync(string roleName)
        {
            throw new NotImplementedException();
        }
    }

    public class MVCAppUserStore<TUser> : MVCAppUserStore<TUser, Guid, MVCAppUserRole, MVCAppIdentityRole>, IMVCUserStore<TUser>, IUserStore<TUser, Guid>, IDisposable where TUser : MVCAppIdentityUser
    {
        public MVCAppUserStore()
        {

        }
    }
}