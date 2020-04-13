using System;
using System.Threading.Tasks;
using CarRent.BL.Logics;
using CarRent.WebApi.Helpers;
using CarRent.WebApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace CarRent.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class OrdersController : ControllerBase
    {
        private readonly UserLogicBL _userLogicBL = new UserLogicBL();
        private readonly ILogger<OrdersController> _logger;
        public OrdersController(ILogger<OrdersController> logger)
        {
            _logger = logger;
        }

        [HttpPost]
        public async Task<IActionResult> NewUserOrder([FromForm] OrderView orderView)
        {
            try
            {
                var user = await _userLogicBL.GetUserByEmail(orderView.Email);
                orderView.UserId = user.Id;
                var orderEntity = Mappers.MapOrderViewToOrderEntity(orderView);
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