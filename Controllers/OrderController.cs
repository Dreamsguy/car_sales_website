using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication1.DTO;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly CarShopDBContext _context;
        public OrderController(CarShopDBContext context)
        {
            _context = context;
        }

        [HttpGet] // Получение всех заказов из базы данных
        public async Task<IActionResult> GetAllOrders() => Ok(await _context.Order.Include(x => x.Cars).ToListAsync()); 
        

        [HttpPost]// Создание заказов из БД
        public async Task<IActionResult> CreateOrder([FromBody] CreateOrderDTO orders)
        {
            var car = await _context.Cars.FirstOrDefaultAsync(c => c.CarId == orders.CarId);

            if (car == null)
            {
                return NotFound($"Car with ID {orders.CarId} not found.");
            }

            Orders order = new Orders
            {
                date = orders.date,
                name = orders.name,
                status = orders.status,
                Cars = new List<Car>()
            };

            order.Cars.Add(car);

            await _context.Order.AddAsync(order);
            await _context.SaveChangesAsync();

            return Ok(order);
        }

        [HttpDelete]
        [Route("{id:int}")]
        public async Task<IActionResult> DeleteOrder([FromRoute] int id)
        {
            var order = await _context.Order.FindAsync(id);

            if (order == null)
            {
                return NotFound();
            }
            _context.Order.Remove(order);
            await _context.SaveChangesAsync();

            return Ok();
        }

        //[HttpPut]
        //[Route("{Name}/{Model}")]

        //public async Task<IActionResult> UpdateOrder([FromRoute] string Name, string Model)
        //{
        //    var cars = await _context.Cars.Where(c => c.Name == Name && c.Model == Model).ToListAsync();

        //    if (cars == null)
        //    {
        //        return NotFound();
        //    }

        //    _context.Order.Update(cars);
        //    await _context.SaveChangesAsync();

        //    return Ok(orders);
        //}

       // [HttpPut]
       // [Route("{Name}/{Model}")]
       // public async Task<IActionResult> UpdateOrder([FromRoute] int Id, string Name, string Model)
       // {
       //     var order = await _context.Order.FindAsync(Id); // получаем объект Order из базы данных

       //     if (order == null)
       //     {
       //         return NotFound();
       //     }

       //     var car = await _context.Cars.FirstOrDefaultAsync(c => c.Name == Name && c.Model == Model);

       //     if (car == null)
       //     {
       //         return NotFound();
       //     }

       //     order.Cars.Add(car); // добавляем объект Car в коллекцию Cars объекта Order

       //     _context.Order.Update(order);
       //     await _context.SaveChangesAsync();

       //     return Ok(order);
       //}

    }
}
