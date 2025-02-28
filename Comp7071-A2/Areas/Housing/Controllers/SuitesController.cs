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
    public class SuitesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SuitesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Housing/Suites
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Suites.Include(s => s.Asset).Include(s => s.Locker).Include(s => s.ParkingSpot);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Housing/Suites/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var suite = await _context.Suites
                .Include(s => s.Asset)
                .Include(s => s.Locker)
                .Include(s => s.ParkingSpot)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (suite == null)
            {
                return NotFound();
            }

            return View(suite);
        }

        // GET: Housing/Suites/Create
        public IActionResult Create()
        {
            ViewData["AssetID"] = new SelectList(_context.Assets, "ID", "ID");
            ViewData["LockerID"] = new SelectList(_context.Lockers, "ID", "ID");
            ViewData["ParkingSpotID"] = new SelectList(_context.ParkingSpots, "ID", "ID");
            return View();
        }

        // POST: Housing/Suites/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,LockerID,ParkingSpotID,AssetID,UnitNumber,Floor,Occupants,Rooms,Bathrooms")] Suite suite)
        {
            if (ModelState.IsValid)
            {
                suite.ID = Guid.NewGuid();
                _context.Add(suite);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AssetID"] = new SelectList(_context.Assets, "ID", "ID", suite.AssetID);
            ViewData["LockerID"] = new SelectList(_context.Lockers, "ID", "ID", suite.LockerID);
            ViewData["ParkingSpotID"] = new SelectList(_context.ParkingSpots, "ID", "ID", suite.ParkingSpotID);
            return View(suite);
        }

        // GET: Housing/Suites/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var suite = await _context.Suites.FindAsync(id);
            if (suite == null)
            {
                return NotFound();
            }
            ViewData["AssetID"] = new SelectList(_context.Assets, "ID", "ID", suite.AssetID);
            ViewData["LockerID"] = new SelectList(_context.Lockers, "ID", "ID", suite.LockerID);
            ViewData["ParkingSpotID"] = new SelectList(_context.ParkingSpots, "ID", "ID", suite.ParkingSpotID);
            return View(suite);
        }

        // POST: Housing/Suites/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("ID,LockerID,ParkingSpotID,AssetID,UnitNumber,Floor,Occupants,Rooms,Bathrooms")] Suite suite)
        {
            if (id != suite.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(suite);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SuiteExists(suite.ID))
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
            ViewData["AssetID"] = new SelectList(_context.Assets, "ID", "ID", suite.AssetID);
            ViewData["LockerID"] = new SelectList(_context.Lockers, "ID", "ID", suite.LockerID);
            ViewData["ParkingSpotID"] = new SelectList(_context.ParkingSpots, "ID", "ID", suite.ParkingSpotID);
            return View(suite);
        }

        // GET: Housing/Suites/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var suite = await _context.Suites
                .Include(s => s.Asset)
                .Include(s => s.Locker)
                .Include(s => s.ParkingSpot)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (suite == null)
            {
                return NotFound();
            }

            return View(suite);
        }

        // POST: Housing/Suites/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var suite = await _context.Suites.FindAsync(id);
            if (suite != null)
            {
                _context.Suites.Remove(suite);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SuiteExists(Guid id)
        {
            return _context.Suites.Any(e => e.ID == id);
        }
    }
}
