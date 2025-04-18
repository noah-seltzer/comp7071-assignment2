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
            var applicationDbContext = _context.ParkingSpots
                .Include(p => p.Building)
                .Include(p => p.HousingGroup)
                .Include(p => p.Vehicle)
                .Include(p => p.Suite)
                .Include(p => p.Renter)
                .ThenInclude(r => r.Identity);
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
                .Include(p => p.Vehicle)
                .Include(p => p.Suite)
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
            ViewData["BuildingID"] = new SelectList(_context.Buildings, "ID", "Address");
            ViewData["HousingGroupID"] = new SelectList(_context.HousingGroups, "ID", "Name");
            var renters = _context.Renters
                .Select(r => new SelectListItem { Value = r.ID.ToString(), Text = r.Name })
                .ToList();
            renters.Insert(0, new SelectListItem { Value = "", Text = "None" });
            ViewData["RenterID"] = new SelectList(renters, "Value", "Text");
            var suites = _context.Suites
                .Select(s => new SelectListItem { Value = s.ID.ToString(), Text = "Unit " + s.UnitNumber })
                .ToList();
            suites.Insert(0, new SelectListItem { Value = "", Text = "None" });
            ViewData["SuiteID"] = new SelectList(suites, "Value", "Text");

            // Vehicles (Include "None" option)
            var vehicles = _context.Vehicles
                .Select(v => new SelectListItem { Value = v.ID.ToString(), Text = v.LicensePlate })
                .ToList();
            vehicles.Insert(0, new SelectListItem { Value = "", Text = "None" });
            ViewData["VehicleID"] = new SelectList(vehicles, "Value", "Text");
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
            ViewData["BuildingID"] = new SelectList(_context.Buildings, "ID", "Address");
            ViewData["HousingGroupID"] = new SelectList(_context.HousingGroups, "ID", "Name");
            var renters = _context.Renters
                .Select(r => new SelectListItem { Value = r.ID.ToString(), Text = r.Name })
                .ToList();
            renters.Insert(0, new SelectListItem { Value = "", Text = "None" });
            ViewData["RenterID"] = new SelectList(renters, "Value", "Text");
            var suites = _context.Suites
                .Select(s => new SelectListItem { Value = s.ID.ToString(), Text = "Unit " + s.UnitNumber })
                .ToList();
            suites.Insert(0, new SelectListItem { Value = "", Text = "None" });
            ViewData["SuiteID"] = new SelectList(suites, "Value", "Text");

            // Vehicles (Include "None" option)
            var vehicles = _context.Vehicles
                .Select(v => new SelectListItem { Value = v.ID.ToString(), Text = v.LicensePlate })
                .ToList();
            vehicles.Insert(0, new SelectListItem { Value = "", Text = "None" });
            ViewData["VehicleID"] = new SelectList(vehicles, "Value", "Text");
            return View();
        }

        // GET: Housing/ParkingSpots/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var parkingSpot = await _context.ParkingSpots
                .Include(p => p.Building)
                .Include(p => p.HousingGroup)
                .Include(p => p.Vehicle)
                .Include(p => p.Suite)
                .Include(p => p.Renter)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (parkingSpot == null)
            {
                return NotFound();
            }
            ViewData["BuildingID"] = new SelectList(_context.Buildings, "ID", "Address", parkingSpot.BuildingID);
            ViewData["HousingGroupID"] = new SelectList(_context.HousingGroups, "ID", "Name", parkingSpot.HousingGroupID);
            ViewData["RenterID"] = new SelectList(_context.Renters, "ID", "Name", parkingSpot.RenterID);

            // Suites filtered by selected Housing Group
            var suites = _context.Suites
                .Select(s => new SelectListItem { Value = s.ID.ToString(), Text = "Unit " + s.UnitNumber })
                .ToList();
            suites.Insert(0, new SelectListItem { Value = "", Text = "None" });
            ViewData["SuiteID"] = new SelectList(suites, "Value", "Text", parkingSpot.SuiteID);

            // Vehicles (Include "None" option)
            var vehicles = _context.Vehicles
                .Select(v => new SelectListItem { Value = v.ID.ToString(), Text = v.LicensePlate })
                .ToList();
            vehicles.Insert(0, new SelectListItem { Value = "", Text = "None" });
            ViewData["VehicleID"] = new SelectList(vehicles, "Value", "Text", parkingSpot.VehicleID);
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

            ViewData["BuildingID"] = new SelectList(_context.Buildings, "ID", "Address", parkingSpot.BuildingID);
            ViewData["HousingGroupID"] = new SelectList(_context.HousingGroups, "ID", "Name", parkingSpot.HousingGroupID);
            ViewData["RenterID"] = new SelectList(_context.Renters, "ID", "Name", parkingSpot.RenterID);

            // Suites filtered by selected Housing Group
            var suites = _context.Suites
                .Select(s => new SelectListItem { Value = s.ID.ToString(), Text = "Unit " + s.UnitNumber })
                .ToList();
            suites.Insert(0, new SelectListItem { Value = "", Text = "None" });
            ViewData["SuiteID"] = new SelectList(suites, "Value", "Text", parkingSpot.SuiteID);

            // Vehicles (Include "None" option)
            var vehicles = _context.Vehicles
                .Select(v => new SelectListItem { Value = v.ID.ToString(), Text = v.LicensePlate })
                .ToList();
            vehicles.Insert(0, new SelectListItem { Value = "", Text = "None" });
            ViewData["VehicleID"] = new SelectList(vehicles, "Value", "Text", parkingSpot.VehicleID);
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
                .Include(p => p.Vehicle)
                .Include(p => p.Suite)
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
