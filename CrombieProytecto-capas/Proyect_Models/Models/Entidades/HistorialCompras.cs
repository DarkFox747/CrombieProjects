namespace Proyect_Models.Models.Entidades
{
    public class HistorialCompras
    {
        public int Id { get; set; }
        public int IdProducto { get; set; }
        public Producto? Producto { get; set; }
        public int IdUsuario { get; set; }
        public Usuario? Usuario { get; set; }
    }
}
