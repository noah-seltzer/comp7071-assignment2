using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Comp7071_A2.Areas.Housing.Models;
using Comp7071_A2.Data;

namespace Comp7071_A2.Areas.Housing.Controllers
{
    [Area("Housing")]
    public class LockersController : Controller
    {
        private readonly ApplicationDbContext _context;

        public LockersController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Housing/Lockers
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Lockers.Include(l => l.Asset).Include(l => l.Suite);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Housing/Lockers/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var locker = await _context.Lockers
                .Include(l => l.Asset)
                .Include(l => l.Suite)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (locker == null)
            {
                return NotFound();
            }

            return View(locker);
        }

        // GET: Housing/Lockers/Create
        public IActionResult Create()
        {
            ViewData["AssetID"] = new SelectList(_context.Assets, "ID", "ID");
            ViewData["SuiteID"] = new SelectList(_context.Suites, "ID", "ID");
            return View();
        }

        // POST: Housing/Lockers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,AssetID,SuiteID,LockerNumber,LockerSize")] Locker locker)
        {
            if (ModelState.IsValid)
            {
                locker.ID = Guid.NewGuid();
                _context.Add(locker);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AssetID"] = new SelectList(_context.Assets, "ID", "ID", locker.AssetID);
            ViewData["SuiteID"] = new SelectList(_context.Suites, "ID", "ID", locker.SuiteID);
            return View(locker);
        }

        // GET: Housing/Lockers/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var locker = await _context.Lockers.FindAsync(id);
            if (locker == null)
            {
                return NotFound();
            }
            ViewData["AssetID"] = new SelectList(_context.Assets, "ID", "ID", locker.AssetID);
            ViewData["SuiteID"] = new SelectList(_context.Suites, "ID", "ID", locker.SuiteID);
            return View(locker);
        }

        // POST: Housing/Lockers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("ID,AssetID,SuiteID,LockerNumber,LockerSize")] Locker locker)
        {
            if (id != locker.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(locker);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LockerExists(locker.ID))
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
            ViewData["AssetID"] = new SelectList(_context.Assets, "ID", "ID", locker.AssetID);
            ViewData["SuiteID"] = new SelectList(_context.Suites, "ID", "ID", locker.SuiteID);
            return View(locker);
        }

        // GET: Housing/Lockers/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var locker = await _context.Lockers
                .Include(l => l.Asset)
                .Include(l => l.Suite)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (locker == null)
            {
                return NotFound();
            }

            return View(locker);
        }

        // POST: Housing/Lockers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var locker = await _context.Lockers.FindAsync(id);
            if (locker != null)
            {
                _context.Lockers.Remove(locker);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LockerExists(Guid id)
        {
            return _context.Lockers.Any(e => e.ID == id);
        }
    }
}
