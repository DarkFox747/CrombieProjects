using System.ComponentModel.DataAnnotations;

namespace CrombieProytecto_V0._1.Models.dtos
{
    public class CrearWishListDto
    {
        [Required]
        [StringLength(100)]
        public string Nombre { get; set; }
    }
}
