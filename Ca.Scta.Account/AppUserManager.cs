using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;

namespace Ca.Scta.Account
{
    public class AppUserManager : UserManager<AppUser,int>
    {
        public AppUserManager(IUserStore<AppUser, int> store) : base(store)
        {
        }
    }
}
