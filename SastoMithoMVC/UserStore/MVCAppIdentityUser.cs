using SastoMithoMVC.UserStore;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using SastoMithoClassLibrary.Models;

namespace SastoMithoMVC.UserStore
{
    public class MVCAppIdentityUser<TKey, TRole> :  UserModel, IUser<TKey>
        where TKey : IEquatable<TKey>
        where TRole : MVCAppUserRole<TKey>
        
       
    {
      
        //
        // Summary:
        //     True if the email is confirmed, default is false
        public virtual bool EmailConfirmed { get; set; }
 
       
        // Summary:
        //     A random value that should change whenever a users credentials have changed (password
        //     changed, login removed)
        public virtual string SecurityStamp { get; set; }
       
        // Summary:
        //     True if the phone number is confirmed, default is false
        public virtual bool PhoneNumberConfirmed { get; set; }
        
        // Summary:
        //     DateTime in UTC when lockout ends, any time in the past is considered not locked
        //     out.
        public virtual DateTimeOffset LockoutEndDateUtc { get; set; }
        //
        // Summary:
        //     Is lockout enabled for this user
        public virtual bool LockoutEnabled { get; set; }
        //
        // Summary:
        //     Used to record failures for the purposes of lockout
        public virtual int AccessFailedCount { get; set; }
        //
        // Summary:
        //     Navigation property for user roles
        public virtual ICollection<TRole> Role { get; }
        
        // Summary:
        //     User ID (Primary Key)
        public virtual TKey Id { get; set; }
        //
        // Summary:
        //     User name
        public virtual string UserName { get; set; }

        
    }
}
public class MVCAppIdentityUser : MVCAppIdentityUser<Guid, MVCAppUserRole>, IUser<Guid>
{
    //
    // Summary:
    //     Constructor which creates a new Guid for the Id
    public MVCAppIdentityUser()
    {
        Id = Guid.NewGuid();
    }
    //
    // Summary:
    //     Constructor that takes a userName
    //
    // Parameters:
    //   userName:
    public MVCAppIdentityUser(string userName)
    {
        UserName = userName;
    }
}
