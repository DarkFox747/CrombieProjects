namespace CrombieProytecto_V0._2.Models.Entidades
{
    public class WishList : BaseEntity
    {
        public required string Nombre { get; set; }
        public int IdUsuario { get; set; }
        public virtual Usuario Usuario { get; set; }
        public virtual ICollection<WishListProductos>? Productos { get; set; }
    }
}
