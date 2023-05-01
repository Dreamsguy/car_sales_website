using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
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
        public async Task<IActionResult> GetAllOrders() => Ok(await _context.Order.ToListAsync());

        [HttpPost]// Создание заказов из БД

        public async Task<IActionResult> AddOrder([FromBody] Orders orders)
        {
            await _context.Order.AddAsync(orders);
            await _context.SaveChangesAsync();

            return Ok(orders);
        }

        [HttpDelete]
        [Route("{id:int}")]
        public async Task<IActionResult> DeleteCar([FromRoute] int id)
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

        [HttpPut]
        [Route("{id:int}")]

        public async Task<IActionResult> UpdateCar([FromRoute] int id, Orders orders)
        {
            _context.Order.Update(orders);
            await _context.SaveChangesAsync();

            return Ok(orders);
        }
    }
}
