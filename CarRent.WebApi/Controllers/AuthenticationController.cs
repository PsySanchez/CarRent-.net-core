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
    public class AuthenticationController : ControllerBase
    {
        private readonly ILogger<AuthenticationController> _logger;
        private readonly IConfiguration _config;
        private readonly AuthService _authService = new AuthService();
        private readonly CredentialsLogicBL _credentialsLogicBL = new CredentialsLogicBL();
        private readonly UserLogicBL _userLogicBL = new UserLogicBL();
        public AuthenticationController(ILogger<AuthenticationController> logger, IConfiguration config)
        {
            _logger = logger;
            _config = config;
        }

        [HttpPost]
        public async Task<IActionResult> Authenticate([FromForm] CredentialsView credential)
        {
            try
            {
                if (!CustomValidators.IsValidEmail(credential.Email)) return ValidationProblem("email not valid");
                var user = await _credentialsLogicBL.Authentication(Mappers.MapCredentialsViewToCredentialsEntity(credential));
                if (user == null) return Unauthorized("email or password is incorrect");
                var accessToken = _authService.CreateAccessToken(user, _config);
                // save only signing credentials
                _credentialsLogicBL.SaveRefreshToken(accessToken.RefreshToken, user, _config.GetValue<int>("JwtAuthentication:LifeTimeRefreshToken"));
                return Ok(accessToken);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                return StatusCode(500);
            }
        }

        [HttpPost("refresh/")]
        public async Task<IActionResult> Refresh([FromForm] Token token)
        {
            try
            {
                var principal = _authService.GetPrincipalFromExpiredToken(token.AccessToken, _config);
                if (principal == null) return Unauthorized("invalid token");
                var email = principal.Identity.Name;
                var user = await _userLogicBL.GetUserByEmail(email);
                if(user == null) return Unauthorized("invalid token");
                if (await _credentialsLogicBL.CompareUserToken(token.RefreshToken, (int)user.Id))
                {
                    var accessToken = _authService.CreateAccessToken(user, _config);
                    // save only signing credentials
                    _credentialsLogicBL.SaveRefreshToken(accessToken.RefreshToken, user, _config.GetValue<int>("JwtAuthentication:LifeTimeRefreshToken"));
                    return Ok(accessToken);
                }
                return Unauthorized();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                return StatusCode(500);
            }
        }
    }
}



//[HttpPost("refreshToken/")]
//[Authorize]
//public async Task<IActionResult> RefreshToken()
//{
//    try
//    {
//        //var accessToken = await HttpContext.GetTokenAsync(JwtBearerDefaults.AuthenticationScheme, "accessToken");
//        var authToken = HttpContext.Request.Headers["Authorization"];
//        var email = User.Identity.Name;
//        var user = await _credentialsLogicBL.GetUser(email);
//        if (await _credentialsLogicBL.CompareUserToken(authToken, email))
//        {
//            var accessToken = _authService.CreateAccessToken(user, _config);
//            // save only signing credentials
//            _credentialsLogicBL.SaveRefreshToken(accessToken.ResfreshToken, user, _config);
//            return Ok(accessToken);
//        }
//        return Unauthorized();
//    }
//    catch (Exception ex)
//    {
//        _logger.LogError(ex.ToString());
//        return StatusCode(500);
//    }

//}