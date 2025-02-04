using CrombieProytecto_V0._2.Context;
using CrombieProytecto_V0._2.Models;
using CrombieProytecto_V0._2.Models.dtos;
using CrombieProytecto_V0._2.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace CrombieProytecto_V0._2.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    
    public class ProductoController : ControllerBase
    {
        private readonly ProductoService _productoService;

        public ProductoController(ProductoService productoService)
        {
            _productoService = productoService;
        }

        //Obtiene todos los productos con paginación
        [HttpGet]
        
        public async Task<ActionResult<PaginatedResult<ProductoDto>>> GetProducts([FromQuery] PaginationParameters paginationParameters)
        {
            var productos = await _productoService.GetProductsAsync(paginationParameters);
            return Ok(productos);
        }

        //Obtiene un producto por ID
        [HttpGet("{id}")]
        
        public async Task<ActionResult<ProductoDto>> GetProduct(int id)
        {
            var producto = await _productoService.GetProductAsync(id);
            if (producto == null)
                return NotFound();

            return Ok(producto);
        }

        // Agrega un nuevo producto
        [HttpPost]
        [CustomAuthorize(RequiredRole = "Admin")]

        public async Task<ActionResult<ProductoDto>> CreateProduct(
            [FromForm] CrearProductoDto createDto, // Recibe datos del producto
            [FromForm] IFormFile imagen)           // Recibe el archivo de imagen
        {
            if (imagen == null || imagen.Length == 0)
                return BadRequest("Debe subir una imagen.");

            var producto = await _productoService.CreateProductAsync(createDto, imagen);
            return CreatedAtAction(nameof(GetProduct), new { id = producto.Id }, producto);
        }

        // Modifica un producto existente
        [HttpPut("{id}")]
        [CustomAuthorize(RequiredRole = "Admin")]
        public async Task<IActionResult> UpdateProduct(
            int id,
            [FromForm] CrearProductoDto updateDto, // Datos actualizados
            [FromForm] IFormFile? imagen)          // Imagen opcional
        {
            var result = await _productoService.UpdateProductAsync(id, updateDto, imagen);
            if (!result)
                return NotFound();

            return NoContent();
        }

        //Elimina un producto existente
        [HttpDelete("{id}")]
        [CustomAuthorize(RequiredRole = "Admin")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var result = await _productoService.DeleteProductAsync(id);
            if (!result)
                return NotFound();

            return NoContent();
        }
    }

}

