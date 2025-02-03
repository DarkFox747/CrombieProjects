using CrombieProytecto_V0._2.Context;
using Microsoft.EntityFrameworkCore;

public class CarritoService
{
    private readonly ProyectContext _context;

    public CarritoService(ProyectContext context)
    {
        _context = context;
    }

    public async Task<CarritoDto> GetCarritoAsync(int usuarioId)
    {
        var carrito = await _context.Carritos
            .Include(c => c.Productos)
            .ThenInclude(cp => cp.Producto)
            .FirstOrDefaultAsync(c => c.UsuarioId == usuarioId);

        if (carrito == null)
        {
            carrito = new Carrito { UsuarioId = usuarioId };
            _context.Carritos.Add(carrito);
            await _context.SaveChangesAsync();
        }

        return new CarritoDto
        {
            Id = carrito.Id,
            UsuarioId = carrito.UsuarioId,
            Productos = carrito.Productos.Select(cp => new CarritoProductoDto
            {
                ProductoId = cp.ProductoId,
                NombreProducto = cp.Producto.Nombre,
                Cantidad = cp.Cantidad
            }).ToList()
        };
    }

    public async Task AddProductoToCarritoAsync(int usuarioId, CrearCarritoDto dto)
    {
        var carrito = await _context.Carritos
            .Include(c => c.Productos)
            .FirstOrDefaultAsync(c => c.UsuarioId == usuarioId);

        if (carrito == null)
        {
            carrito = new Carrito { UsuarioId = usuarioId };
            _context.Carritos.Add(carrito);
        }

        var carritoProducto = carrito.Productos.FirstOrDefault(cp => cp.ProductoId == dto.ProductoId);
        if (carritoProducto == null)
        {
            carritoProducto = new CarritoProducto
            {
                ProductoId = dto.ProductoId,
                Cantidad = dto.Cantidad
            };
            carrito.Productos.Add(carritoProducto);
        }
        else
        {
            carritoProducto.Cantidad += dto.Cantidad;
        }

        await _context.SaveChangesAsync();
    }

    public async Task ClearCarritoAsync(int usuarioId)
    {
        var carrito = await _context.Carritos
            .Include(c => c.Productos)
            .FirstOrDefaultAsync(c => c.UsuarioId == usuarioId);

        if (carrito != null)
        {
            _context.CarritoProductos.RemoveRange(carrito.Productos);
            await _context.SaveChangesAsync();
        }
    }
}
