using System.ComponentModel.DataAnnotations;

namespace CrombieProytecto_V0._1.Models.dtos
{
    public class LoginDto
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
