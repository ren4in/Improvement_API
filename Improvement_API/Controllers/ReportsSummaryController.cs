using Improvement_API.db;
using Improvement_Client.Services;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Improvement_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportsSummaryController : ControllerBase
    {
        private readonly ReportSummaryService _reportSummaryService;

        public ReportsSummaryController(ReportSummaryService reportSummaryService)
        {
            _reportSummaryService = reportSummaryService;
        }

        [HttpGet("SummaryReport")]
        public async Task<ActionResult<List<SummaryReport>>> GetSummaryReport(DateTime startDate, DateTime endDate)
        {
            var result = await _reportSummaryService.GetSummaryReportAsync(startDate, endDate);
            return Ok(result);
        }
    }
}
