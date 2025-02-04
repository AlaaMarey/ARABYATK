using System.ComponentModel.DataAnnotations;

namespace ARABYTAK.APIS.DTOs
{
    public class SignInUserDto
    {

        [Required]
        public string Emial { get; set; }

        [Required]
        //[RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d).{8,}$")]
        public string Password { get; set; }

        [Required]
        [Compare("Password")]
        public string ConfirmPassword { get; set; }

    }
}
