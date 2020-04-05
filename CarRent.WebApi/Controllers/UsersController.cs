using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CarRent.BL.Logics;
using CarRent.WebApi.Helpers;
using CarRent.WebApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace CarRent.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly ILogger<UsersController> _logger;
        private readonly UserLogicBL _userLogicBL = new UserLogicBL();
        public UsersController(ILogger<UsersController> logger)
        {
            _logger = logger;
        }

        [HttpPost("registration/")]
        public async Task<IActionResult> Registration([FromForm] UserView userView)
        {
            try
            {
                if (!CustomValidators.IsValidEmail(userView.Email)) return ValidationProblem("email not valid");
                if(!CustomValidators.IsValidDateOfBirth(userView.DateOfBirth)) return ValidationProblem("date of birth not valid");
                var userEntity = await _userLogicBL.Registration(Mappers.MapUserViewToUserEntity(userView));
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