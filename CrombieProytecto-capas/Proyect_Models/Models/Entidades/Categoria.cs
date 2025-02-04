using Proyect_Models.Models.Entidades;

namespace Proyect_Models.Models.Entidades
{
    public class Categoria : BaseEntity
    {
        public string Nombre { get; set; }
        public ICollection<ProductoCategoria> ProductoCategorias { get; set; }
    }
}
