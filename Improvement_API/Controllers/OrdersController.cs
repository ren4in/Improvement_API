using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Improvement_API;

namespace Improvement_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly DepartmentDbContext _context;

        public OrdersController(DepartmentDbContext context)
        {
            _context = context;
        }

        [HttpGet("user/{id}")]
        public async Task<ActionResult<IEnumerable<Order>>> GetOrdersByUser(int id)
        {
            return await _context.Order
                .Include(r => r.id_ExecutorNavigation)
                .Include(r => r.id_SupervisorNavigation)

                .Where(r => r.id_Executor == id)
                .ToListAsync();
        }
        [HttpGet("search")]
        public async Task<ActionResult<IEnumerable<Report>>> SearchOrders(int? id_User, string searchText)
        {
            if (string.IsNullOrEmpty(searchText))
            {
                return BadRequest("Search text cannot be empty");
            }

            // Преобразуем поисковый текст в нижний регистр, чтобы поиск был регистронезависимым
            searchText = searchText.ToLower();

            var matchedOrders = await _context.Order
                .Include(r => r.id_ExecutorNavigation).Include(r => r.id_SupervisorNavigation)
               .Where(r =>
            r.id_Executor == id_User &&
            (
                r.Text.ToLower().Contains(searchText) ||
                (r.id_SupervisorNavigation.FirstName.ToLower() + " " +
                 r.id_SupervisorNavigation.MiddleName.ToLower() + " " +
                 r.id_SupervisorNavigation.LastName.ToLower()).Contains(searchText) ||
                r.Header.ToLower().Contains(searchText)
            )
        )
        .ToListAsync();

            if (matchedOrders == null || !matchedOrders.Any())
            {
                return NotFound("No orders found matching the search criteria");
            }
 

            return Ok(matchedOrders);
        }




        [HttpPost("ToggleAccepted/{id_Order}")]
        public async Task<IActionResult> ToggleAccepted(int id_Order)
        {
            var order = await _context.Order.FindAsync(id_Order);

            if (order == null)
            {
                return NotFound();
            }

            // Изменяем значение Accepted на противоположное
            order.Accepted = !order.Accepted;


            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OrderExists(id_Order))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }


        // GET: api/Orders
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Order>>> GetOrder()
        {
            return await _context.Order.Include(p=>p.id_ExecutorNavigation).ToListAsync();
        }

        // GET: api/Orders/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Order>> GetOrder(int? id)
        {
            var order = await _context.Order.FindAsync(id);

            if (order == null)
            {
                return NotFound();
            }

            return order;
        }

        // PUT: api/Orders/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutOrder(int? id, Order order)
        {
            if (id != order.id_Order)
            {
                return BadRequest();
            }

            _context.Entry(order).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OrderExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Orders
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Order>> PostOrder(Order order)
        {
            _context.Order.Add(order);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetOrder", new { id = order.id_Order }, order);
        }

        // DELETE: api/Orders/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrder(int? id)
        {
            var order = await _context.Order.FindAsync(id);
            if (order == null)
            {
                return NotFound();
            }

            _context.Order.Remove(order);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool OrderExists(int? id)
        {
            return _context.Order.Any(e => e.id_Order == id);
        }
    }
}
