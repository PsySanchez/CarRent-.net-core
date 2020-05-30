using CarRent.DAL.Logics;
using CarRent.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarRent.BL.Logics
{
    public class CarsLogicBL
    {
        private readonly CarsLogicDAL _carsLogicDAL = new CarsLogicDAL();
        public async Task<List<CarEntity>> GetAllCars()
        {
            return await _carsLogicDAL.GetAllCars();
        }

        public async Task<List<ManufacturerEntity>> GetAllManufacturers()
        {
            return await _carsLogicDAL.GetAllManufacturers();
        }

        public async Task<List<ModelCarEntity>> GetAllModelCars()
        {
            return await _carsLogicDAL.GetAllModelCars();
        }

        public async Task<CarEntity> GetCarById(int id)
        {
            return await _carsLogicDAL.GetCarById(id);
        }

        public void AddNewCar(CarEntity carEntity)
        {
            carEntity.Manufacturer = carEntity.Manufacturer.First().ToString().ToUpper() + carEntity.Manufacturer.Substring(1);
            _carsLogicDAL.AddNewCar(carEntity);
        }
    }
}
