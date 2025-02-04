using Proyect_Models.Models.Entidades;

namespace Proyect_Models.Models.Entidades
{
    public class ProductoCategoria
    {
        public int ProductoId { get; set; }
        public Producto Producto { get; set; }
        public int CategoriaId { get; set; }
        public Categoria Categoria { get; set; }
    }
}
