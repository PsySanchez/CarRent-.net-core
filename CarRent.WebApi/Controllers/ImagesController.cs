using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace CarRent.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImagesController : ControllerBase
    {
        private readonly ILogger<ImagesController> _logger;
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
                var image = System.IO.File.OpenRead("C:\\Users\\User\\OneDrive\\Documents\\GitRepos\\CarRent .net core\\Assets\\images\\" + imageName);
                return File(image, "image/png");
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
