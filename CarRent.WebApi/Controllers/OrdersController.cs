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

        [HttpGet("userOrdersHistory/")]
        public async Task<IActionResult> GetUserOrdersHistory()
        {
            try
            {
                var accessToken = await HttpContext.GetTokenAsync("access_token");
                var principal = _authService.GetPrincipalFromToken(accessToken, _config);
                var email = principal.Identity.Name;
                var user = await _userLogicBL.GetUserByEmail(email);
                var userOrdersHistory = await _ordersLogicBL.GetUserOrdersHistory((int)user.Id);
                return Ok(userOrdersHistory);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                return StatusCode(500);
            }
        }
    }
}