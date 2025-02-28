using Comp7071_A2.Data;
using Comp7071_A2.Areas.ManageCare.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Comp7071_A2.Areas.ManageCare.Controllers
{
    public class ScheduleController : ManageCareController
    {
        private readonly ApplicationDbContext _context;

        public ScheduleController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: ManageCare/Schedule
        public async Task<IActionResult> Index()
        {
            var schedules = await _context.Schedule
                .Include(s => s.Service)
                .Include(s => s.Employees)
                .Include(s => s.Customers)
                .ToListAsync();
            return View(schedules);
        }

        // GET: ManageCare/Schedule/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var schedule = await _context.Schedule
                .Include(s => s.Service)
                .Include(s => s.Employees)
                .Include(s => s.Customers)
                .FirstOrDefaultAsync(m => m.Id == id);
                
            if (schedule == null)
            {
                return NotFound();
            }

            return View(schedule);
        }

        // GET: ManageCare/Schedule/Create
        public IActionResult Create()
        {
            //var employees = _context.Customers, "Id", "Name"
            ViewData["ServiceId"] = new SelectList(_context.Services, "Id", "Name");
            ViewData["Employees"] = new MultiSelectList(_context.Employees, "Id", "Name");
            ViewData["Customers"] = new MultiSelectList(_context.Customers, "Id", "Name");
            return View();
        }

        // POST: ManageCare/Schedule/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("StartTime,EndTime,ServiceId")] Schedule schedule, Guid[] selectedEmployees, Guid[] selectedCustomers)
        {
            if (ModelState.IsValid)
            {
                schedule.Id = Guid.NewGuid();
                
                // Add selected employees
                if (selectedEmployees.Length > 0)
                {
                    var employees = await _context.Employees
                        .Where(e => selectedEmployees.Contains(e.Id))
                        .ToListAsync();
                    
                    schedule.Employees = employees;
                }
                
                // Add selected customers
                if (selectedCustomers.Length > 0)
                {
                    var customers = await _context.Customers
                        .Where(c => selectedCustomers.Contains(c.Id))
                        .ToListAsync();
                    
                    schedule.Customers = customers;
                }
                
                _context.Add(schedule);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            
            ViewData["ServiceId"] = new SelectList(_context.Services, "Id", "Name", schedule.ServiceId);
            ViewData["Employees"] = new MultiSelectList(_context.Employees, "Id", "Name", selectedEmployees);
             ViewData["Customers"] = new MultiSelectList(_context.Customers, "Id", "Name", selectedCustomers);
            return View(schedule);
        }

        // GET: ManageCare/Schedule/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var schedule = await _context.Schedule
                .Include(s => s.Employees)
                .Include(s => s.Customers)
                .FirstOrDefaultAsync(m => m.Id == id);
                
            if (schedule == null)
            {
                return NotFound();
            }
            
            ViewData["ServiceId"] = new SelectList(_context.Services, "Id", "Name", schedule.ServiceId);
            ViewData["Employees"] = new MultiSelectList(_context.Employees, "Id", "Name", schedule.Employees?.Select(e => e.Id));
            ViewData["Customers"] = new MultiSelectList(_context.Customers, "Id", "Name", schedule.Customers?.Select(c => c.Id));
            return View(schedule);
        }

        // POST: ManageCare/Schedule/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,StartTime,EndTime,ServiceId")] Schedule schedule, Guid[] selectedEmployees, Guid[] selectedCustomers)
        {
            if (id != schedule.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    // Get the existing schedule with its relationships
                    var existingSchedule = await _context.Schedule
                        .Include(s => s.Employees)
                        .Include(s => s.Customers)
                        .FirstOrDefaultAsync(s => s.Id == id);
                    
                    if (existingSchedule == null)
                    {
                        return NotFound();
                    }
                    
                    // Update scalar properties
                    existingSchedule.StartTime = schedule.StartTime;
                    existingSchedule.EndTime = schedule.EndTime;
                    existingSchedule.ServiceId = schedule.ServiceId;
                    
                    // Update employees
                    existingSchedule.Employees.Clear();
                    if (selectedEmployees != null && selectedEmployees.Length > 0)
                    {
                        foreach (var employeeId in selectedEmployees)
                        {
                            var employee = await _context.Employees.FindAsync(employeeId);
                            if (employee != null)
                            {
                                existingSchedule.Employees.Add(employee);
                            }
                        }
                    }
                    
                    // Update customers
                    existingSchedule.Customers.Clear();
                    if (selectedCustomers != null && selectedCustomers.Length > 0)
                    {
                        foreach (var customerId in selectedCustomers)
                        {
                            var customer = await _context.Customers.FindAsync(customerId);
                            if (customer != null)
                            {
                                existingSchedule.Customers.Add(customer);
                            }
                        }
                    }
                    
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ScheduleExists(schedule.Id))
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
            
            ViewData["ServiceId"] = new SelectList(_context.Services, "Id", "Name", schedule.ServiceId);
            ViewData["Employees"] = new MultiSelectList(_context.Employees, "Id", "Name", selectedEmployees);
            ViewData["Customers"] = new MultiSelectList(_context.Customers, "Id", "Name", selectedCustomers);
            return View(schedule);
        }

        // GET: ManageCare/Schedule/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var schedule = await _context.Schedule
                .Include(s => s.Service)
                .Include(s => s.Employees)
                .Include(s => s.Customers)
                .FirstOrDefaultAsync(m => m.Id == id);
                
            if (schedule == null)
            {
                return NotFound();
            }

            return View(schedule);
        }

        // POST: ManageCare/Schedule/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var schedule = await _context.Schedule.FindAsync(id);
            if (schedule != null)
            {
                _context.Schedule.Remove(schedule);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ScheduleExists(Guid id)
        {
            return _context.Schedule.Any(e => e.Id == id);
        }
    }
} 