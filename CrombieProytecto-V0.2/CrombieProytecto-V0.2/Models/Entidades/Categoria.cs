namespace CrombieProytecto_V0._2.Models.Entidades
{
    public class Categoria : BaseEntity
    {
        public string Nombre { get; set; }
        public ICollection<ProductoCategoria> ProductoCategorias { get; set; }
    }
}
