using System.ComponentModel.DataAnnotations;

namespace CrombieProytecto_V0._1.Models.dtos
{
    public class RegistroUsuarioDto
    {
        [Required]
        [StringLength(50)]
        public string Nombre { get; set; }

        [Required]
        [StringLength(50)]
        public string Username { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [MinLength(6)]
        public string Password { get; set; }
    }
}
