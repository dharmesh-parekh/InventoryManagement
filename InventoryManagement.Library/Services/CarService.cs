using InventoryManagement.Core.Entities;
using InventoryManagement.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;


namespace InventoryManagement.Library.Services
{
    public interface ICarService
    {
        List<Car> GetAll();
        Car GetById(long Id);
        long Save(Car car);

        void Delete(Car car);

        List<GetCars_Result> GetCarList(int userId, int pageNo, int pageSize, string sortColumn, string sortOrder, string searchValue = "");

    }

    public class CarService : ICarService
    {
        IRepository<Car> _carRepository;
        public CarService(IRepository<Car> carRepository)
        {
            _carRepository = carRepository;
        }

        public List<Car> GetAll()
        {
            return _carRepository.Table.ToList();
        }

        public Car GetById(long Id)
        {
            return _carRepository.GetById(Id);
        }

        public long Save(Car car)
        {
            if (car.CarId > 0)
                _carRepository.Update(car);
            else
                _carRepository.Insert(car);
            return car.CarId;
        }

        public void Delete(Car car)
        {
            _carRepository.Delete(car);
        }

        public List<GetCars_Result> GetCarList(int userId, int pageNo, int pageSize, string sortColumn, string sortOrder, string searchValue = "")
        {
            return _carRepository.Context.Database.SqlQuery<GetCars_Result>("exec GetCars {0},{1},{2},{3},{4},{5}", userId, pageNo, pageSize, sortColumn, sortOrder, searchValue).ToList();
        }

    }
}
