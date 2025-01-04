using System.ComponentModel.DataAnnotations;

namespace CrombieProytecto_V0._1.Models
{
    public class Usuario
    {
        [Key]
        public int Id { get; set; }

        [MaxLength(50)]
        public string Nombre { get; set; }

        [MaxLength(100)]
        public string Email { get; set; }
        public string Password { get; set; }


    }
}
