using CrombieProytecto_V0._2.Models.dtos;
using CrombieProytecto_V0._2.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace CrombieProytecto_V0._2.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class WishListsController : ControllerBase
    {
        private readonly WishListService _wishListService;

        public WishListsController(WishListService wishListService)
        {
            _wishListService = wishListService;
        }

        [HttpGet ("Get de todas las wishlist")]
        public async Task<ActionResult<IEnumerable<WishListDto>>> GetWishLists()
        {
            try
            {
                var userId = GetUserId();
                var wishLists = await _wishListService.GetWishListsAsync(userId);
                return Ok(wishLists);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("Get wishlist por:{id}")]
        public async Task<ActionResult<WishListDto>> GetWishList(int id)
        {
            var userId = GetUserId();
            var wishList = await _wishListService.GetWishListByIdAsync(id, userId);

            if (wishList == null)
                return NotFound($"No se encontró la wishlist con ID {id}");

            return Ok(wishList);
        }

        [HttpPost("Crear Wishlist por nombre:{nombre}")]
        public async Task<ActionResult<WishListDto>> CreateWishList(string nombre)
        {
            var userId = GetUserId();
            var createDto = new CrearWishListDto { Nombre = nombre };
            var wishList = await _wishListService.CreateWishListAsync(createDto, userId);
            return CreatedAtAction(nameof(GetWishList), new { id = wishList.Id }, wishList);
        }

        [HttpPost("Agregar un producto a al wishlist{wishListId}")]
        public async Task<ActionResult<WishListDto>> AddProductToWishList(int wishListId, int productId)
        {
            try
            {
                var userId = GetUserId();
                var wishList = await _wishListService.AddProductToWishListAsync(wishListId, productId, userId);
                return Ok(wishList);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete(template: "Eliminar un producto de la wishlist por id:{wishListId}/remove-product/{productId}")]
        public async Task<ActionResult<WishListDto>> RemoveProductFromWishList(int wishListId, int productId)
        {
            var userId = GetUserId();
            var wishList = await _wishListService.RemoveProductFromWishListAsync(wishListId, productId, userId);

            if (wishList == null)
                return NotFound();

            return Ok(wishList);
        }

        [HttpDelete("Eliminar una wishlist por id:{id}")]
        public async Task<IActionResult> DeleteWishList(int id)
        {
            var userId = GetUserId();
            var success = await _wishListService.DeleteWishListAsync(id, userId);

            if (!success)
                return NotFound();

            return NoContent();
        }

        private int GetUserId() => int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "0");
    }
}
