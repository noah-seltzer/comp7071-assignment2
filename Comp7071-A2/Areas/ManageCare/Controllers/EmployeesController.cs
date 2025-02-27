using Comp7071_A2.Areas.ManageCare.Data;
using Comp7071_A2.Areas.ManageCare.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Comp7071_A2.Areas.ManageCare.Controllers
{
    public class EmployeesController : ManageCareController
    {
        private readonly CareManageMentDBContext _context;

        public EmployeesController(CareManageMentDBContext context)
        {
            _context = context;
        }

        // GET: ManageCare/Employees
        public async Task<IActionResult> Index()
        {
            return View(await _context.Employees.ToListAsync());
        }

        // GET: ManageCare/Employees/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employee = await _context.Employees
                .Include(e => e.Schedule)
                .Include(e => e.Certifications)
                .FirstOrDefaultAsync(m => m.Id == id);
                
            if (employee == null)
            {
                return NotFound();
            }

            return View(employee);
        }

        // GET: ManageCare/Employees/Create
        public IActionResult Create()
        {
            ViewData["Certifications"] = new SelectList(_context.Set<Certification>(), "Id", "Name");
            return View();
        }

        // POST: ManageCare/Employees/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,Address,Email,EmergencyContactPhone,JobTitle")] Employee employee)
        {
            if (ModelState.IsValid)
            {
                employee.Id = Guid.NewGuid();
                _context.Add(employee);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Certifications"] = new SelectList(_context.Set<Certification>(), "Id", "Name");
            return View(employee);
        }

        // GET: ManageCare/Employees/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employee = await _context.Employees.FindAsync(id);
            if (employee == null)
            {
                return NotFound();
            }
            ViewData["Certifications"] = new SelectList(_context.Set<Certification>(), "Id", "Name");
            return View(employee);
        }

        // POST: ManageCare/Employees/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,Name,Address,Email,EmergencyContactPhone,JobTitle")] Employee employee)
        {
            if (id != employee.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(employee);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EmployeeExists(employee.Id))
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
            ViewData["Certifications"] = new SelectList(_context.Set<Certification>(), "Id", "Name");
            return View(employee);
        }

        // GET: ManageCare/Employees/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employee = await _context.Employees
                .Include(e => e.Certifications)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (employee == null)
            {
                return NotFound();
            }

            return View(employee);
        }

        // POST: ManageCare/Employees/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var employee = await _context.Employees.FindAsync(id);
            if (employee != null)
            {
                _context.Employees.Remove(employee);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EmployeeExists(Guid id)
        {
            return _context.Employees.Any(e => e.Id == id);
        }
    }
} 