using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SastoMithoMVC.UserStore
{
    public interface IMVCUserStore<TUser> : IUserStore<TUser, Guid>, IDisposable where TUser : class, IUser<Guid>
    {
        
    }
    
}
