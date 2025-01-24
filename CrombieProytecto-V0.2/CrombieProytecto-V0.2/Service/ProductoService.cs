﻿using CrombieProytecto_V0._2.Context;
using CrombieProytecto_V0._2.Models.dtos;
using CrombieProytecto_V0._2.Models;
using Microsoft.EntityFrameworkCore;
using CrombieProytecto_V0._2.Models.Entidades;

namespace CrombieProytecto_V0._2.Service
{
    public class ProductoService
    {
        private readonly ProyectContext _context;

        public ProductoService(ProyectContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ProductoDto>> GetProductsAsync()
        {
            return await _context.Productos
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
                })
                .ToListAsync();
        }

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

        public async Task<ProductoDto> CreateProductAsync(CrearProductoDto createDto)
        {
            var producto = new Producto
            {
                Nombre = createDto.Nombre,
                Descripcion = createDto.Descripcion,
                Precio = createDto.Precio,
                Stock = createDto.Stock,
                URL = createDto.URL,
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

        public async Task<bool> UpdateProductAsync(int id, CrearProductoDto updateDto)
        {
            var producto = await _context.Productos
                .Include(p => p.ProductoCategorias)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (producto == null)
                return false;

            producto.Nombre = updateDto.Nombre;
            producto.Descripcion = updateDto.Descripcion;
            producto.Precio = updateDto.Precio;
            producto.Stock = updateDto.Stock;
            producto.URL = updateDto.URL;
            producto.UpdatedAt = DateTime.UtcNow;

            // Actualizar las categorías
            var existingCategorias = producto.ProductoCategorias.ToList();
            _context.ProductoCategorias.RemoveRange(existingCategorias);

            foreach (var categoriaId in updateDto.CategoriaIds)
            {
                var productoCategoria = new ProductoCategoria
                {
                    ProductoId = producto.Id,
                    CategoriaId = categoriaId
                };
                _context.ProductoCategorias.Add(productoCategoria);
            }

            await _context.SaveChangesAsync();
            return true;
        }

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
