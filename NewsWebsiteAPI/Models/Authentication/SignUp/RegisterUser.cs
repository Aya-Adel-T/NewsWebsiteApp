using System.ComponentModel.DataAnnotations;

namespace NewsWebsiteAPI.Models.Authentication.SignUp
{
    public class RegisterUser
    {
        [Required(ErrorMessage ="User Name Is Required")]
        public string? UserName { get; set; }
        [Required(ErrorMessage = "Email Is Required")]
        public string? Email { get; set; }
        [Required(ErrorMessage = "Password Is Required")]
        public string? Password { get; set; }
    }
}
