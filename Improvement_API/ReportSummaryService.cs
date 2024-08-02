using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Improvement_API.db;
using Improvement_API;

namespace Improvement_Client.Services
{
    public class ReportSummaryService
    {
        private readonly DepartmentDbContext _context;

        public ReportSummaryService(DepartmentDbContext context)
        {
            _context = context;
        }

        public async Task<List<SummaryReport>> GetSummaryReportAsync(DateTime startDate, DateTime endDate)
        {
            var result = await _context.User
                .Select(user => new SummaryReport
                {
                    UserId = user.id_User.Value,
                    FullName = user.LastName + " " + user.FirstName + " " + user.MiddleName,
                    TotalOrders = user.Orders1
                        .Count(o => o.Date_of_Issue >= startDate && o.Date_of_Issue <= endDate),
                    CompletedOrders = user.Orders1
                        .Count(o => o.Date_of_Issue >= startDate && o.Date_of_Issue <= endDate && o.Accepted == true),
                    UncompletedOrders = user.Orders1
                        .Count(o => o.Date_of_Issue >= startDate && o.Date_of_Issue <= endDate && o.Accepted == false),
                    OverdueOrders = user.Orders1
                        .Count(o => o.Date_of_Issue >= startDate && o.Date_of_Issue <= endDate && o.Accepted == false && o.Deadline < DateTime.Now)
                })
                .ToListAsync();

            return result;
        }
    }
}
