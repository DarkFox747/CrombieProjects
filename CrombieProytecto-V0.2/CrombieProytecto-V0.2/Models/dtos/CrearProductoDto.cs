using System.ComponentModel.DataAnnotations;

namespace CrombieProytecto_V0._2.Models.dtos
{
    public class CrearProductoDto
    {
        [Required]
        [StringLength(200)]
        public string Nombre { get; set; }

        [StringLength(1000)]
        public string Descripcion { get; set; }

        [Required]
        [Range(0.01, double.MaxValue)]
        public decimal Precio { get; set; }

        [Required]
        [Range(0, int.MaxValue)]
        public int Stock { get; set; }
    }
}
