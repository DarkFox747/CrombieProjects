using Proyect_DAL.Context;
using Proyect_Models.Models.dtos;
using Proyect_Models.Models.Entidades;
using Microsoft.EntityFrameworkCore;

namespace Proyect_BLL.Service
{
    //Define los m�todos para Categor�a
    public class CategoriaService
    {
        private readonly ProyectContext _context;

        public CategoriaService(ProyectContext context)
        {
            _context = context;
        }
        //Obtiene todas las categor�as
        public async Task<IEnumerable<CategoriaDto>> GetCategoriasAsync()
        {
            return await _context.Categorias
                .Select(c => new CategoriaDto
                {
                    Id = c.Id,
                    Nombre = c.Nombre
                })
                .ToListAsync();
        }
        //Obtiene una categor�a por ID
        public async Task<CategoriaDto?> GetCategoriaAsync(int id)
        {
            var categoria = await _context.Categorias.FindAsync(id);
            if (categoria == null)
                return null;

            return new CategoriaDto
            {
                Id = categoria.Id,
                Nombre = categoria.Nombre
            };
        }
        //Crea una nueva categor�a
        public async Task<CategoriaDto> CreateCategoriaAsync(CategoriaDto createDto)
        {
            var categoria = new Categoria
            {
                Nombre = createDto.Nombre,
                CreatedAt = DateTime.UtcNow
            };

            _context.Categorias.Add(categoria);
            await _context.SaveChangesAsync();

            return new CategoriaDto
            {
                Id = categoria.Id,
                Nombre = categoria.Nombre
            };
        }
        //Actualiza una categor�a existente
        public async Task<bool> UpdateCategoriaAsync(int id, CategoriaDto updateDto)
        {
            var categoria = await _context.Categorias.FindAsync(id);
            if (categoria == null)
                return false;

            categoria.Nombre = updateDto.Nombre;
            categoria.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();
            return true;
        }
        //Elimina una categor�a existente
        public async Task<bool> DeleteCategoriaAsync(int id)
        {
            var categoria = await _context.Categorias.FindAsync(id);
            if (categoria == null)
                return false;

            _context.Categorias.Remove(categoria);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
