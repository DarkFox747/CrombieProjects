using Proyect_BLL.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace CrombieProytecto_V0._2.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [CustomAuthorize(RequiredRole = "Admin")]
    //[CustomAuthorize(RequiredRole = "Admin")]
    public class S3Controller : ControllerBase
    {
        private readonly S3Service _s3Service;

        public S3Controller(S3Service s3Service)
        {
            _s3Service = s3Service;
        }

        // Subir una imagen a S3
        [HttpPost("upload")]

        public async Task<IActionResult> UploadImage(IFormFile file)
        {
            if (file == null || file.Length == 0)
                return BadRequest("Archivo no válido.");

            try
            {
                var key = await _s3Service.UploadFileAsync(file);
                return Ok(new { Key = key, Message = "Imagen subida correctamente." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al subir la imagen: {ex.Message}");
            }
        }

        // Obtener la URL de una imagen por su clave (key)
        [HttpGet("url/{key}")]
        public IActionResult GetImageUrl(string key)
        {
            try
            {
                var url = _s3Service.GetFileUrl(key);
                return Ok(new { Url = url });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al obtener la URL de la imagen: {ex.Message}");
            }
        }

        // Obtener todas las URLs de las imágenes en el bucket
        [HttpGet("urls")]
        public async Task<IActionResult> GetAllImageUrls()
        {
            try
            {
                var urls = await _s3Service.GetAllFileUrlsAsync();
                return Ok(urls);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al obtener las URLs de las imágenes: {ex.Message}");
            }
        }

        // Eliminar una imagen por su clave (key)
        [HttpDelete("delete/{key}")]
        public async Task<IActionResult> DeleteImage(string key)
        {
            try
            {
                await _s3Service.DeleteFileAsync(key);
                return Ok(new { Message = "Imagen eliminada correctamente." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al eliminar la imagen: {ex.Message}");
            }
        }

        // Obtener todas las claves (keys) de las imágenes en el bucket
        [HttpGet("keys")]
        public async Task<IActionResult> GetAllKeys()
        {
            try
            {
                var keys = await _s3Service.ListFilesAsync();
                return Ok(keys);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al obtener las claves de las imágenes: {ex.Message}");
            }
        }
    }
}