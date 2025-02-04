using CrombieProytecto_V0._2.Context;
using Microsoft.EntityFrameworkCore;

public class CompraService
{
    private readonly ProyectContext _context;

    public CompraService(ProyectContext context)
    {
        _context = context;
    }

    public async Task<CompraDto> CrearCompraAsync(int usuarioId, CrearCompraDto dto)
    {
        var compra = new Compra
        {
            UsuarioId = usuarioId,
            FechaCompra = DateTime.UtcNow,
            Productos = dto.Productos.Select(p => new CompraProducto
            {
                ProductoId = p.ProductoId,
                Cantidad = p.Cantidad
            }).ToList()
        };

        _context.Compras.Add(compra);
        await _context.SaveChangesAsync();

        return new CompraDto
        {
            Id = compra.Id,
            UsuarioId = compra.UsuarioId,
            FechaCompra = compra.FechaCompra,
            Productos = compra.Productos.Select(cp => new CompraProductoDto
            {
                ProductoId = cp.ProductoId,
                NombreProducto = cp.Producto.Nombre,
                Cantidad = cp.Cantidad
            }).ToList()
        };
    }

    public async Task<List<CompraDto>> GetHistorialComprasAsync(int usuarioId)
    {
        var compras = await _context.Compras
            .Include(c => c.Productos)
            .ThenInclude(cp => cp.Producto)
            .Where(c => c.UsuarioId == usuarioId)
            .ToListAsync();

        return compras.Select(c => new CompraDto
        {
            Id = c.Id,
            UsuarioId = c.UsuarioId,
            FechaCompra = c.FechaCompra,
            Productos = c.Productos.Select(cp => new CompraProductoDto
            {
                ProductoId = cp.ProductoId,
                NombreProducto = cp.Producto.Nombre,
                Cantidad = cp.Cantidad
            }).ToList()
        }).ToList();
    }

    public async Task<CompraDto> GetCompraAsync(int usuarioId, int compraId)
    {
        var compra = await _context.Compras
            .Include(c => c.Productos)
            .ThenInclude(cp => cp.Producto)
            .FirstOrDefaultAsync(c => c.UsuarioId == usuarioId && c.Id == compraId);
        if (compra == null)
        {
            return null;
        }
        return new CompraDto
        {
            Id = compra.Id,
            UsuarioId = compra.UsuarioId,
            FechaCompra = compra.FechaCompra,
            Productos = compra.Productos.Select(cp => new CompraProductoDto
            {
                ProductoId = cp.ProductoId,
                NombreProducto = cp.Producto.Nombre,
                Cantidad = cp.Cantidad
            }).ToList()
        };
    }
}
