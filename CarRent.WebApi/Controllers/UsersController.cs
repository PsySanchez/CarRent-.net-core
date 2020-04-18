using System;
using System.Threading.Tasks;
using CarRent.BL.Logics;
using CarRent.WebApi.Helpers;
using CarRent.WebApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace CarRent.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    { 
        private readonly IConfiguration _config;
        private readonly ILogger<UsersController> _logger;
        private readonly UserLogicBL _userLogicBL = new UserLogicBL();
        public UsersController(ILogger<UsersController> logger, IConfiguration config)
        {
            _config = config;
            _logger = logger;
        }

        [HttpPost("registration/")]
        public async Task<IActionResult> Registration([FromForm] UserView userView)
        {
            try
            {
                if (!CustomValidators.IsValidEmail(userView.Email)) return ValidationProblem("email not valid");
                if(!CustomValidators.IsValidDateOfBirth(userView.DateOfBirth)) return ValidationProblem("date of birth not valid");
                var userEntity = await _userLogicBL.Registration(Mappers.MapUserViewToUserEntity(userView), _config);
                if (userEntity == null) return Conflict("email already exists");
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                return StatusCode(500);
            }
        }
    }
}