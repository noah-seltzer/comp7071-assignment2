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
    public class VehiclesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public VehiclesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Housing/Vehicles
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Vehicles.Include(v => v.ParkingSpot).Include(v => v.Renter);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Housing/Vehicles/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vehicle = await _context.Vehicles
                .Include(v => v.ParkingSpot)
                .Include(v => v.Renter)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (vehicle == null)
            {
                return NotFound();
            }

            return View(vehicle);
        }

        // GET: Housing/Vehicles/Create
        public IActionResult Create()
        {
            ViewData["ParkingSpotID"] = new SelectList(_context.ParkingSpots, "ID", "SpotNumber");
            ViewData["RenterID"] = new SelectList(_context.Renters, "ID", "Name");
            return View();
        }

        // POST: Housing/Vehicles/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,ParkingSpotID,RenterID,VehicleType,LicensePlate")] Vehicle vehicle)
        {
            if (ModelState.IsValid)
            {
                vehicle.ID = Guid.NewGuid();
                _context.Add(vehicle);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ParkingSpotID"] = new SelectList(_context.ParkingSpots, "ID", "SpotNumber", vehicle.ParkingSpotID);
            ViewData["RenterID"] = new SelectList(_context.Renters, "ID", "Name", vehicle.RenterID);
            return View(vehicle);
        }

        // GET: Housing/Vehicles/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vehicle = await _context.Vehicles.FindAsync(id);
            if (vehicle == null)
            {
                return NotFound();
            }
            ViewData["ParkingSpotID"] = new SelectList(_context.ParkingSpots, "ID", "SpotNumber", vehicle.ParkingSpotID);
            ViewData["RenterID"] = new SelectList(_context.Renters, "ID", "Name", vehicle.RenterID);
            return View(vehicle);
        }

        // POST: Housing/Vehicles/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("ID,ParkingSpotID,RenterID,VehicleType,LicensePlate")] Vehicle vehicle)
        {
            if (id != vehicle.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(vehicle);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VehicleExists(vehicle.ID))
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
            ViewData["ParkingSpotID"] = new SelectList(_context.ParkingSpots, "ID", "SpotNumber", vehicle.ParkingSpotID);
            ViewData["RenterID"] = new SelectList(_context.Renters, "ID", "Name", vehicle.RenterID);
            return View(vehicle);
        }

        // GET: Housing/Vehicles/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vehicle = await _context.Vehicles
                .Include(v => v.ParkingSpot)
                .Include(v => v.Renter)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (vehicle == null)
            {
                return NotFound();
            }

            return View(vehicle);
        }

        // POST: Housing/Vehicles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var vehicle = await _context.Vehicles.FindAsync(id);
            if (vehicle != null)
            {
                _context.Vehicles.Remove(vehicle);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool VehicleExists(Guid id)
        {
            return _context.Vehicles.Any(e => e.ID == id);
        }
    }
}
