using CrombieProytecto_V0._2.Context;
using CrombieProytecto_V0._2.Models.dtos;
using CrombieProytecto_V0._2.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace CrombieProytecto_V0._2.Controllers
{ 
    [ApiController]
    [Route("api/[controller]")]
    [CustomAuthorize]
    public class CarritoController : ControllerBase
    {
        private readonly CarritoService _carritoService;
        private readonly ProyectContext _context;

        public CarritoController(CarritoService carritoService, ProyectContext context)
        {
            _carritoService = carritoService;
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<CarritoDto>> GetCarrito()
        {
            var email = User.FindFirst(ClaimTypes.Email)?.Value;
            if (string.IsNullOrEmpty(email))
            {
                return Unauthorized("No se pudo obtener el email del usuario.");
            }

            var usuario = await _context.Usuarios.FirstOrDefaultAsync(u => u.Email == email);
            if (usuario == null)
            {
                return Unauthorized("No se encontró el usuario en la base de datos.");
            }

            var carrito = await _carritoService.GetCarritoAsync(usuario.Id);
            return Ok(carrito);
        }

        [HttpPost]
        public async Task<IActionResult> AddProductoToCarrito([FromBody] CrearCarritoDto dto)
        {
            var email = User.FindFirst(ClaimTypes.Email)?.Value;
            if (string.IsNullOrEmpty(email))
            {
                return Unauthorized("No se pudo obtener el email del usuario.");
            }

            var usuario = await _context.Usuarios.FirstOrDefaultAsync(u => u.Email == email);
            if (usuario == null)
            {
                return Unauthorized("No se encontró el usuario en la base de datos.");
            }

            await _carritoService.AddProductoToCarritoAsync(usuario.Id, dto);
            return NoContent();
        }

        [HttpDelete]
        public async Task<IActionResult> ClearCarrito()
        {
            var email = User.FindFirst(ClaimTypes.Email)?.Value;
            if (string.IsNullOrEmpty(email))
            {
                return Unauthorized("No se pudo obtener el email del usuario.");
            }

            var usuario = await _context.Usuarios.FirstOrDefaultAsync(u => u.Email == email);
            if (usuario == null)
            {
                return Unauthorized("No se encontró el usuario en la base de datos.");
            }

            await _carritoService.ClearCarritoAsync(usuario.Id);
            return NoContent();
        }
    }

}