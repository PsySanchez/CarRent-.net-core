using CarRent.DAL.Models;
using CarRent.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
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
                Image = car.Image,
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
                Image = model.Image
            }).ToListAsync();
        }
        public async Task<CarEntity> GetCarById(int id)
        {
            using var db = new CarRentContext();
            return await db.Cars.Where(car => car.Id == id)
                .Select(car => new CarEntity
                {
                    Id = car.Id,
                    Image = car.Image,
                    Model = car.Model,
                    PricePerDay = car.PricePerDay,
                    CarNumber = car.CarNumber,
                    Manufacturer = car.ManufacturerName
                }).SingleOrDefaultAsync();
        }

        public async void AddNewCar(CarEntity carEntity)
        {
            int manufacturerId = await AddManufacturer(carEntity.Manufacturer);
            int modelId = await AddModel(carEntity, manufacturerId);
            AddCompanyFleet(modelId, carEntity.CarNumber);       
        }

        private async Task<int> AddManufacturer(string manufacturerName)
        {
            using var db = new CarRentContext();
            var manufacturer = await db.ManufacturersCar
                .FirstOrDefaultAsync(manufacturer =>
                manufacturer.ManufacturerName == manufacturerName);
            if (manufacturer == null)
            {
                ManufacturersCar manufacturerCar = new ManufacturersCar
                {
                    ManufacturerName = manufacturerName
                };
                await db.ManufacturersCar.AddAsync(manufacturer);
                await db.SaveChangesAsync();
                return manufacturerCar.Id;
            }
            return manufacturer.Id;
        }

        private async Task<int> AddModel(CarEntity carEntity, int manufacturerId)
        {
            using var db = new CarRentContext();
            var model = await db.ModelsCar
                .FirstOrDefaultAsync(model =>
                model.ManufacturerId == manufacturerId && model.Model == carEntity.Model);
            if (model == null)
            {
                ModelsCar modelCar = new ModelsCar
                {
                    ManufacturerId = manufacturerId,
                    Model = carEntity.Model,
                    PricePerDay = carEntity.PricePerDay,
                    Image = carEntity.Image
                };
                await db.ModelsCar.AddAsync(model);
                await db.SaveChangesAsync();
                return modelCar.Id;
            }
            return model.Id;
        }

        private async void AddCompanyFleet(int modelId, string carNumber)
        {
            using var db = new CarRentContext();
            var company = await db.CompanyFleet
                .FirstOrDefaultAsync(company =>
                company.CarNumber == carNumber);
            if (company == null)
            {
                CompanyFleet companyFleet = new CompanyFleet
                {
                    ModelId = modelId,
                    CarNumber = carNumber
                };
                await db.CompanyFleet.AddAsync(company);
                await db.SaveChangesAsync();
            }
        }
    }
}
