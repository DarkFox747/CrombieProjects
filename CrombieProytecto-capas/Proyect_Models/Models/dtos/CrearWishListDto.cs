using System.ComponentModel.DataAnnotations;

namespace Proyect_Models.Models.dtos
{
    public class CrearWishListDto
    {
        [Required]
        [StringLength(100)]
        public string Nombre { get; set; }
    }
}
