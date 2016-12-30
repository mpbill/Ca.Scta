using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Ca.Scta.Account.Tests
{
    [TestClass]
    public class TokenTests
    {
        [TestInitialize]
        public void Initialize()
        {
            

        }
        [TestMethod]
        public void CreatedTokenShouldValidate()
        {
            var user = new AppUser();
            user.Email = "example@example.com";
            user.EmailConfirmed = true;
            user.Id = 2;
            user.PasswordHash = "ThisIsAPasswordHash";
            user.SecurityStamp = "ThisIsASecurityStamp";
            user.UserName = "ThisIsAUserName";
            var claimsIdentityFactory = new AppUserClaimsIdentityFactory();
            var signingCredentialsFactory = new SigningCredentialsFactory();
            var tokenFactory = new AppUserTokenService(claimsIdentityFactory, signingCredentialsFactory);
            var token = tokenFactory.CreateTokenAsync(user);
            var result = tokenFactory.ValidateToken(token);
            Assert.IsTrue(result.IsValid);
        }
    }
}
