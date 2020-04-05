using CarRent.DAL.Models;
using CarRent.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarRent.DAL.Logics
{
    public class CarsLogicDAL
    {
        public async Task<List<CarEntity>> GetAllCars()
        {
            using var db = new CarRentContext();
            return await db.Cars.Select(car => new CarEntity
            {
                Id = car.Id,
                Photo = car.Photo,
                Model = car.Model,
                PricePerDay = car.PricePerDay,
                CarNumber = car.CarNumber,
                Manufacturer = car.ManufacturerName
            }).ToListAsync();
        }
        public async Task<List<ManufacturerEntity>> GetAllManufacturers()
        {
            using var db = new CarRentContext();
            return await db.ManufacturersCar.Select(manufacturer => new ManufacturerEntity
            {
                Id = manufacturer.Id,
                Manufacturer = manufacturer.ManufacturerName
            }).ToListAsync();
        }
        public async Task<List<ModelCarEntity>> GetAllModelCars()
        {
            using var db = new CarRentContext();
            return await db.ModelsCar.Select(model => new ModelCarEntity
            {
                Id = model.Id,
                Model = model.Model,
                ManufacturerId = model.ManufacturerId,
                PricePerDay = model.PricePerDay,
                Photo = model.Photo
            }).ToListAsync();
        }
        public async Task<CarEntity> GetCarById(int id)
        {
            using var db = new CarRentContext();
            return await db.Cars.Where(car => car.Id == id)
                .Select(car => new CarEntity 
                {
                    Id = car.Id,
                    Photo = car.Photo,
                    Model = car.Model,
                    PricePerDay = car.PricePerDay,
                    CarNumber = car.CarNumber,
                    Manufacturer = car.ManufacturerName
                }).SingleOrDefaultAsync();
        }
    }
}
