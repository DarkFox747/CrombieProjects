namespace CrombieProytecto_V0._2.Models.Entidades
{
    public class Producto : BaseEntity
    {
        public required string Nombre { get; set; }
        public string? Descripcion { get; set; }
        public decimal Precio { get; set; }
        public int Stock { get; set; }
        public string? URL { get; set; }
        public virtual ICollection<WishListProductos>? WishListProductos { get; set; }
        public virtual ICollection<ProductoCategoria> ProductoCategorias { get; set; }
    }

}
