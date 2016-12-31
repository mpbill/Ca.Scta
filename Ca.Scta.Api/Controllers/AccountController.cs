using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using Ca.Scta.Account;
using Microsoft.AspNet.Identity;
using System.ComponentModel.DataAnnotations;
namespace Ca.Scta.Api.Controllers
{
    public interface IAccountController { }
    [RoutePrefix("Account")]
    public class AccountController : ApiController, IAccountController
    {
        private readonly UserManager<AppUser, int> _userManager;
        private readonly IAppUserTokenService _tokenService;

        public AccountController(
            UserManager<AppUser,int> userManager,
            IAppUserTokenService tokenService
            )
        {
            _userManager = userManager;
            _tokenService = tokenService;
        }

        [HttpPost]
        [Route("Create")]
        public async Task<IHttpActionResult> CreateAccount(CreateAccountViewModel viewModel)
        {
            if (ModelState.IsValid == false)
                return BadRequest(ModelState);
            var appUser = new AppUser
            {
                Email = viewModel.Email,
                EmailConfirmed = false,
                UserName = viewModel.UserName
            };
            var identityResult = await _userManager.CreateAsync(appUser, viewModel.Password);
            if (identityResult.Succeeded)
                return Ok();
            return BadRequest(string.Join(", ", identityResult.Errors));
        }
        [HttpPost]
        [Route("Login")]
        public async Task<IHttpActionResult> Login(LoginViewModel viewModel)
        {
            var user = await _userManager.FindByNameAsync(viewModel.UserName);
            if (user == null)
                return NotFound();
            var passwordVerified = await _userManager.CheckPasswordAsync(user, viewModel.Password);
            if (passwordVerified)
            {
                var token = _tokenService.CreateTokenAsync(user);
                var tokenResponse = new TokenResponse(token);
                return Ok(tokenResponse);
            }
            else
            {
                return Unauthorized();
            }
        }
        [Route("UserInfo")]
        [HttpGet]
        [Authorize]
        public async Task<IHttpActionResult> GetUserInfo()
        {
            return Ok();
        }

        [HttpGet, Route("AuthenticationTest"), Authorize]
        public async Task<IHttpActionResult> AuthenticationTest()
        {
            return Ok("You Are Authenticated");
        }
    }

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

    public class LoginViewModel
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }
    }

    public class CreateAccountViewModel
    {
        [Required]
        public string UserName { get; set; }
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        [Required, Compare(otherProperty: "Password", ErrorMessage = "Passwords Must Be The Same")]
        public string ConfirmPassword { get; set; }
    }
}
