using SastoMithoMVC.UserStore;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Dapper;
using System.Data;
using SastoMithoClassLibrary.Models;

namespace SastoMithoMVC.DataAccess
{
    public static class UserStoreDataAccess<TUser, TRole, TKey, TUserRole>
        where TUser : MVCAppIdentityUser<TKey, TUserRole>
        where TUserRole : MVCAppUserRole<TKey>
        where TRole : MVCAppIdentityRole<TKey, TUserRole>
        where TKey : IEquatable<TKey>
    {
        //using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(DataAccessHelper.CnnVal("Sample")))
        //{ 
        // await connection.Execute("dbo.People_Insert @FirstName, @LastName, @EmailAddress, @PhoneNumber", );    
        //}
        public static void SetPasswordAsync(TUser user, string Hash)
        {
            user.PasswordHash = Hash;   
        }
        public static void SetSecurityStamp(TUser user, string stamp)
        {
            user.SecurityStamp = stamp;
        }
        public static async Task<TUser> FindByNameAsync(string username)
        {
            using (IDbConnection connection = DataAccessHelper.Connection1())
            {


                try
                {
                    var output = await connection.QueryFirstAsync<TUser>("dbo.Users_FindByUserName @UserName", new { UserName = username });
                    return output;
                }
                catch (InvalidOperationException)
                {


                    return null;
                }
            }
        }
        public static string GetEmail(TUser user)
        {
            return user.EmailAddress;
        }
        public static async Task<TUser> FindByEmailAsync(string emailAddress)
        {
            using (IDbConnection connection = DataAccessHelper.Connection1())
            {
                

                try
                {
                    var output = await connection.QueryFirstAsync<TUser>("dbo.Users_GetByEmailAddress @EmailAddress", new { EmailAddress = emailAddress });
                    return output;
                }
                catch (InvalidOperationException)
                {


                    return null;
                }
            }
        }
        
        public static void SetLockoutEnabled(TUser user, bool enabled)
        {
            user.LockoutEnabled = enabled;
        }
        public static async Task CreateUserAsync(TUser user)
        {
            using (IDbConnection connection = DataAccessHelper.Connection1())
            {
                var tempuser = new { Id = user.Id, EmailAddress = user.EmailAddress, EmailConfirmed = user.EmailConfirmed, PasswordHash = user.PasswordHash, SecurityStamp = user.SecurityStamp, PhoneNumber = user.PhoneNumber, PhoneNumberConfirmed = user.PhoneNumberConfirmed, LockoutEndDateUtc = user.LockoutEndDateUtc, LockoutEnabled = user.LockoutEnabled, AccessFailedCount = user.AccessFailedCount, UserName = user.UserName, FirstName = user.FirstName, LastName = user.LastName };
                List<object> users = new List<object>
                {
                    tempuser
                };
                await connection.ExecuteAsync("dbo.Users_InsertUser @Id, @EmailAddress , @EmailConfirmed, @PasswordHash, @SecurityStamp, @PhoneNumber, @PhoneNumberConfirmed, @LockoutEndDateUtc, @LockoutEnabled , @AccessFailedCount, @UserName, @FirstName, @LastName", users);
            }
        }
        public static async Task<TUser> FindByIdAsync(TKey id)
        {
            using (IDbConnection connection = DataAccessHelper.Connection1())
            {
                try
                {
                    var output = await connection.QueryFirstAsync<TUser>("dbo.Users_FindById @Id", new { Id = id });
                    return output;
                }
                catch (InvalidOperationException)
                {


                    return null;
                }
            }
        }
        public static bool GetLockoutEnabled(TUser user)
        {
            return user.LockoutEnabled;
        }
        public static DateTimeOffset GetLockoutEndDate(TUser user)
        {
            return user.LockoutEndDateUtc;
        }
        public static string GetSecurityStamp(TUser user)
        {
            return user.SecurityStamp;
        }
        public static string GetPasswordHash(TUser user)
        {
            return user.PasswordHash;
        }
        public static int GetAccessFailedCount(TUser user)
        {
            return user.AccessFailedCount;
        }
        public static async Task<IList<string>> GetRolesAsync(TUser user)
        {
            using (IDbConnection connection = DataAccessHelper.Connection1())
            {
                try
                {
                    var output = await connection.QueryAsync<string>("dbo.Users_GetRoles @Id", new { Id = user.Id });
                    var finaloutput = new List<string>();
                    foreach (string item in output)
                    {
                        finaloutput.Add(item);
                    }
                    return finaloutput;
                }
                catch (InvalidOperationException)
                {


                    return null;
                }
            }
            
        }
        public static string GetPhoneNumber(TUser user)
        {
            return user.PhoneNumber;
        }
        public static bool GetPhoneNumberConfirmed(TUser user)
        {
            return user.PhoneNumberConfirmed;
        }
        public static bool HasPassword(TUser user)
        {
            if (user.PasswordHash != null) return true; else return false;
        }
        public static int IncrementAccessFailedCount(TUser user)
        {
            user.AccessFailedCount++;
            return user.AccessFailedCount;
        }
        public static void SetPhoneNumber(TUser user, string phonenumber)
        {
            user.PhoneNumber = phonenumber;
        }
        public static void SetPhoneNumberConfirmed(TUser user, bool confirmed)
        {
            user.PhoneNumberConfirmed = confirmed;
        }
        public static async Task UpdateUserAsync(TUser user)
        {
            using (IDbConnection connection = DataAccessHelper.Connection1())
            {
                var tempuser = new { Id = user.Id, EmailAddress = user.EmailAddress, EmailConfirmed = user.EmailConfirmed, PasswordHash = user.PasswordHash, SecurityStamp = user.SecurityStamp, PhoneNumber = user.PhoneNumber, PhoneNumberConfirmed = user.PhoneNumberConfirmed, LockoutEndDateUtc = user.LockoutEndDateUtc, LockoutEnabled = user.LockoutEnabled, AccessFailedCount = user.AccessFailedCount, UserName = user.UserName, FirstName = user.FirstName, LastName=user.LastName };
                List<object> users = new List<object>
                {
                    tempuser
                };
                await connection.ExecuteAsync("dbo.Users_UpdateUser @Id, @EmailAddress , @EmailConfirmed, @PasswordHash, @SecurityStamp, @PhoneNumber, @PhoneNumberConfirmed, @LockoutEndDateUtc, @LockoutEnabled , @AccessFailedCount, @UserName, @FirstName, @LastName", users);
            }
        }
        public static void SetLockoutEndDate(TUser user, DateTimeOffset lockoutEnd)
        {
            user.LockoutEndDateUtc = lockoutEnd;
        }
        public static void ResetAccessFailedCount(TUser user)
        {
            user.AccessFailedCount = 0;
        }
        public static async Task AddtoRoleAsync(TUser user, string rolename)
        {
            using (IDbConnection connection = DataAccessHelper.Connection1())
            {
                var tempuser = new { Id = user.Id, RoleName = rolename };
                List<object> users = new List<object>
                {
                    tempuser
                };
                await connection.ExecuteAsync("dbo.Users_AddtoRole @Id, @RoleName", users);
            }
        }
        public static async Task<IList<UserLoginInfo>> GetLogins(TUser user)
        {
            using (IDbConnection connection = DataAccessHelper.Connection1())
            {
                try
                {
                    IEnumerable<UserLoginInfo> logins;

                    logins = await connection.QueryAsync<UserLoginInfo>("dbo.Users_GetLogins @Id", new { Id = user.Id });
                    var finaloutput = new List<UserLoginInfo>();
                    foreach (UserLoginInfo item in logins)
                    {
                        finaloutput.Add(item);
                    }
                    return finaloutput;
                }
                catch (InvalidOperationException)
                {


                    return null;
                }
            }

        }
        public static async Task<TUser> FindUserwithLoginAsync(UserLoginInfo login)
        {
            using (IDbConnection connection = DataAccessHelper.Connection1())
            {
                try
                {
                   

                    var user = await connection.QueryFirstAsync<TUser>("dbo.Users_FindUserwithLoginAsync @LoginProvider, @ProviderKey", new { LoginProvider = login.LoginProvider, ProviderKey = login.ProviderKey });
                    return user;
                   
                }
                catch (InvalidOperationException)
                {


                    return null;
                }
            }

        }
        public static async Task AddLoginAsync(TUser user, UserLoginInfo login)
        {
            using (IDbConnection connection = DataAccessHelper.Connection1())
            {
                var tempuser = new { Id = user.Id, LoginProvider = login.LoginProvider, ProviderKey = login.ProviderKey };
                List<object> users = new List<object>
                {
                    tempuser
                };
                await connection.ExecuteAsync("dbo.Users_AddLogin @Id, @LoginProvider, @ProviderKey", users);
            }
        }
    }

}