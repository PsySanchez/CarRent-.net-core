﻿using System;
using System.IO;
using System.Threading.Tasks;
using CarRent.BL.Logics;
using CarRent.WebApi.Helpers;
using CarRent.WebApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace CarRent.WebApi.Controllers
{
    [Authorize(Roles = "admin")]
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly IConfiguration _config;
        private readonly ILogger<CarsController> _logger;

        private readonly CarsLogicBL _carsLogicBL = new CarsLogicBL();
        private readonly UserLogicBL _userLogicBL = new UserLogicBL();
        public AdminController(ILogger<CarsController> logger, IConfiguration config)
        {
            _logger = logger;
            _config = config;
        }
        // POST: api/Cars/AddNewCar
        [HttpPost("AddNewCar/")]
        public IActionResult AddNewCar([FromForm] CarView carView)
        {
            try
            {
                _carsLogicBL.AddNewCar(Mappers.MapCarViewToCarEntity(carView));
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                return StatusCode(500);
            }
        }
        // POST: api/Images/uploadImage
        [RequestSizeLimit(5000000)]
        [HttpPost("UploadImage/")]
        public async Task<IActionResult> UploadImage([FromForm] IFormFile image)
        {
            var filePath = _config.GetValue<string>("ImagePath:Path");
            try
            {
                if (image == null) return ValidationProblem();

                using var stream = new FileStream(filePath + image.FileName, FileMode.Create);
                await image.CopyToAsync(stream);
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                return StatusCode(500);
            }
        }

        [HttpPost("CustSearch/")]
        public async Task<IActionResult> CustSearch([FromForm] CustSearchView custSearch)
        {
            try
            {
                if (!CustomValidators.IsValidCustSearch(custSearch)) return ValidationProblem("email or phone number not valid");

                var response = await _userLogicBL.CustSearch(Mappers.MapCustSearchViewToCustSearchEntity(custSearch));
                if (response == null) return NotFound();

                return Ok(Mappers.MapUserEntityToUserView(response));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                return StatusCode(500);
            }
        }
    }
}