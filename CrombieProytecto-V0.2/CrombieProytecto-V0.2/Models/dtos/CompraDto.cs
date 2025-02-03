public class CompraDto
{
    public int Id { get; set; }
    public int UsuarioId { get; set; }
    public DateTime FechaCompra { get; set; }
    public List<CompraProductoDto> Productos { get; set; } = new List<CompraProductoDto>();
}

public class CompraProductoDto
{
    public int ProductoId { get; set; }
    public string NombreProducto { get; set; }
    public int Cantidad { get; set; }
}

public class CrearCompraDto
{
    public List<CrearCarritoDto> Productos { get; set; } = new List<CrearCarritoDto>();
}
