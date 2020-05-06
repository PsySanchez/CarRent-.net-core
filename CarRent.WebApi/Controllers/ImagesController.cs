using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace CarRent.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImagesController : ControllerBase
    {
        private readonly ILogger<ImagesController> _logger;
        private readonly String _filePath = "C:\\Users\\User\\OneDrive\\GitRepos\\CarRent .net core\\Assets\\images\\";
        public ImagesController(ILogger<ImagesController> logger)
        {
            _logger = logger;
        }
        // GET: api/Images
        [HttpGet("{imageName}")]
        public IActionResult GetImage(string imageName)
        {
            try
            {
                var image = System.IO.File.OpenRead(_filePath + imageName);
                return File(image, "image/png");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                return StatusCode(500);
            }
        }
        // POST: api/Images/uploadImage
        [Authorize(Roles = "admin")]
        [RequestSizeLimit(5000000)]
        [HttpPost("UploadImage/")]
        public async Task<IActionResult> Post([FromForm] IFormFile image)
        {
            try
            {
                if (image == null) return ValidationProblem();

                using var stream = new FileStream(_filePath + image.FileName, FileMode.Create);
                await image.CopyToAsync(stream);
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                return StatusCode(500);
            }

        }
        [HttpGet]
        public IActionResult Test()
        {
            return Ok("images work");
        }
    }
}
