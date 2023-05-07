using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication1.Models;

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
        public async Task<IActionResult> GetAllCars() => Ok(await _context.Cars.ToListAsync());

        [HttpPost]// Создание машин из БД

        public async Task<IActionResult> AddCar([FromBody] Car cars)
        {
            await _context.Cars.AddAsync(cars);
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

        public async Task<IActionResult> UpdateCar([FromRoute] int id, Car car)
        {
            _context.Cars.Update(car);
            await _context.SaveChangesAsync();

            return Ok(car);
        }

        [HttpGet]
        [Route("{Name}")]
        public async Task<IActionResult> ShowAllCarsTheSameName([FromRoute] string Name)
        {
            var cars = await _context.Cars.Where(c => c.Name == Name).ToListAsync();

            if (cars == null)
            {
                return NotFound();
            }

            return Ok(cars);
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
