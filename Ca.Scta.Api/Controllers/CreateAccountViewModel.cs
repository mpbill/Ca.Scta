using System.ComponentModel.DataAnnotations;

namespace Ca.Scta.Api.Controllers
{
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