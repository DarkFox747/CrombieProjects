using Microsoft.AspNetCore.Mvc;
using CrombieProytecto_V0._1.Service;

namespace CrombieProytecto_V0._1.Controllers
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

        /// <summary>
        /// Uploads an image to Amazon S3.
        /// </summary>
        /// <param name="file">The image file to upload.</param>
        /// <returns>The key of the uploaded image.</returns>
        [HttpPost("upload")]
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
