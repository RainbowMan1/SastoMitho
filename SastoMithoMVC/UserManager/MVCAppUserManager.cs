using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using SastoMithoMVC.UserStore;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security.DataProtection;

namespace SastoMithoMVC.UserManager
{
    public class MVCAppParentUserManager<TUser, TKey>: UserManager<TUser, TKey> where TUser : class, IUser<TKey>
        where TKey : IEquatable<TKey>
    {
        public MVCAppParentUserManager(IUserStore<TUser, TKey> store) : base (store)
        {
            Store = store;
        }
       

    }
    public class MVCAppUserManager<TUser> : MVCAppParentUserManager<TUser,Guid> where TUser : class, IUser<Guid>
    {
        public MVCAppUserManager(IMVCUserStore<TUser> store) : base (store)
        {

        }
       
      
        

    }
    public class MVCAppUserValidator<TUser>: UserValidator<TUser, Guid> where TUser : class, IUser<Guid>
    {
        public MVCAppUserValidator(UserManager<TUser, Guid> manager): base(manager)
        {

        }
    }
    public class MVCAppDataProtectorTokenProvider<TUser> : DataProtectorTokenProvider<TUser, Guid> where TUser : class, IUser<Guid>
    {
        //
        // Summary:
        //     Constructor
        //
        // Parameters:
        //   protector:
        public MVCAppDataProtectorTokenProvider(IDataProtector protector) : base(protector) { }
    }
    public class MVCAppPhoneNumberTokenProvider<TUser> : PhoneNumberTokenProvider<TUser, Guid> where TUser : class, IUser<Guid>
    {
        public MVCAppPhoneNumberTokenProvider()
        {

        }
    }
}
  