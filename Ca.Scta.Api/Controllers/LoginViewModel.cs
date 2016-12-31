using System.ComponentModel.DataAnnotations;

namespace Ca.Scta.Api.Controllers
{
    public class LoginViewModel
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }
    }
}