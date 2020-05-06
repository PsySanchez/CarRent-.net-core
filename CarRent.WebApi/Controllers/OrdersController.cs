using System;
using System.Threading.Tasks;
using CarRent.BL.Logics;
using CarRent.WebApi.Helpers;
using CarRent.WebApi.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace CarRent.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class OrdersController : ControllerBase
    {
        private readonly UserLogicBL _userLogicBL = new UserLogicBL();
        private readonly AuthService _authService = new AuthService();
        private readonly OrdersLogicBL _ordersLogicBL = new OrdersLogicBL();
        private readonly IConfiguration _config;
        private readonly ILogger<OrdersController> _logger;
        public OrdersController(ILogger<OrdersController> logger, IConfiguration config)
        {
            _logger = logger;
            _config = config;
        }

        [HttpPost]
        public async Task<IActionResult> NewUserOrder([FromForm] OrderView orderView)
        {
            try
            {
                var user = await _userLogicBL.GetUserByEmail(orderView.Email);
                orderView.UserId = user.Id;
                var orderEntity = Mappers.MapOrderViewToOrderEntity(orderView);
                _ordersLogicBL.AddUserOrder(orderEntity);
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                return StatusCode(500);
            }
        }
        // GET: api/Orders/userOrdersHistory/{customer email}
        [HttpGet("userOrdersHistory/{custEmail}")]
        public async Task<IActionResult> GetUserOrdersHistory(string custEmail)
        {
            try
            {
                if (!CustomValidators.IsValidEmail(custEmail)) return ValidationProblem("email not valid");

                var accessToken = await HttpContext.GetTokenAsync("access_token");
                var principal = _authService.GetPrincipalFromToken(accessToken, _config);
                var userEmail = principal.Identity.Name;
                // get the user who sent the request
                var user = await _userLogicBL.GetUserByEmail(userEmail);
                // get the user in whose name the request was
                var cust = await _userLogicBL.GetUserByEmail(custEmail);
                // the user can receive information only if the request is in his name
                // , or if he is admin
                if (custEmail == userEmail || user.Role == "admin")
                {
                    var userOrdersHistory = await _ordersLogicBL.GetUserOrdersHistory((int)cust.Id);
                    return Ok(userOrdersHistory);
                }
                return Forbid("email not valid");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                return StatusCode(500);
            }
        }
    }
}