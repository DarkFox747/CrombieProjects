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
        [HttpGet("Get de todas las wishlist")]
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
        [HttpGet("Get wishlist por:{id}")]
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
        [HttpPost("Crear Wishlist por nombre:{nombre}")]
        public async Task<ActionResult<WishListDto>> CreateWishList(string nombre)
        {
            try
            {
                var userId = await GetUserId(); // Llamada asíncrona
                var createDto = new CrearWishListDto { Nombre = nombre };
                var wishList = await _wishListService.CreateWishListAsync(createDto, userId);
                return CreatedAtAction(nameof(GetWishList), new { id = wishList.Id }, wishList);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        // Agrega producto a wishlist existente
        [HttpPost("Agregar un producto a al wishlist{wishListId}")]
        public async Task<ActionResult<WishListDto>> AddProductToWishList(int wishListId, int productId)
        {
            try
            {
                var userId = await GetUserId(); // Llamada asíncrona
                var wishList = await _wishListService.AddProductToWishListAsync(wishListId, productId, userId);
                return Ok(wishList);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // Elimina producto de wishlist existente
        [HttpDelete(template: "Eliminar un producto de la wishlist por id:{wishListId}/remove-product/{productId}")]
        public async Task<ActionResult<WishListDto>> RemoveProductFromWishList(int wishListId, int productId)
        {
            try
            {
                var userId = await GetUserId(); // Llamada asíncrona
                var wishList = await _wishListService.RemoveProductFromWishListAsync(wishListId, productId, userId);

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
        [HttpDelete("Eliminar una wishlist por id:{id}")]
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