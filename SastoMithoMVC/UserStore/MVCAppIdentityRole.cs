using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;

namespace SastoMithoMVC.UserStore
{
    public class MVCAppIdentityRole<TKey, TUserRole>: IRole<TKey>
        where TKey : IEquatable<TKey>
        where TUserRole : MVCAppUserRole<TKey>
    {
        //
        // Summary:
        //     Constructor
        public MVCAppIdentityRole() { }

        //
        // Summary:
        //     Navigation property for users in the role
        public virtual ICollection<TUserRole> Users { get; }
        //
        // Summary:
        //     Role id
        public TKey Id { get; set; }
        //
        // Summary:
        //     Role name
        public string Name { get; set; }
    }
    public class MVCAppIdentityRole : MVCAppIdentityRole<Guid, MVCAppUserRole>   {
        
        public MVCAppIdentityRole()
        {
            Guid.NewGuid();
        }        
       
        public MVCAppIdentityRole(string roleName)
        {

            Name = roleName;
        }
    }
}
