using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication1.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Intrinsics.X86;
using WebApplication1.DTO;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarController : ControllerBase
    {
        private readonly CarShopDBContext _context;
        public CarController(CarShopDBContext context)
        {
            _context = context;
        }

        [HttpGet] // Получение всех машин из базы данных
        public async Task<IActionResult> GetAllCars() 
        {
            List<CarDTO> CreateCar = new List<CarDTO>();//список объектов машин
            List <Car> Cars = await _context.Cars.ToListAsync();

            foreach(var Car in Cars) 
            {
                CarDTO car = new CarDTO() 
                { 
                    CarId = Car.CarId,
                    Name = Car.Name, 
                    Model = Car.Model, 
                    Horsepower = Car.Horsepower, 
                    Color = Car.Color,
                    Cost = Car.Cost
                };// создание объекта машины и инициализация данных 

                CreateCar.Add(car);
            }
           
            return Ok(CreateCar);  

        } 

        [HttpPost]// Создание машин из БД

        public async Task<IActionResult> AddCar([FromBody] CreateCarDTO cars)
        {
            
            Car car = new Car 
            { 
                Name = cars.Name,
                Model = cars.Model,
                Horsepower = Convert.ToInt32(cars.Horsepower),
                Cost = Convert.ToDouble(cars.Cost),
                Color = cars.Color
            };
            
            await _context.Cars.AddAsync(car);
            await _context.SaveChangesAsync();

            return Ok(cars);
        }

        [HttpDelete]// Удаление машины из БД
        [Route("{id:int}")]
        public async Task<IActionResult> DeleteCar([FromRoute] int id)
        {
            var car = await _context.Cars.FindAsync(id);

            if (car == null) 
            { 
                return NotFound();
            }
            _context.Cars.Remove(car);
            await _context.SaveChangesAsync();

            return Ok(car);
        }

        [HttpPut]// Изменение машины в БД
        [Route("{id:int}")]

        public async Task<IActionResult> UpdateCar([FromRoute] int id, CarDTO car)
        {
            var CurrentCar = await _context.Cars.FindAsync(id);

            CurrentCar.Model = car.Model;
            CurrentCar.Name = car.Name;
            CurrentCar.Horsepower = car.Horsepower;
            CurrentCar.Color = car.Color;
            CurrentCar.Cost = car.Cost;

            _context.Cars.Update(CurrentCar);
            await _context.SaveChangesAsync();

            return Ok(car);
        }

        [HttpGet]
        [Route("{id:int}")]
        public async Task<IActionResult> GetCarById([FromRoute] int id)
        {
            var car = await _context.Cars.FindAsync(id);

            if (car == null)
            {
                return NotFound();
            }

            return Ok(car);
        }

        [HttpGet]
        [Route("{Name}/{Model}")]
        public async Task<IActionResult> ShowCarByNameAndModel([FromRoute] string Name, string Model)
        {
            var cars = await _context.Cars.Where(c => c.Name == Name && c.Model == Model).ToListAsync();

            if (cars == null)
            {
                return NotFound();
            }

            return Ok(cars);
        }

    }
}
