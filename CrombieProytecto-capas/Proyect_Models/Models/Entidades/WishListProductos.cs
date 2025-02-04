namespace Proyect_Models.Models.Entidades
{
    public class WishListProductos : BaseEntity
    {
        public int IdWishList { get; set; }
        public int IdProducto { get; set; }
        public virtual WishList? WishList { get; set; }
        public virtual Producto? Producto { get; set; }
    }
}
