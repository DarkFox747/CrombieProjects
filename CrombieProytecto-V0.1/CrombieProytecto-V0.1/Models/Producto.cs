
namespace CrombieProytecto_V0._1.Models
{
    public class Producto : BaseEntity
    {
        public required string Nombre { get; set; }
        public string? Descripcion { get; set; }
        public decimal Precio { get; set; }
        public int Stock { get; set; }
        public virtual ICollection<WishListProductos>? WishListProductos { get; set; }

    }

}
