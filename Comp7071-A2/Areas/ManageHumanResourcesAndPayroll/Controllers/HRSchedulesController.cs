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
    public class HRSchedulesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public HRSchedulesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: ManageHumanResourcesAndPayroll/HRSchedules
        public async Task<IActionResult> Index()
        {
            return View(await _context.HRSchedules.ToListAsync());
        }

        // GET: ManageHumanResourcesAndPayroll/HRSchedules/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var hRSchedule = await _context.HRSchedules
                .FirstOrDefaultAsync(m => m.ID == id);
            if (hRSchedule == null)
            {
                return NotFound();
            }

            return View(hRSchedule);
        }

        // GET: ManageHumanResourcesAndPayroll/HRSchedules/Create
        public IActionResult Create()
        {

            return View();
        }

        // POST: ManageHumanResourcesAndPayroll/HRSchedules/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Start_Date,End_Date,Hours_Scheduled,Status,Recurrance")] HRSchedule hRSchedule)
        {
            if (ModelState.IsValid)
            {
                hRSchedule.ID = Guid.NewGuid();
                _context.Add(hRSchedule);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(hRSchedule);
        }

        // GET: ManageHumanResourcesAndPayroll/HRSchedules/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var hRSchedule = await _context.HRSchedules.FindAsync(id);
            if (hRSchedule == null)
            {
                return NotFound();
            }
            return View(hRSchedule);
        }

        // POST: ManageHumanResourcesAndPayroll/HRSchedules/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("ID,Start_Date,End_Date,Hours_Scheduled,Status,Recurrance")] HRSchedule hRSchedule)
        {
            if (id != hRSchedule.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(hRSchedule);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!HRScheduleExists(hRSchedule.ID))
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
            return View(hRSchedule);
        }

        // GET: ManageHumanResourcesAndPayroll/HRSchedules/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var hRSchedule = await _context.HRSchedules
                .FirstOrDefaultAsync(m => m.ID == id);
            if (hRSchedule == null)
            {
                return NotFound();
            }

            return View(hRSchedule);
        }

        // POST: ManageHumanResourcesAndPayroll/HRSchedules/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var hRSchedule = await _context.HRSchedules.FindAsync(id);
            if (hRSchedule != null)
            {
                _context.HRSchedules.Remove(hRSchedule);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool HRScheduleExists(Guid id)
        {
            return _context.HRSchedules.Any(e => e.ID == id);
        }
    }
}
