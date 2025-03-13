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
            var applicationDbContext = _context.Suites
                .Include(s => s.Building)
                .Include(s => s.HousingGroup)
                .Include(s => s.Renter)
                .ThenInclude(r => r.Identity)
                .Include(s => s.Locker)
                .Include(s => s.ParkingSpot);
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
                .Include(s => s.Building)
                .Include(s => s.HousingGroup)
                .Include(s => s.Renter)
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
            ViewData["BuildingID"] = new SelectList(_context.Buildings, "ID", "Address");
            ViewData["HousingGroupID"] = new SelectList(_context.HousingGroups, "ID", "Name");

            var renters = _context.Renters.Include(r => r.Identity)
                .Where(r => r.Identity != null)
                .Select(r => new SelectListItem { Value = r.ID.ToString(), Text = r.Identity.Email })
                .ToList();
            renters.Insert(0, new SelectListItem { Value = "", Text = "None" }); // Add "None" option
            ViewData["RenterID"] = new SelectList(renters, "Value", "Text");

            var lockers = _context.Lockers
                .Select(l => new SelectListItem { Value = l.ID.ToString(), Text = "Locker " + l.LockerNumber })
                .ToList();
            lockers.Insert(0, new SelectListItem { Value = "", Text = "None" }); // Default empty option
            ViewData["LockerID"] = new SelectList(lockers, "Value", "Text");

            var parkingSpots = _context.ParkingSpots
                .Select(p => new SelectListItem { Value = p.ID.ToString(), Text = "Spot " + p.SpotNumber })
                .ToList();
            parkingSpots.Insert(0, new SelectListItem { Value = "", Text = "None" }); // Default empty option
            ViewData["ParkingSpotID"] = new SelectList(parkingSpots, "Value", "Text");

            return View();
        }

        // POST: Housing/Suites/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("LockerID,ParkingSpotID,UnitNumber,Floor,Occupants,Rooms,Bathrooms,ID,HousingGroupID,RenterID,BuildingID,IsAvailable,RentAmount")] Suite suite)
        {
            if (ModelState.IsValid)
            {
                suite.ID = Guid.NewGuid();
                if (suite.RenterID == Guid.Empty)
                {
                    suite.RenterID = null;
                }
                if (suite.LockerID == Guid.Empty)
                {
                    suite.LockerID = null;
                }
                if (suite.ParkingSpotID == Guid.Empty)
                {
                    suite.ParkingSpotID = null;
                }
                _context.Add(suite);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["BuildingID"] = new SelectList(_context.Buildings, "ID", "Address");
            ViewData["HousingGroupID"] = new SelectList(_context.HousingGroups, "ID", "Name");

            var renters = _context.Renters.Include(r => r.Identity)
                .Where(r => r.Identity != null)
                .Select(r => new SelectListItem { Value = r.ID.ToString(), Text = r.Identity.Email })
                .ToList();
            renters.Insert(0, new SelectListItem { Value = "", Text = "None" });
            ViewData["RenterID"] = new SelectList(renters, "Value", "Text");

            var lockers = _context.Lockers
                .Select(l => new SelectListItem { Value = l.ID.ToString(), Text = "Locker " + l.LockerNumber })
                .ToList();
            lockers.Insert(0, new SelectListItem { Value = "", Text = "None" }); 
            ViewData["LockerID"] = new SelectList(lockers, "Value", "Text");

            var parkingSpots = _context.ParkingSpots
                .Select(p => new SelectListItem { Value = p.ID.ToString(), Text = "Spot " + p.SpotNumber })
                .ToList();
            parkingSpots.Insert(0, new SelectListItem { Value = "", Text = "None" }); 
            ViewData["ParkingSpotID"] = new SelectList(parkingSpots, "Value", "Text");

            return View();
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

            ViewData["BuildingID"] = new SelectList(_context.Buildings, "ID", "Address", suite.BuildingID);
            ViewData["HousingGroupID"] = new SelectList(_context.HousingGroups, "ID", "Name", suite.HousingGroupID);

            var renters = _context.Renters.Include(r => r.Identity)
                .Where(r => r.Identity != null)
                .Select(r => new SelectListItem { Value = r.ID.ToString(), Text = r.Identity.Email })
                .ToList();
            renters.Insert(0, new SelectListItem { Value = "", Text = "None" }); 
            ViewData["RenterID"] = new SelectList(renters, "Value", "Text");

            var lockers = _context.Lockers
                .Select(l => new SelectListItem { Value = l.ID.ToString(), Text = "Locker " + l.LockerNumber })
                .ToList();
            lockers.Insert(0, new SelectListItem { Value = "", Text = "None" }); 
            ViewData["LockerID"] = new SelectList(lockers, "Value", "Text");

            var parkingSpots = _context.ParkingSpots
                .Select(p => new SelectListItem { Value = p.ID.ToString(), Text = "Spot " + p.SpotNumber })
                .ToList();
            parkingSpots.Insert(0, new SelectListItem { Value = "", Text = "None" }); 
            ViewData["ParkingSpotID"] = new SelectList(parkingSpots, "Value", "Text");

            return View(suite);
        }

        // POST: Housing/Suites/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("LockerID,ParkingSpotID,UnitNumber,Floor,Occupants,Rooms,Bathrooms,ID,HousingGroupID,RenterID,BuildingID,IsAvailable,RentAmount")] Suite suite)
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

            ViewData["BuildingID"] = new SelectList(_context.Buildings, "ID", "Address", suite.BuildingID);
            ViewData["HousingGroupID"] = new SelectList(_context.HousingGroups, "ID", "Name", suite.HousingGroupID);

            var renters = _context.Renters.Include(r => r.Identity)
                .Where(r => r.Identity != null)
                .Select(r => new SelectListItem { Value = r.ID.ToString(), Text = r.Identity.Email })
                .ToList();
            renters.Insert(0, new SelectListItem { Value = "", Text = "None" }); 
            ViewData["RenterID"] = new SelectList(renters, "Value", "Text");

            var lockers = _context.Lockers
                .Select(l => new SelectListItem { Value = l.ID.ToString(), Text = "Locker " + l.LockerNumber })
                .ToList();
            lockers.Insert(0, new SelectListItem { Value = "", Text = "None" }); 
            ViewData["LockerID"] = new SelectList(lockers, "Value", "Text");

            var parkingSpots = _context.ParkingSpots
                .Select(p => new SelectListItem { Value = p.ID.ToString(), Text = "Spot " + p.SpotNumber })
                .ToList();
            parkingSpots.Insert(0, new SelectListItem { Value = "", Text = "None" }); 
            ViewData["ParkingSpotID"] = new SelectList(parkingSpots, "Value", "Text");

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
                .Include(s => s.Building)
                .Include(s => s.HousingGroup)
                .Include(s => s.Renter)
                .ThenInclude(r => r.Identity)
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
