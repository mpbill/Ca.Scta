using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web.Http;
using Ca.Scta.Account;
using Ca.Scta.Api.Controllers.Account.Models;
using Microsoft.AspNet.Identity;
using TokenResponse = Ca.Scta.Api.Controllers.Account.Models.TokenResponse;
using UserInfoViewModel = Ca.Scta.Api.Controllers.Account.Models.UserInfoViewModel;

namespace Ca.Scta.Api.Controllers.Account
{
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

            var claimsPrincipal = User as ClaimsPrincipal;
            int id = 0;
            if (claimsPrincipal != null) 
                Int32.TryParse(claimsPrincipal.Claims.FirstOrDefault(claim => claim.Type=="UserId")?.Value,out id);
            if (id == 0)
                return NotFound();
            var appUser = await _userManager.FindByIdAsync(id);
            if (appUser == null)
                return NotFound();
            
            var userInfo = new UserInfoViewModel(appUser);
            
            return Ok(userInfo);
        }
    }
}
