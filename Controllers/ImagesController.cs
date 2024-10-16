using Microsoft.AspNetCore.Mvc;
using SurveillanceAPI.Services;

namespace SurveillanceAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImagesController : ControllerBase
    {
        private readonly ImageService _imageService;

        public ImagesController(ImageService imageService)
        {
            _imageService = imageService;
        }

        [HttpGet("{imageName}")]
        public IActionResult GetImage(string imageName)
        {
            var image = _imageService.GetImage(imageName);
            return File(image, "image/jpeg");
        }

        [HttpGet("newest")]
        public IActionResult GetNewestImage()
        {
            var image = _imageService.GetNewestImage();
            return File(image, "image/jpeg");
        }

        [HttpGet("test/{filename}")]
        public IActionResult Test(string filename)
        {
            return Ok(_imageService.Test(filename));
        }

        [HttpPost]
        public async Task<IActionResult> UploadImage([FromForm] IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                return BadRequest("No file uploaded.");
            }

            // Call the UploadImage method of your ImageService and pass the file
            await _imageService.UploadImage(file);

            return Ok(new { message = "Image uploaded successfully." });
        }
    }
}
