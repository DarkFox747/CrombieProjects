namespace CrombieProytecto_V0._2.Models.Entidades
{
    public class ProductoCategoria
    {
        public int ProductoId { get; set; }
        public Producto Producto { get; set; }
        public int CategoriaId { get; set; }
        public Categoria Categoria { get; set; }
    }
}
