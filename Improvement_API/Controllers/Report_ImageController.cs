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
    public class Report_ImageController : ControllerBase
    {
        private readonly DepartmentDbContext _context;

        public Report_ImageController(DepartmentDbContext context)
        {
            _context = context;
        }


        // GET: api/Report_Image
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Report_Image>>> GetReport_Image()
        {
            return await _context.Report_Image.ToListAsync();
        }

        [HttpGet("report/{id}")]
        public async Task<ActionResult<IEnumerable<Report_Image>>> GetReportsByOrder(int id)
        {
            return await _context.Report_Image
                .Include(r => r.id_ReportNavigation)
                .Where(r => r.id_Report == id)
                .ToListAsync();
        }
        // GET: api/Report_Image/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Report_Image>> GetReport_Image(int? id)
        {
            var report_Image = await _context.Report_Image.FindAsync(id);

            if (report_Image == null)
            {
                return NotFound();
            }

            return report_Image;
        }

        // PUT: api/Report_Image/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutReport_Image(int? id, Report_Image report_Image)
        {
            if (id != report_Image.id_Report_Image)
            {
                return BadRequest();
            }

            _context.Entry(report_Image).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Report_ImageExists(id))
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

        // POST: api/Report_Image
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Report_Image>> PostReport_Image(Report_Image report_Image)
        {
            _context.Report_Image.Add(report_Image);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetReport_Image", new { id = report_Image.id_Report_Image }, report_Image);
        }

        // DELETE: api/Report_Image/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteReport_Image(int? id)
        {
            var report_Image = await _context.Report_Image.FindAsync(id);
            if (report_Image == null)
            {
                return NotFound();
            }

            _context.Report_Image.Remove(report_Image);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool Report_ImageExists(int? id)
        {
            return _context.Report_Image.Any(e => e.id_Report_Image == id);
        }
    }
}
