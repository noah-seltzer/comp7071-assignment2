using Comp7071_A2.Data;
using Comp7071_A2.Areas.ManageCare.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NuGet.Packaging;

namespace Comp7071_A2.Areas.ManageCare.Controllers
{
    public class EmployeesController : ManageCareController
    {
        private readonly ApplicationDbContext _context;

        public EmployeesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: ManageCare/Employees
        public async Task<IActionResult> Index()
        {
            return View(await _context.Employees.Include(e => e.Certifications).ToListAsync());
        }

        // GET: ManageCare/Employees/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employee = await _context.Employees
                .Include(e => e.Certifications)
                .Include(e => e.Schedule)
                .ThenInclude(s => s.Customers)
                .Include(e => e.Schedule)
                .ThenInclude(s => s.Service)

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

            var jobTitles = new List<SelectListItem>
            {
                new SelectListItem { Value = "Manager", Text = "Manager" },
                new SelectListItem { Value = "Employee", Text = "Employee" }
            };
            ViewData["JobTitles"] = new SelectList(jobTitles, "Value", "Text");
            return View();
        }

        // POST: ManageCare/Employees/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,Address,Email,EmergencyContactPhone,JobTitle")] Employee employee, Guid[] selectedCertifications)
        {
            if (ModelState.IsValid)
            {

                var certSet = new HashSet<Guid>(selectedCertifications);
                var certs =
                    (from certification in _context.Certifications
                    where certSet.Any(c => certification.Id == c)
                    select certification).ToList();

                if (certs.Count() != certSet.Count())
                    return NotFound("Certification not found");


                employee.Certifications = certs; 
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

            var employee = await (
                from e in _context.Employees.Include(e => e.Certifications)
                where e.Id == id
                select e
            ).FirstOrDefaultAsync();

            if (employee == null)
            {
                return NotFound();
            }

            foreach (var item in employee.Certifications)
            {
                Console.WriteLine(item);
            }
            ViewData["Certifications"] = new MultiSelectList(_context.Certifications, "Id", "Name", employee.Certifications.Select(c => c.Id));
            return View(employee);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,Name,Address,Email,EmergencyContactPhone,JobTitle")] Employee employee, Guid[] selectedCertifications)
        {
            if (id != employee.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                // Load the employee with their existing certifications
                var employeeToUpdate = await _context.Employees
                    .Include(e => e.Certifications)
                    .FirstOrDefaultAsync(e => e.Id == id);

                if (employeeToUpdate == null)
                {
                    return NotFound();
                }

                // Update scalar properties
                employeeToUpdate.Name = employee.Name;
                employeeToUpdate.Address = employee.Address;
                employeeToUpdate.Email = employee.Email;
                employeeToUpdate.EmergencyContactPhone = employee.EmergencyContactPhone;
                // employeeToUpdate.JobTitle = employee.JobTitle;

                // Get the selected certifications
                var certSet = new HashSet<Guid>(selectedCertifications);
                var certs = await _context.Certifications
                    .Where(c => certSet.Contains(c.Id))
                    .ToListAsync();

                if (certs.Count != certSet.Count)
                {
                    return NotFound("Certification not found");
                }

                // Remove existing certifications that are not in the selected list
                var certificationsToRemove = employeeToUpdate.Certifications
                    .Where(c => !certSet.Contains(c.Id))
                    .ToList();

                foreach (var cert in certificationsToRemove)
                {
                    employeeToUpdate.Certifications.Remove(cert);
                }

                // Add new certifications that are not already associated with the employee
                foreach (var cert in certs)
                {
                    if (!employeeToUpdate.Certifications.Any(c => c.Id == cert.Id))
                    {
                        employeeToUpdate.Certifications.Add(cert);
                    }
                }

                try
                {
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

            // If ModelState is invalid, repopulate the certifications dropdown
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