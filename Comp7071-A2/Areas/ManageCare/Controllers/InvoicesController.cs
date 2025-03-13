using Comp7071_A2.Data;
using Comp7071_A2.Areas.ManageCare.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Comp7071_A2.Areas.ManageCare.Controllers
{
    public class InvoicesController : ManageCareController
    {
        private readonly ApplicationDbContext _context;

        public InvoicesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: ManageCare/Invoices
        public async Task<IActionResult> Index()
        {
            var invoices = await _context.Invoices
                .Include(i => i.Customer)
                .Include(i => i.Lines)
                .ToListAsync();
            return View(invoices);
        }

        // GET: ManageCare/Invoices/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var invoice = await _context.Invoices
                .Include(i => i.Customer)
                .Include(i => i.Lines)
                .FirstOrDefaultAsync(m => m.Id == id);
                
            if (invoice == null)
            {
                return NotFound();
            }

            return View(invoice);
        }

        // GET: ManageCare/Invoices/Create
        public IActionResult Create()
        {
            ViewData["Customers"] = new SelectList(_context.Customers, "Id", "Name");
            return View();
        }

        // POST: ManageCare/Invoices/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("StartDate,EndDate,CustomerId")] Invoice invoice)
        {
            if (ModelState.IsValid)
            {
                invoice.Id = Guid.NewGuid();
                
                // Generate invoice lines based on customer's scheduled services
                var customer = await _context.Customers
                    .Include(c => c.Schedules)
                    .ThenInclude(s => s.Service)
                    .FirstOrDefaultAsync(c => c.Id == invoice.CustomerId);
                
                if (customer != null && customer.Schedules != null)
                {
                    // Find schedules within the invoice date range
                    var schedules = customer.Schedules
                        .Where(s => s.StartTime >= invoice.StartDate && s.EndTime <= invoice.EndDate)
                        .ToList();
                    
                    foreach (var schedule in schedules)
                    {
                        if (schedule.Service != null)
                        {
                            // Calculate duration in hours
                            var duration = (schedule.EndTime - schedule.StartTime).TotalHours;
                            
                            // Create invoice line
                            var line = new InvoiceLine
                            {
                                Id = Guid.NewGuid(),
                                InvoiceId = invoice.Id,
                                Description = $"{schedule.Service.Name} - {schedule.StartTime:g} to {schedule.EndTime:g}",
                                Quantity = 1,
                                UnitPrice = schedule.Service.Rate,
                                Amount = schedule.Service.Rate * (decimal)duration
                            };
                            
                            invoice.Lines.Add(line);
                        }
                    }
                }
                
                _context.Add(invoice);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            
            ViewData["Customers"] = new SelectList(_context.Customers, "Id", "Name", invoice.CustomerId);
            return View(invoice);
        }

        // GET: ManageCare/Invoices/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var invoice = await _context.Invoices
                .Include(i => i.Lines)
                .FirstOrDefaultAsync(i => i.Id == id);
                
            if (invoice == null)
            {
                return NotFound();
            }
            
            ViewData["CustomerId"] = new SelectList(_context.Customers, "Id", "Name", invoice.CustomerId);
            return View(invoice);
        }

        // POST: ManageCare/Invoices/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,StartDate,EndDate,CustomerId")] Invoice invoice)
        {
            if (id != invoice.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(invoice);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!InvoiceExists(invoice.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            
            ViewData["CustomerId"] = new SelectList(_context.Customers, "Id", "Name", invoice.CustomerId);
            return View(invoice);
        }

        // GET: ManageCare/Invoices/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var invoice = await _context.Invoices
                .Include(i => i.Customer)
                .FirstOrDefaultAsync(m => m.Id == id);
                
            if (invoice == null)
            {
                return NotFound();
            }

            return View(invoice);
        }

        // POST: ManageCare/Invoices/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var invoice = await _context.Invoices.FindAsync(id);
            if (invoice != null)
            {
                _context.Invoices.Remove(invoice);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // GET: ManageCare/Invoices/Email/5
        public async Task<IActionResult> Email(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var invoice = await _context.Invoices
                .Include(i => i.Customer)
                .Include(i => i.Lines)
                .FirstOrDefaultAsync(m => m.Id == id);
                
            if (invoice == null)
            {
                return NotFound();
            }

            // In a real application, you would send an email here
            // For now, we'll just show a success message
            TempData["SuccessMessage"] = $"Invoice #{invoice.Id} has been emailed to {invoice.Customer.Name}.";
            
            return RedirectToAction(nameof(Details), new { id });
        }

        // GET: ManageCare/Invoices/Report
        public async Task<IActionResult> Report()
        {
            // Group invoices by month and calculate total revenue
            var invoices = await _context.Invoices
                .Include(i => i.Lines)
                .ToListAsync();
                
            var report = invoices
                .GroupBy(i => new { i.StartDate.Year, i.StartDate.Month })
                .Select(g => new InvoiceReportViewModel
                {
                    Year = g.Key.Year,
                    Month = g.Key.Month,
                    InvoiceCount = g.Count(),
                    TotalRevenue = g.SelectMany(i => i.Lines).Sum(l => l.Amount)
                })
                .OrderByDescending(r => r.Year)
                .ThenByDescending(r => r.Month)
                .ToList();
                
            return View(report);
        }

        private bool InvoiceExists(Guid id)
        {
            return _context.Invoices.Any(e => e.Id == id);
        }
    }


    public class InvoiceReportViewModel
    {
        public int Year { get; set; }
        public int Month { get; set; }
        public int InvoiceCount { get; set; }
        public decimal TotalRevenue { get; set; }
        
        public string MonthName => new DateTime(Year, Month, 1).ToString("MMMM");
    }
} 