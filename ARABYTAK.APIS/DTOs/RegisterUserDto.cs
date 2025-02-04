using System.ComponentModel.DataAnnotations;

namespace ARABYTAK.APIS.DTOs
{
    public class RegisterUserDto
    {

        [Required]
        public string Name { get; set; }

        [Required]
        public string Email { get; set; }
        [Required]
        // [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d).{8,}$")]
        public string Password { get; set; }

    }
}
