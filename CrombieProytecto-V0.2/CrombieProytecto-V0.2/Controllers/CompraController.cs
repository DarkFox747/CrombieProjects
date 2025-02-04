using CrombieProytecto_V0._2.Context;
using CrombieProytecto_V0._2.Models;
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
    public class CompraController : ControllerBase
    {
        private readonly CompraService _compraService;
        private readonly CarritoService _carritoService;
        private readonly ProyectContext _context;

        public CompraController(CompraService compraService, CarritoService carritoService, ProyectContext context)
        {
            _compraService = compraService;
            _carritoService = carritoService;
            _context = context;
        }

        [HttpPost]
        public async Task<ActionResult<CompraDto>> CrearCompra()
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
            if (carrito.Productos.Count == 0)
            {
                return BadRequest("El carrito está vacío.");
            }

            var crearCompraDto = new CrearCompraDto
            {
                Productos = carrito.Productos.Select(p => new CrearCarritoDto
                {
                    ProductoId = p.ProductoId,
                    Cantidad = p.Cantidad
                }).ToList()
            };

            var compra = await _compraService.CrearCompraAsync(usuario.Id, crearCompraDto);
            await _carritoService.ClearCarritoAsync(usuario.Id);

            return Ok(compra);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<List<CompraDto>>> GetHistorialCompras(int id)
        {
            var usuario = await _context.Usuarios.FirstOrDefaultAsync(u => u.Id == id);
            if (usuario == null)
            {
                return Unauthorized("No se encontró el usuario en la base de datos.");
            }

            var historialCompras = await _compraService.GetHistorialComprasAsync(usuario.Id);
            return Ok(historialCompras);
        }

        [HttpGet]
        public async Task<ActionResult<List<CompraDto>>> GetHistorialCompras()
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

            var historialCompras = await _compraService.GetHistorialComprasAsync(usuario.Id);
            return Ok(historialCompras);
        }
    }
}