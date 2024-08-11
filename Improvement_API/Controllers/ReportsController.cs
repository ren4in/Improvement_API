using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace Improvement_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportsController : ControllerBase
    {
        private readonly DepartmentDbContext _context;

        public ReportsController(DepartmentDbContext context)
        {
            _context = context;
        }

        // GET: api/Reports
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Report>>> GetReport()
        {
            return await _context.Report.Include(r=>r.id_OrderNavigation.id_ExecutorNavigation).ToListAsync();
        }

        [HttpGet("user/{id}")]
        public async Task<ActionResult<IEnumerable<Report>>> GetReportsByUser(int id)
        {
            return await _context.Report
                .Include(r => r.id_OrderNavigation.id_ExecutorNavigation)
                .Where(r => r.id_OrderNavigation.id_Executor == id)
                .ToListAsync();
        }

        // GET: api/Reports/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Report>> GetReport(int id)
        {
            var report = await _context.Report.FindAsync(id);

            if (report == null)
            {
                return NotFound();
            }

            return report;
        }

        // PUT: api/Reports/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutReport(int id, Report report)
        {
            if (id != report.id_Report)
            {
                return BadRequest();
            }

            _context.Entry(report).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ReportExists(id))
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

        [HttpGet("search")]
        public async Task<ActionResult<IEnumerable<Report>>> SearchReports(int? id_Order, string searchText )
        {
            if (string.IsNullOrEmpty(searchText))
            {
                return BadRequest("Search text cannot be empty");
            }

            // Преобразуем поисковый текст в нижний регистр, чтобы поиск был регистронезависимым
            searchText = searchText.ToLower();

            var matchedReports= await _context.Report
                .Include(r => r.id_OrderNavigation.id_ExecutorNavigation).Include(r=>r.id_OrderNavigation.id_SupervisorNavigation)
                .Where(r =>
                    (r.id_Order==id_Order) && 
                    (r.Text.ToLower().Contains(searchText) ||
                    r.Manager_Comment.ToLower().Contains(searchText) ||
                     r.Header.ToLower().Contains(searchText)  
                     )
                )
                .ToListAsync();

            if (matchedReports == null || !matchedReports.Any())
            {
                return NotFound("No reports found matching the search criteria");
            }

            return Ok(matchedReports);
        }


        [HttpGet("order/{id}")]
        public async Task<ActionResult<IEnumerable<Report>>> GetReportsByOrder(int id)
        {
            return await _context.Report
                .Include(r => r.id_OrderNavigation.id_ExecutorNavigation)

                .Where(r => r.id_Order == id)
                .ToListAsync();
        }



        // POST: api/Reports
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Report>> PostReport(Report report)
        {
            _context.Report.Add(report);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetReport", new { id = report.id_Report }, report);
        }

        [HttpPost("ToggleAccepted/{id_Report}")]
        public async Task<IActionResult> ToggleAccepted(int id_Report)
        {
            var report = await _context.Report.FindAsync(id_Report);

            if (report == null)
            {
                return NotFound();
            }

            // Изменяем значение Accepted на противоположное
            report.Accepted = !report.Accepted;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ReportExists(id_Report))
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
        // DELETE: api/Reports/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteReport(int id)
        {
            var report = await _context.Report.FindAsync(id);
            if (report == null)
            {
                return NotFound();
            }

            _context.Report.Remove(report);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ReportExists(int id)
        {
            return _context.Report.Any(e => e.id_Report == id);
        }
    }
}
