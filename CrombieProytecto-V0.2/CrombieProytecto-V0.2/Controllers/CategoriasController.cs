using CrombieProytecto_V0._2.Models.dtos;
using CrombieProytecto_V0._2.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CrombieProytecto_V0._2.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    

    public class CategoriasController : ControllerBase
    {
        private readonly CategoriaService _categoriaService;

        public CategoriasController(CategoriaService categoriaService)
        {
            _categoriaService = categoriaService;
        }
        //Obtiene todas las categorías
        
        [HttpGet]
        [CustomAuthorize]
        public async Task<ActionResult<IEnumerable<CategoriaDto>>> GetCategorias()
        {
            var categorias = await _categoriaService.GetCategoriasAsync();
            return Ok(categorias);
        }
        //Obtiene una categoría por ID
        [HttpGet("{id}")]
        [CustomAuthorize(RequiredRole = "Admin")]
        public async Task<ActionResult<CategoriaDto>> GetCategoria(int id)
        {
            var categoria = await _categoriaService.GetCategoriaAsync(id);
            if (categoria == null)
                return NotFound();

            return Ok(categoria);
        }
        //Crea una nueva categoría
        [HttpPost]
        [CustomAuthorize(RequiredRole = "Admin")]
        public async Task<ActionResult<CategoriaDto>> CreateCategoria(CategoriaDto createDto)
        {
            var categoria = await _categoriaService.CreateCategoriaAsync(createDto);
            return CreatedAtAction(nameof(GetCategoria), new { id = categoria.Id }, categoria);
        }
        //Actualiza una categoría existente
        [HttpPut("{id}")]
        [CustomAuthorize(RequiredRole = "Admin")]
        public async Task<IActionResult> UpdateCategoria(int id, CategoriaDto updateDto)
        {
            var success = await _categoriaService.UpdateCategoriaAsync(id, updateDto);
            if (!success)
                return NotFound();

            return NoContent();
        }
        //Elimina una categoría existente
        [HttpDelete("{id}")]
        [CustomAuthorize(RequiredRole = "Admin")]
        public async Task<IActionResult> DeleteCategoria(int id)
        {
            var success = await _categoriaService.DeleteCategoriaAsync(id);
            if (!success)
                return NotFound();

            return NoContent();
        }
    }
}
