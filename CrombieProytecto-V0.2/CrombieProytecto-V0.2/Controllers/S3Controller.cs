using Microsoft.AspNetCore.Mvc;
using CrombieProytecto_V0._2.Service;

namespace CrombieProytecto_V0._2.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class S3Controller : ControllerBase
    {
        private readonly S3Service _s3Service;

        public S3Controller(S3Service s3Service)
        {
            _s3Service = s3Service;
        }
       
        [HttpPost("Cargar Productos al S3")]
        public async Task<IActionResult> UploadImage(IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                return BadRequest("No file provided.");
            }

            var key = await _s3Service.UploadFileAsync(file);
            return Ok(new { Key = key });
        }
    }
}
