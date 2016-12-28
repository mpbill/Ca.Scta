using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ca.Scta.Dal.Cqrs.Base;

namespace Ca.Scta.Dal.Cqrs
{
    public class AddAppUser : ICommand
    {
        public AddAppUser(string userName, string passwordHash, string securityStamp, string email, bool emailConfirmed)
        {
            UserName = userName;
            PasswordHash = passwordHash;
            SecurityStamp = securityStamp;
            Email = email;
            EmailConfirmed = emailConfirmed;
        }
        
        public string UserName { get; private set; }
        public string PasswordHash { get; private set; }
        public string SecurityStamp { get; private set; }
        public string Email { get; private set; }
        public bool EmailConfirmed { get; private set; }
    }
    public class 
}
