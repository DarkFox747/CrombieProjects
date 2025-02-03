using CrombieProytecto_V0._2.Models.Entidades;

public class Compra
{
    public int Id { get; set; }
    public int UsuarioId { get; set; }
    public Usuario Usuario { get; set; }
    public DateTime FechaCompra { get; set; }
    public ICollection<CompraProducto> Productos { get; set; } = new List<CompraProducto>();
}

public class CompraProducto
{
    public int CompraId { get; set; }
    public Compra Compra { get; set; }
    public int ProductoId { get; set; }
    public Producto Producto { get; set; }
    public int Cantidad { get; set; }
}
