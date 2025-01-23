using CrombieProytecto_V0._2.Context;
using CrombieProytecto_V0._2.Models.dtos;
using CrombieProytecto_V0._2.Models.Entidades;
using Microsoft.EntityFrameworkCore;

namespace CrombieProytecto_V0._2.Service
{
    public class WishListService
    {
        private readonly ProyectContext _context;

        public WishListService(ProyectContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<WishListDto>> GetWishListsAsync(int userId)
        {
            return await _context.WishList
                .AsNoTracking()
                .Where(w => w.IdUsuario == userId)
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
        }

        public async Task<WishListDto?> GetWishListByIdAsync(int id, int userId)
        {
            var wishList = await _context.WishList
                .AsNoTracking()
                .Include(w => w.Productos)
                    .ThenInclude(i => i.Producto)
                .FirstOrDefaultAsync(w => w.Id == id && w.IdUsuario == userId);

            if (wishList == null) return null;

            return new WishListDto
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
        }

        public async Task<WishListDto> CreateWishListAsync(CrearWishListDto createDto, int userId)
        {
            var wishList = new WishList
            {
                Nombre = createDto.Nombre,
                IdUsuario = userId,
                CreatedAt = DateTime.UtcNow,
                Productos = new List<WishListProductos>()
            };

            _context.WishList.Add(wishList);
            await _context.SaveChangesAsync();

            return new WishListDto
            {
                Id = wishList.Id,
                Nombre = wishList.Nombre,
                Productos = new List<ProductoDto>()
            };
        }

        public async Task<WishListDto?> AddProductToWishListAsync(int wishListId, int productId, int userId)
        {
            var wishList = await _context.WishList
                .Include(w => w.Productos)
                .FirstOrDefaultAsync(w => w.Id == wishListId && w.IdUsuario == userId);

            if (wishList == null)
                throw new Exception("WishList no encontrada");

            var producto = await _context.Productos.FindAsync(productId);
            if (producto == null)
                throw new Exception("Producto no encontrado");

            if (wishList.Productos.Any(i => i.IdProducto == productId))
                throw new Exception("El producto ya está en la wishlist");

            wishList.Productos.Add(new WishListProductos
            {
                IdWishList = wishListId,
                IdProducto = productId,
                CreatedAt = DateTime.UtcNow
            });

            await _context.SaveChangesAsync();

            return await GetWishListByIdAsync(wishListId, userId);
        }

        public async Task<WishListDto?> RemoveProductFromWishListAsync(int wishListId, int productId, int userId)
        {
            var wishList = await _context.WishList
                .Include(w => w.Productos)
                .FirstOrDefaultAsync(w => w.Id == wishListId && w.IdUsuario == userId);

            if (wishList == null) return null;

            var wishListItem = wishList.Productos.FirstOrDefault(i => i.IdProducto == productId);
            if (wishListItem == null) return null;

            wishList.Productos.Remove(wishListItem);
            await _context.SaveChangesAsync();

            return await GetWishListByIdAsync(wishListId, userId);
        }

        public async Task<bool> DeleteWishListAsync(int id, int userId)
        {
            var wishList = await _context.WishList.FirstOrDefaultAsync(w => w.Id == id && w.IdUsuario == userId);

            if (wishList == null) return false;

            _context.WishList.Remove(wishList);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}
