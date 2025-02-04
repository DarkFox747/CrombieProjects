using Proyect_DAL.Context;
using Proyect_Models.Models.dtos;
using Proyect_BLL.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace CrombieProytecto_V0._2.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [CustomAuthorize]
    public class WishListsController : ControllerBase
    {
        private readonly WishListService _wishListService;
        private readonly ProyectContext _context; // Inyectar el contexto de la base de datos

        public WishListsController(WishListService wishListService, ProyectContext context)
        {
            _wishListService = wishListService;
            _context = context; // Inicializar el contexto
        }

        // Obtiene todas las wishlists
        [HttpGet]
        public async Task<ActionResult<IEnumerable<WishListDto>>> GetWishLists()
        {
            try
            {
                var userId = await GetUserId(); // Llamada asíncrona
                var wishLists = await _wishListService.GetWishListsAsync(userId);
                return Ok(wishLists);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        // Obtiene wishlist por ID
        [HttpGet("{id}")]
        public async Task<ActionResult<WishListDto>> GetWishList(int id)
        {
            try
            {
                var userId = await GetUserId(); // Llamada asíncrona
                var wishList = await _wishListService.GetWishListByIdAsync(id, userId);

                if (wishList == null)
                    return NotFound($"No se encontró la wishlist con ID {id}");

                return Ok(wishList);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        // Crea nueva wishlist
        [HttpPost]
        public async Task<ActionResult<WishListDto>> CreateWishList([FromBody] CrearWishListDto createDto)
        {
            try
            {
                var userId = await GetUserId(); // Llamada asíncrona
                var wishList = await _wishListService.CreateWishListAsync(createDto, userId);
                return CreatedAtAction(nameof(GetWishList), new { id = wishList.Id }, wishList);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        // Agrega producto a wishlist existente
        [HttpPost("add-product")]
        public async Task<ActionResult<WishListDto>> AddProductToWishList([FromBody] AddRemoveProductDto dto)
        {
            try
            {
                var userId = await GetUserId(); // Llamada asíncrona
                var wishList = await _wishListService.AddProductToWishListAsync(dto.WishListId, dto.ProductId, userId);
                return Ok(wishList);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // Elimina producto de wishlist existente
        [HttpDelete("remove-product")]
        public async Task<ActionResult<WishListDto>> RemoveProductFromWishList([FromBody] AddRemoveProductDto dto)
        {
            try
            {
                var userId = await GetUserId(); // Llamada asíncrona
                var wishList = await _wishListService.RemoveProductFromWishListAsync(dto.WishListId, dto.ProductId, userId);

                if (wishList == null)
                    return NotFound();

                return Ok(wishList);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        // Elimina wishlist por ID
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteWishList(int id)
        {
            try
            {
                var userId = await GetUserId(); // Llamada asíncrona
                var success = await _wishListService.DeleteWishListAsync(id, userId);

                if (!success)
                    return NotFound();

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        // Obtiene todas las wishlists de un usuario por ID
        [HttpGet("user/{userId}")]
        public async Task<ActionResult<IEnumerable<WishListDto>>> GetWishListsByUserId(int userId)
        {
            try
            {
                var wishLists = await _wishListService.GetWishListsAsync(userId);
                return Ok(wishLists);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        // Obtiene todas las wishlists creadas con información resumida
        [HttpGet("all-Wishlist")]
        public async Task<ActionResult<IEnumerable<WishListSummaryDto>>> GetAllWishListSummaries()
        {
            try
            {
                var wishLists = await _context.WishList
                    .Include(w => w.Usuario)
                    .Select(w => new WishListSummaryDto
                    {
                        WishListId = w.Id,
                        WishListNombre = w.Nombre,
                        UsuarioId = w.IdUsuario,
                        UsuarioNombre = w.Usuario.Nombre
                    })
                    .ToListAsync();

                return Ok(wishLists);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        private async Task<int> GetUserId()
        {
            // Obtener el userId del claim (si está presente)
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userIdClaim != null && int.TryParse(userIdClaim, out var userId))
            {
                return userId;
            }

            // Si no se encuentra el userId en los claims, obtener el email del usuario
            var emailClaim = User.FindFirst(ClaimTypes.Email)?.Value;
            if (string.IsNullOrEmpty(emailClaim))
            {
                throw new UnauthorizedAccessException("No se pudo obtener el email del usuario.");
            }

            // Buscar el userId en la base de datos usando el email
            var usuario = await _context.Usuarios.FirstOrDefaultAsync(u => u.Email == emailClaim);
            if (usuario == null)
            {
                throw new UnauthorizedAccessException("No se encontró el usuario en la base de datos.");
            }

            return usuario.Id;
        }
    }
}