namespace CrombieProytecto_V0._1.Models
{
    public class WishListProductos : BaseEntity
    {
        public int IdWishList { get; set; }
        public int IdProducto { get; set; }
        public virtual WishList? WishList { get; set; }
        public virtual Producto? Producto { get; set; }
    }
}
