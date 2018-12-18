using System;

namespace SastoMithoMVC.UserStore
{
    public class MVCAppUserRole<TKey> where TKey : IEquatable<TKey>
    {
        public MVCAppUserRole()
        {



        }

        //
        // Summary:
        //     UserId for the user that is in the role
        public virtual TKey UserId { get; set; }
        //
        // Summary:
        //     RoleId for the role
        public virtual TKey RoleId { get; set; }
    }
    public class MVCAppUserRole : MVCAppUserRole<Guid>
    {
        public MVCAppUserRole()

        {

        }
    }
}