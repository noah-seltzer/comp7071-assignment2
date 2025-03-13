using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Comp7071_A2.Areas.ManageHumanResourcesAndPayroll.Models;
using Comp7071_A2.Data;

namespace Comp7071_A2.Areas.ManageHumanResourcesAndPayroll.Controllers
{
    [Area("ManageHumanResourcesAndPayroll")]
    public class HRShiftsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public HRShiftsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: ManageHumanResourcesAndPayroll/HRShifts
        public async Task<IActionResult> Index()
        {
            return View(await _context.Shifts.ToListAsync());
        }

        // GET: ManageHumanResourcesAndPayroll/HRShifts/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var shift = await _context.Shifts
                .FirstOrDefaultAsync(m => m.ID == id);
            if (shift == null)
            {
                return NotFound();
            }

            return View(shift);
        }

        // GET: ManageHumanResourcesAndPayroll/HRShifts/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ManageHumanResourcesAndPayroll/HRShifts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Hours_Worked,Hours_Scheduled,Start_Time,End_Time,Status")] Shift shift)
        {
            if (ModelState.IsValid)
            {
                shift.ID = Guid.NewGuid();
                _context.Add(shift);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(shift);
        }

        // GET: ManageHumanResourcesAndPayroll/HRShifts/Edit/5
        public async Task<IActionResult> Edit(Guid? id, [FromQuery] string? returnURL)
        {
            if (id == null)
            {
                return NotFound();
            }

            var shift = await _context.Shifts.FindAsync(id);
            if (shift == null)
            {
                return NotFound();
            }
            if (returnURL != null)
            {
                ViewBag.returnURL = returnURL;
            }
            return View(shift);
        }

        // POST: ManageHumanResourcesAndPayroll/HRShifts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [FromQuery] string? returnID, [FromQuery] string? returnMethod, [FromQuery] string? returnController, [Bind("ID,Hours_Worked,Hours_Scheduled,Start_Time,End_Time,Status")] Shift shift)
        {
            if (id != shift.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(shift);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ShiftExists(shift.ID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                if (returnController != null && returnMethod != null)
                {
                    return RedirectToAction(returnMethod, returnController);
                }
                return RedirectToAction(nameof(Index));
            }
            return View(shift);
        }

        // GET: ManageHumanResourcesAndPayroll/HRShifts/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var shift = await _context.Shifts
                .FirstOrDefaultAsync(m => m.ID == id);
            if (shift == null)
            {
                return NotFound();
            }

            return View(shift);
        }

        // POST: ManageHumanResourcesAndPayroll/HRShifts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var shift = await _context.Shifts.FindAsync(id);
            if (shift != null)
            {
                _context.Shifts.Remove(shift);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ShiftExists(Guid id)
        {
            return _context.Shifts.Any(e => e.ID == id);
        }
    }
}
