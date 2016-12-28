using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.
using Microsoft.AspNet.Identity;

namespace Ca.Scta.Account
{
    public class AppUser : IUser<int>
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string SecurityStamp { get; set; }
        public string PasswordHash { get; set; }
        public string Email { get; set; }
        public bool EmailConfirmed { get; set; }
    }
}
