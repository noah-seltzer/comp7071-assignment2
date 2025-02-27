using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Comp7071_A2.Areas.ManageHumanResourcesAndPayroll.Models;
using Comp7071_A2.Data;
using System.Security.Claims;

namespace Comp7071_A2.Areas.ManageHumanResourcesAndPayroll.Controllers
{
    [Area("ManageHumanResourcesAndPayroll")]
    public class HREmployeesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public HREmployeesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: ManageHumanResourcesAndPayroll/HREmployees
        public async Task<IActionResult> Index()
        {
            return View(await _context.HREmployees.ToListAsync());
        }

        // GET: ManageHumanResourcesAndPayroll/HREmployees/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var hREmployee = await _context.HREmployees
                .FirstOrDefaultAsync(m => m.ID == id);
            if (hREmployee == null)
            {
                return NotFound();
            }

            return View(hREmployee);
        }

        // GET: ManageHumanResourcesAndPayroll/HREmployees/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ManageHumanResourcesAndPayroll/HREmployees/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Name,Adderess,Emergency_Contact,Job_Title,Employment_Type")] HREmployee hREmployee)
        {


            //gets the user making the changes and checks if they're a manager
            var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var currentEmployee = await _context.HREmployees.FirstOrDefaultAsync(e => e.ID.ToString() == currentUserId);

            if (currentEmployee == null || currentEmployee.Job_Title != "Manager")
            {
                ModelState.AddModelError(string.Empty, "Only managers can create new employees.");
                return View(hREmployee);
            }

            if (ModelState.IsValid)
            {
                hREmployee.ID = Guid.NewGuid();
                _context.Add(hREmployee);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(hREmployee);
        }

        // GET: ManageHumanResourcesAndPayroll/HREmployees/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var hREmployee = await _context.HREmployees.FindAsync(id);
            if (hREmployee == null)
            {
                return NotFound();
            }
            return View(hREmployee);
        }

        // POST: ManageHumanResourcesAndPayroll/HREmployees/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("ID,Name,Adderess,Emergency_Contact,Job_Title,Employment_Type")] HREmployee hREmployee)
        {
            if (id != hREmployee.ID)
            {
                return NotFound();
            }


            //gets the user making the changes and checks if they're a manager
            var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var currentEmployee = await _context.HREmployees.FirstOrDefaultAsync(e => e.ID.ToString() == currentUserId);

            if (currentEmployee == null || currentEmployee.Job_Title != "Manager")
            {
                ModelState.AddModelError(string.Empty, "Only managers can edit employee details.");
                return View(hREmployee);
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(hREmployee);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!HREmployeeExists(hREmployee.ID))
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
            return View(hREmployee);
        }

        // GET: ManageHumanResourcesAndPayroll/HREmployees/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var hREmployee = await _context.HREmployees
                .FirstOrDefaultAsync(m => m.ID == id);
            if (hREmployee == null)
            {
                return NotFound();
            }

            return View(hREmployee);
        }

        // POST: ManageHumanResourcesAndPayroll/HREmployees/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {

            //gets the user making the changes and checks if they're a manager
            var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var currentEmployee = await _context.HREmployees.FirstOrDefaultAsync(e => e.ID.ToString() == currentUserId);

            if (currentEmployee == null || currentEmployee.Job_Title != "Manager")
            {
                ModelState.AddModelError(string.Empty, "Only managers can delete employees.");
                return View(await _context.HREmployees.FindAsync(id));
            }

            var hREmployee = await _context.HREmployees.FindAsync(id);
            if (hREmployee != null)
            {
                _context.HREmployees.Remove(hREmployee);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool HREmployeeExists(Guid id)
        {
            return _context.HREmployees.Any(e => e.ID == id);
        }
    }
}
