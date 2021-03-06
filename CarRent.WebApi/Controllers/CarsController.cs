﻿using System;
using System.Collections.Generic;
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
    public class CarsController : ControllerBase
    {
        private readonly IConfiguration _config;
        private readonly ILogger<CarsController> _logger;
 
        private readonly CarsLogicBL _carsLogicBL = new CarsLogicBL();

        public CarsController(ILogger<CarsController> logger, IConfiguration config)
        {
            _logger = logger;
            _config = config;
        }

        // GET: api/Cars
        [HttpGet]
        public async Task<IActionResult> GetAllCars()
        {
            try
            {
                var carsEntitiesList = await _carsLogicBL.GetAllCars();
                var response = new List<CarView>();
                foreach (var car in carsEntitiesList)
                {
                    response.Add(Mappers.MapCarEntityToCarView(car));
                }
                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                return StatusCode(500);
            }
        }
        [HttpGet("manufacturers/")]
        public async Task<IActionResult> GetManufacturers()
        {
            try
            {
                var manufacturerEntityList = await _carsLogicBL.GetAllManufacturers();
                var response = new List<ManufacturerView>();
                foreach (var manufacturer in manufacturerEntityList)
                {
                    response.Add(Mappers.MapManufacturerEntityToManufacturerView(manufacturer));
                }
                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                return StatusCode(500);
            }
        }
        [HttpGet("models/")]
        public async Task<IActionResult> GetModels()
        {
            try
            {
                var modelEntityList = await _carsLogicBL.GetAllModelCars();
                var response = new List<ModelCarView>();
                foreach (var model in modelEntityList)
                {
                    response.Add(Mappers.MapCarEntityToModelCarView(model));
                }
                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                return StatusCode(500);
            }
        }
        // GET: api/Cars/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCar(int id)
        {
            try
            {
                var carEntity = await _carsLogicBL.GetCarById(id);
                if (carEntity == null) return NotFound();
                var response = Mappers.MapCarEntityToCarView(carEntity);
                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                return StatusCode(500);
            }
        }
        // GET: api/Images
        [HttpGet("image/{imageName}")]
        public IActionResult GetImage(string imageName)
        {
            var filePath = _config.GetValue<string>("ImagePath:Path");
            try
            {
                var image = System.IO.File.OpenRead(filePath + imageName);
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
