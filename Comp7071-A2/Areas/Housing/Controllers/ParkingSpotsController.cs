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
    public class ParkingSpotsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ParkingSpotsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Housing/ParkingSpots
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.ParkingSpots.Include(p => p.Building).Include(p => p.HousingGroup).Include(p => p.Renter);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Housing/ParkingSpots/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var parkingSpot = await _context.ParkingSpots
                .Include(p => p.Building)
                .Include(p => p.HousingGroup)
                .Include(p => p.Renter)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (parkingSpot == null)
            {
                return NotFound();
            }

            return View(parkingSpot);
        }

        // GET: Housing/ParkingSpots/Create
        public IActionResult Create()
        {
            ViewData["BuildingID"] = new SelectList(_context.Buildings, "ID", "ID");
            ViewData["HousingGroupID"] = new SelectList(_context.HousingGroups, "ID", "ManagerID");
            ViewData["RenterID"] = new SelectList(_context.Renters, "ID", "IdentityID");
            return View();
        }

        // POST: Housing/ParkingSpots/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("SpotNumber,SuiteID,VehicleID,ID,HousingGroupID,RenterID,BuildingID,IsAvailable,RentAmount")] ParkingSpot parkingSpot)
        {
            if (ModelState.IsValid)
            {
                parkingSpot.ID = Guid.NewGuid();
                _context.Add(parkingSpot);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["BuildingID"] = new SelectList(_context.Buildings, "ID", "ID", parkingSpot.BuildingID);
            ViewData["HousingGroupID"] = new SelectList(_context.HousingGroups, "ID", "ManagerID", parkingSpot.HousingGroupID);
            ViewData["RenterID"] = new SelectList(_context.Renters, "ID", "IdentityID", parkingSpot.RenterID);
            return View(parkingSpot);
        }

        // GET: Housing/ParkingSpots/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var parkingSpot = await _context.ParkingSpots.FindAsync(id);
            if (parkingSpot == null)
            {
                return NotFound();
            }
            ViewData["BuildingID"] = new SelectList(_context.Buildings, "ID", "ID", parkingSpot.BuildingID);
            ViewData["HousingGroupID"] = new SelectList(_context.HousingGroups, "ID", "ManagerID", parkingSpot.HousingGroupID);
            ViewData["RenterID"] = new SelectList(_context.Renters, "ID", "IdentityID", parkingSpot.RenterID);
            return View(parkingSpot);
        }

        // POST: Housing/ParkingSpots/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("SpotNumber,SuiteID,VehicleID,ID,HousingGroupID,RenterID,BuildingID,IsAvailable,RentAmount")] ParkingSpot parkingSpot)
        {
            if (id != parkingSpot.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(parkingSpot);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ParkingSpotExists(parkingSpot.ID))
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
            ViewData["BuildingID"] = new SelectList(_context.Buildings, "ID", "ID", parkingSpot.BuildingID);
            ViewData["HousingGroupID"] = new SelectList(_context.HousingGroups, "ID", "ManagerID", parkingSpot.HousingGroupID);
            ViewData["RenterID"] = new SelectList(_context.Renters, "ID", "IdentityID", parkingSpot.RenterID);
            return View(parkingSpot);
        }

        // GET: Housing/ParkingSpots/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var parkingSpot = await _context.ParkingSpots
                .Include(p => p.Building)
                .Include(p => p.HousingGroup)
                .Include(p => p.Renter)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (parkingSpot == null)
            {
                return NotFound();
            }

            return View(parkingSpot);
        }

        // POST: Housing/ParkingSpots/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var parkingSpot = await _context.ParkingSpots.FindAsync(id);
            if (parkingSpot != null)
            {
                _context.ParkingSpots.Remove(parkingSpot);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ParkingSpotExists(Guid id)
        {
            return _context.ParkingSpots.Any(e => e.ID == id);
        }
    }
}
