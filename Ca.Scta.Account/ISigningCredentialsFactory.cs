using Microsoft.IdentityModel.Tokens;

namespace Ca.Scta.Account
{
    public interface ISigningCredentialsFactory
    {
        SecurityKey GetKey();
        SigningCredentials GetSigningCredentials();
    }
}