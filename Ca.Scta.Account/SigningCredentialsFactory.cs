using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace Ca.Scta.Account
{
    public class SigningCredentialsFactory : ISigningCredentialsFactory
    {
        private readonly SecurityKey _key;
        public SigningCredentialsFactory()
        {
            var keyPhrase = "ThisIsTheKeyPhrase";
            var keyBytes = Encoding.Unicode.GetBytes(keyPhrase);
            _key = new SymmetricSecurityKey(keyBytes);
        }

        public SecurityKey GetKey()
        {
            return _key;
        }
        public SigningCredentials GetSigningCredentials()
        {
            var credentials = new SigningCredentials(_key,SecurityAlgorithms.HmacSha256);
            return credentials;
        }
    }
}