using System.ComponentModel.DataAnnotations;

namespace Proyect_Models.Models.dtos
{
    public class CrearProductoDto
    {
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public decimal Precio { get; set; }
        public int Stock { get; set; }
        public List<int> CategoriaIds { get; set; } = new List<int>();
    }
}
