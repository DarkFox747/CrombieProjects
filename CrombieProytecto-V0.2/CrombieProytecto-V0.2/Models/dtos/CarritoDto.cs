public class CarritoDto
{
    public int Id { get; set; }
    public int UsuarioId { get; set; }
    public List<CarritoProductoDto> Productos { get; set; } = new List<CarritoProductoDto>();
}

public class CarritoProductoDto
{
    public int ProductoId { get; set; }
    public string NombreProducto { get; set; }
    public int Cantidad { get; set; }
}

public class CrearCarritoDto
{
    public int ProductoId { get; set; }
    public int Cantidad { get; set; }
}
