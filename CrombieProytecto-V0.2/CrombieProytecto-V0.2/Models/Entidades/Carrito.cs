using CrombieProytecto_V0._2.Models.Entidades;

public class Carrito
{
    public int Id { get; set; }
    public int UsuarioId { get; set; }
    public Usuario Usuario { get; set; }
    public ICollection<CarritoProducto> Productos { get; set; } = new List<CarritoProducto>();
}

public class CarritoProducto
{
    public int CarritoId { get; set; }
    public Carrito Carrito { get; set; }
    public int ProductoId { get; set; }
    public Producto Producto { get; set; }
    public int Cantidad { get; set; }
}
