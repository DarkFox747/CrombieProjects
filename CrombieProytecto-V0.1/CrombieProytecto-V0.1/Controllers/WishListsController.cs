using CrombieProytecto_V0._1.Context;
using CrombieProytecto_V0._1.Models;
using CrombieProytecto_V0._1.Models.dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace CrombieProytecto_V0._1.Controllers

{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class WishListsController : ControllerBase
    {
        private readonly ProyectContext _context;
        private readonly ILogger<WishListsController> _logger;

        public WishListsController(ProyectContext context, ILogger<WishListsController> logger)
        {
            _context = context;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<WishListDto>>> GetWishLists()
        {
            try
            {
                var IdUsuario = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "0");
                _logger.LogInformation($"Obteniendo wishlists para el usuario {IdUsuario}");

                var wishLists = await _context.WishList
                    .AsNoTracking()
                    .Where(w => w.IdUsuario == IdUsuario)
                    .Include(w => w.Productos)
                        .ThenInclude(i => i.Producto)
                    .Select(w => new WishListDto
                    {
                        Id = w.Id,
                        Nombre = w.Nombre,
                        Productos = w.Productos.Select(i => new ProductoDto
                        {
                            Id = i.Producto.Id,
                            Nombre = i.Producto.Nombre,
                            Descripcion = i.Producto.Descripcion,
                            Precio = i.Producto.Precio,
                            Stock = i.Producto.Stock
                        }).ToList()
                    })
                    .ToListAsync();

                _logger.LogInformation($"Se encontraron {wishLists.Count} wishlists");
                return Ok(wishLists);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error al obtener wishlists: {ex.Message}");
                return StatusCode(500, "Error interno al obtener las wishlists");
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<WishListDto>> GetWishList(int id)
        {
            var IdUsuario = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "0");

            var wishList = await _context.WishList
                .AsNoTracking()
                .Include(w => w.Productos)
                    .ThenInclude(i => i.Producto)
                .FirstOrDefaultAsync(w => w.Id == id && w.IdUsuario == IdUsuario);

            if (wishList == null)
                return NotFound($"No se encontró la wishlist con ID {id}");

            var wishListDto = new WishListDto
            {
                Id = wishList.Id,
                Nombre = wishList.Nombre,
                Productos = wishList.Productos.Select(i => new ProductoDto
                {
                    Id = i.Producto.Id,
                    Nombre = i.Producto.Nombre,
                    Descripcion = i.Producto.Descripcion,
                    Precio = i.Producto.Precio,
                    Stock = i.Producto.Stock
                }).ToList()
            };

            return Ok(wishListDto);
        }

        [HttpPost]
        public async Task<ActionResult<WishListDto>> CreateWishList(CrearWishListDto createDto)
        {
            var IdUsuario = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "0");

            var wishList = new WishList
            {
                Nombre = createDto.Nombre,
                IdUsuario = IdUsuario,
                CreatedAt = DateTime.UtcNow,
                Productos = new List<WishListProductos>()
            };

            _context.WishList.Add(wishList);
            await _context.SaveChangesAsync();

            return CreatedAtAction(
                nameof(GetWishList),
                new { id = wishList.Id },
                new WishListDto
                {
                    Id = wishList.Id,
                    Nombre = wishList.Nombre,
                    Productos = new List<ProductoDto>()
                });
        }

        [HttpPost("{wishListId}/products")]
        public async Task<ActionResult<WishListDto>> AddProductToWishList(int IdWishList, [FromBody] int IdProducto)
        {
            try
            {
                var IdUsuario = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "0");

                var wishList = await _context.WishList
                    .Include(w => w.Productos)
                    .FirstOrDefaultAsync(w => w.Id == IdWishList && w.IdUsuario == IdUsuario);

                if (wishList == null)
                    return NotFound("WishList no encontrada");

                var producto = await _context.Productos.FindAsync(IdProducto);
                if (producto == null)
                    return NotFound("Producto no encontrado");

                if (wishList.Productos.Any(i => i.IdProducto == IdProducto))
                    return BadRequest("El producto ya está en la wishlist");

                var wishListProductos = new WishListProductos
                {
                    IdWishList = IdWishList,
                    IdProducto = IdProducto,
                    CreatedAt = DateTime.UtcNow
                };

                wishList.Productos.Add(wishListProductos);
                await _context.SaveChangesAsync();

                // Recargar la wishlist con los productos
                var updatedWishList = await _context.WishList
                    .Include(w => w.Productos)
                        .ThenInclude(i => i.Producto)
                    .FirstOrDefaultAsync(w => w.Id == IdWishList);

                var wishListDto = new WishListDto
                {
                    Id = updatedWishList.Id,
                    Nombre = updatedWishList.Nombre,
                    Productos = updatedWishList.Productos.Select(i => new ProductoDto
                    {
                        Id = i.Producto.Id,
                        Nombre = i.Producto.Nombre,
                        Descripcion = i.Producto.Descripcion,
                        Precio = i.Producto.Precio,
                        Stock = i.Producto.Stock
                    }).ToList()
                };

                return Ok(wishListDto);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error al agregar producto a la wishlist: {ex.Message}");
                return StatusCode(500, "Error interno al agregar el producto a la wishlist");
            }
        }

        [HttpDelete("{wishListId}/products/{productId}")]
        public async Task<ActionResult<WishListDto>> RemoveProductFromWishList(int wishListId, int productId)
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "0");

            var wishList = await _context.WishList
                .Include(w => w.Productos)
                .FirstOrDefaultAsync(w => w.Id == wishListId && w.IdUsuario == userId);

            if (wishList == null)
                return NotFound("WishList no encontrada");

            var wishListItem = wishList.Productos.FirstOrDefault(i => i.IdProducto == productId);
            if (wishListItem == null)
                return NotFound("Producto no encontrado en la wishlist");

            wishList.Productos.Remove(wishListItem);
            await _context.SaveChangesAsync();

            // Recargar la wishlist actualizada
            var updatedWishList = await _context.WishList
                .Include(w => w.Productos)
                    .ThenInclude(i => i.Producto)
                .FirstOrDefaultAsync(w => w.Id == wishListId);

            var wishListDto = new WishListDto
            {
                Id = updatedWishList.Id,
                Nombre = updatedWishList.Nombre,
                Productos = updatedWishList.Productos.Select(i => new ProductoDto
                {
                    Id = i.Producto.Id,
                    Nombre = i.Producto.Nombre,
                    Descripcion = i.Producto.Descripcion,
                    Precio = i.Producto.Precio,
                    Stock = i.Producto.Stock
                }).ToList()
            };

            return Ok(wishListDto);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteWishList(int id)
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "0");

            var wishList = await _context.WishList
                .FirstOrDefaultAsync(w => w.Id == id && w.IdUsuario == userId);

            if (wishList == null)
                return NotFound();

            _context.WishList.Remove(wishList);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
