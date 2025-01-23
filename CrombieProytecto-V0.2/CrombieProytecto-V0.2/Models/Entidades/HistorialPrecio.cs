namespace CrombieProytecto_V0._2.Models.Entidades
{
    public class HistorialPrecio
    {
        public int Id { get; set; }
        public int ProductoId { get; set; }
        public Producto Producto { get; set; }
        public decimal Precio { get; set; }
        public DateTime Fecha { get; set; }
    }

}
