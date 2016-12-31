namespace Ca.Scta.Account
{
    public class TokenResponse
    {
        public TokenResponse(string token)
        {
            Token = token;
        }

        public TokenResponse()
        {

        }
        public string Token { get; set; }

    }
}