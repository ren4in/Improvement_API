using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Improvement_API.db;

namespace Improvement_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Point_ImageController : ControllerBase
    {
        private readonly DepartmentDbContext _context;

        public Point_ImageController(DepartmentDbContext context)
        {
            _context = context;
        }

        // GET: api/Point_Image
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Point_Image>>> GetPoint_Image()
        {
            return await _context.Point_Image.ToListAsync();
        }

        // GET: api/Point_Image/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Point_Image>> GetPoint_Image(int? id)
        {
            var point_Image = await _context.Point_Image.FindAsync(id);

            if (point_Image == null)
            {
                return NotFound();
            }

            return point_Image;
        }

        // PUT: api/Point_Image/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPoint_Image(int? id, Point_Image point_Image)
        {
            if (id != point_Image.id_Point_Image)
            {
                return BadRequest();
            }

            _context.Entry(point_Image).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Point_ImageExists(id))
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

        // POST: api/Point_Image
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Point_Image>> PostPoint_Image(Point_Image point_Image)
        {
            _context.Point_Image.Add(point_Image);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPoint_Image", new { id = point_Image.id_Point_Image }, point_Image);
        }

        // DELETE: api/Point_Image/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePoint_Image(int? id)
        {
            var point_Image = await _context.Point_Image.FindAsync(id);
            if (point_Image == null)
            {
                return NotFound();
            }

            _context.Point_Image.Remove(point_Image);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool Point_ImageExists(int? id)
        {
            return _context.Point_Image.Any(e => e.id_Point_Image == id);
        }
    }
}
