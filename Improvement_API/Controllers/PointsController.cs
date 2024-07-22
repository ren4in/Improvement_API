using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Improvement_API.db;
using Microsoft.Data.SqlClient;

namespace Improvement_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PointsController : ControllerBase
    {
        private readonly DepartmentDbContext _context;

        public PointsController(DepartmentDbContext context)
        {
            _context = context;
        }

        // GET: api/Points
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Point>>> GetPoint()
        {
            return await _context.Point.Include(u=>u.id_UserNavigation).Include(p=> p.id_Point_ImageNavigation).ToListAsync();
        }

        // GET: api/Points/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Point>> GetPoint(int? id)
        {
            var point = await _context.Point.FindAsync(id);

            if (point == null)
            {
                return NotFound();
            }

            return point;
        }

        // PUT: api/Points/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPoint(int? id, Point point)
        {
            if (id != point.id_Point)
            {
                return BadRequest();
            }

            _context.Entry(point).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PointExists(id))
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

        // POST: api/Points
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Point>> PostPoint(Point point)
        {
            _context.Point.Add(point);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (PointExists(point.id_Point))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetPoint", new { id = point.id_Point }, point);
        }

        // DELETE: api/Points/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePoint(int? id)
        {
            var point = await _context.Point.FindAsync(id);
            if (point == null)
            {
                return NotFound();
            }

            // Выполнение собственного SQL-запроса на удаление записи
            var commandText = "DELETE FROM Point WHERE id_Point= @id";
            var idParam = new SqlParameter("@id", id);

            await _context.Database.ExecuteSqlRawAsync(commandText, idParam);

            return NoContent();
        }

        private bool PointExists(int? id)
        {
            return _context.Point.Any(e => e.id_Point == id);
        }
    }
}
