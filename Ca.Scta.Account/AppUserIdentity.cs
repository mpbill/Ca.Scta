using System.Security.Principal;

namespace Ca.Scta.Account
{
    public class AppUserIdentity : IIdentity
    {
        public AppUserIdentity(string userName)
        {
            Name = userName;
            AuthenticationType = "password";
            IsAuthenticated = true;
        }
        public string Name { get; private set; }
        public string AuthenticationType { get; private set; }
        public bool IsAuthenticated { get; private set; }
    }
}