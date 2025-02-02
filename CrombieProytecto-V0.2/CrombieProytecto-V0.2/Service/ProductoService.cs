using CrombieProytecto_V0._2.Context;
using CrombieProytecto_V0._2.Models.dtos;
using CrombieProytecto_V0._2.Models;
using Microsoft.EntityFrameworkCore;
using CrombieProytecto_V0._2.Models.Entidades;
using System;
using CrombieProytecto_V0._2.Service;

namespace CrombieProytecto_V0._2.Service
{
    //Define los métodos para Producto
    public class ProductoService
    {
        private readonly ProyectContext _context;
        private readonly PaginationService<ProductoDto> _paginationService;
        private readonly S3Service _s3Service;
        private readonly string _bucketFolder = "productos"; //carpeta del s3

        public ProductoService(ProyectContext context, PaginationService<ProductoDto> paginationService, S3Service S3Service)
        {
            _context = context;
            _paginationService = paginationService;
            _s3Service = S3Service;
        }

        //Obtiene todos los productos con paginación
        public async Task<PaginatedResult<ProductoDto>> GetProductsAsync(PaginationParameters paginationParameters)
        {
            var query = _context.Productos
                .Include(p => p.ProductoCategorias)
                .ThenInclude(pc => pc.Categoria)
                .Select(p => new ProductoDto
                {
                    Id = p.Id,
                    Nombre = p.Nombre,
                    Descripcion = p.Descripcion,
                    Precio = p.Precio,
                    Stock = p.Stock,
                    URL = p.URL,
                    Categorias = p.ProductoCategorias.Select(pc => new CategoriaDto
                    {
                        Id = pc.Categoria.Id,
                        Nombre = pc.Categoria.Nombre
                    }).ToList()
                });

            return await _paginationService.GetPaginatedResult(query, paginationParameters.PageNumber, paginationParameters.PageSize);
        }

        //Obtiene un producto por ID
        public async Task<ProductoDto?> GetProductAsync(int id)
        {
            var producto = await _context.Productos
                .Include(p => p.ProductoCategorias)
                .ThenInclude(pc => pc.Categoria)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (producto == null)
                return null;

            return new ProductoDto
            {
                Id = producto.Id,
                Nombre = producto.Nombre,
                Descripcion = producto.Descripcion,
                Precio = producto.Precio,
                Stock = producto.Stock,
                URL = producto.URL,
                Categorias = producto.ProductoCategorias.Select(pc => new CategoriaDto
                {
                    Id = pc.Categoria.Id,
                    Nombre = pc.Categoria.Nombre
                }).ToList()
            };
        }

        //Crea un nuevo producto
        public async Task<ProductoDto> CreateProductAsync(CrearProductoDto createDto, IFormFile imagen)
        {

            var imageKey = await _s3Service.UploadFileAsync(imagen);
            var imageUrl = _s3Service.GetFileUrl(imageKey);


            var producto = new Producto
            {
                Nombre = createDto.Nombre,
                Descripcion = createDto.Descripcion,
                Precio = createDto.Precio,
                Stock = createDto.Stock,
                URL = imageUrl,
                CreatedAt = DateTime.UtcNow
            };

            _context.Productos.Add(producto);
            await _context.SaveChangesAsync();

            foreach (var categoriaId in createDto.CategoriaIds)
            {
                var productoCategoria = new ProductoCategoria
                {
                    ProductoId = producto.Id,
                    CategoriaId = categoriaId
                };
                _context.ProductoCategorias.Add(productoCategoria);
            }

            await _context.SaveChangesAsync();

            return new ProductoDto
            {
                Id = producto.Id,
                Nombre = producto.Nombre,
                Descripcion = producto.Descripcion,
                Precio = producto.Precio,
                Stock = producto.Stock,
                URL = producto.URL,
                Categorias = createDto.CategoriaIds.Select(id => new CategoriaDto { Id = id }).ToList()
            };
        }

        //Actualiza un producto existente
        public async Task<bool> UpdateProductAsync(int id, CrearProductoDto updateDto, IFormFile? imagen)
        {
            var producto = await _context.Productos
                .Include(p => p.ProductoCategorias)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (producto == null)
                return false;

            // Subir nueva imagen si se proporciona
            if (imagen != null && imagen.Length > 0)
            {
                // Eliminar imagen anterior de S3
                if (!string.IsNullOrEmpty(producto.URL))
                {
                    var oldImageKey = producto.URL.Split('/').Last();
                    await _s3Service.DeleteFileAsync(oldImageKey);
                }

                // Subir nueva imagen
                var imageKey = await _s3Service.UploadFileAsync(imagen);
                producto.URL = _s3Service.GetFileUrl(imageKey);
            }

            // Actualizar otros campos
            producto.Nombre = updateDto.Nombre;
            producto.Descripcion = updateDto.Descripcion;
            producto.Precio = updateDto.Precio;
            producto.Stock = updateDto.Stock;
            producto.UpdatedAt = DateTime.UtcNow;

            // Actualizar categorías
            _context.ProductoCategorias.RemoveRange(producto.ProductoCategorias);
            foreach (var categoriaId in updateDto.CategoriaIds)
            {
                _context.ProductoCategorias.Add(new ProductoCategoria
                {
                    ProductoId = producto.Id,
                    CategoriaId = categoriaId
                });
            }

            await _context.SaveChangesAsync();
            return true;
        }

        //Elimina producto existente
        public async Task<bool> DeleteProductAsync(int id)
        {
            var producto = await _context.Productos.FindAsync(id);
            if (producto == null)
                return false;

            _context.Productos.Remove(producto);
            await _context.SaveChangesAsync();
            return true;
        }
    }

}
