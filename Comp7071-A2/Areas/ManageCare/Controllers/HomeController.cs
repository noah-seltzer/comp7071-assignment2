using Comp7071_A2.Data;
using Comp7071_A2.Areas.ManageCare.Models;
using Comp7071_A2.Areas.ManageCare.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Comp7071_A2.Areas.ManageCare.Controllers
{
    public class HomeController : ManageCareController
    {
        private readonly ApplicationDbContext _context;

        public HomeController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var viewModel = new DashboardViewModel
            {
                CustomerCount = await _context.Customers.CountAsync(),
                EmployeeCount = await _context.Employees.CountAsync(),
                ServiceCount = await _context.Services.CountAsync(),
                ScheduleCount = await _context.Schedule.CountAsync(),
                TotalRevenue = await _context.Invoices
                    .SelectMany(i => i.Lines)
                    .SumAsync(l => l.Amount),

                RecentCustomers = await _context.Customers
                    .OrderByDescending(c => c.Id)
                    .Take(5)
                    .ToListAsync(),

                PopularServices = await _context.Services
                    .OrderByDescending(s => s.Schedule.Count)
                    .Take(5)
                    .ToListAsync(),

                RecentInvoices = await _context.Invoices
                    .Include(i => i.Customer)
                    .Include(i => i.Lines)
                    .OrderByDescending(i => i.StartDate)
                    .Take(5)
                    .ToListAsync(),

                UpcomingSchedules = await _context.Schedule
                    .Include(s => s.Service)
                    .Include(s => s.Customers)
                    .Include(s => s.Employees)
                    .Where(s => s.StartTime > DateTime.Now)
                    .OrderBy(s => s.StartTime)
                    .Take(5)
                    .ToListAsync()
            };

            return View(viewModel);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }

    public class ErrorViewModel
    {
        public string? RequestId { get; set; }

        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
    }
}