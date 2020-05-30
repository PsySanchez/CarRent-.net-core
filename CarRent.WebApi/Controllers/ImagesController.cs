using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Net.Http.Headers;

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
    }
}
