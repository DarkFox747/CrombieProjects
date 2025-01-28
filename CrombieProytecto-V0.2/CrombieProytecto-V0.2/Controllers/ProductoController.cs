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
    [Authorize]
    public class ProductoController : ControllerBase
    {
        private readonly ProductoService _productoService;

        public ProductoController(ProductoService productoService)
        {
            _productoService = productoService;
        }
        //Obtiene todos los productos
        [HttpGet("Get de todos los productos")]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<ProductoDto>>> GetProducts()
        {
            var productos = await _productoService.GetProductsAsync();
            return Ok(productos);
        }
        //Obtiene un producto por ID
        [HttpGet("Get de prodcutos por id: {id}")]
        [AllowAnonymous]
        public async Task<ActionResult<ProductoDto>> GetProduct(int id)
        {
            var producto = await _productoService.GetProductAsync(id);
            if (producto == null)
                return NotFound();

            return Ok(producto);
        }
        //Agrega un nuevo producto
        [HttpPost("Agregar un producto:")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<ProductoDto>> CreateProduct([FromQuery] string nombre, [FromQuery] string descripcion, [FromQuery] decimal precio, [FromQuery] int stock, [FromQuery] string? url)
        {
            var createDto = new CrearProductoDto
            {
                Nombre = nombre,
                Descripcion = descripcion,
                Precio = precio,
                Stock = stock,
                URL = url
            };

            var producto = await _productoService.CreateProductAsync(createDto);
            return CreatedAtAction(nameof(GetProduct), new { id = producto.Id }, producto);
        }
        //Modifica un producto existente
        [HttpPut("Modificar un producto:{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateProduct(int id, [FromQuery] string nombre, [FromQuery] string descripcion, [FromQuery] decimal precio, [FromQuery] int stock, [FromQuery] string? url)
        {
            var updateDto = new CrearProductoDto
            {
                Nombre = nombre,
                Descripcion = descripcion,
                Precio = precio,
                Stock = stock,
                URL = url
            };

            var result = await _productoService.UpdateProductAsync(id, updateDto);
            if (!result)
                return NotFound();

            return NoContent();
        }
        //Elimina un producto existente
        [HttpDelete("Eliminar un producto:{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var result = await _productoService.DeleteProductAsync(id);
            if (!result)
                return NotFound();

            return NoContent();
        }
    }

}

