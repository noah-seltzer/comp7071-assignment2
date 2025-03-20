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
            var applicationDbContext = _context.Lockers
                .Include(l => l.Building)
                .Include(l => l.HousingGroup)
                .Include(l => l.Suite)
                .Include(l => l.Renter)
                .ThenInclude(r => r.Identity);
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
                .Include(l => l.Building)
                .Include(l => l.HousingGroup)
                .Include(l => l.Suite)
                .Include(l => l.Renter)
                .ThenInclude(r => r.Identity)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (locker == null)
            {
                return NotFound();
            }

            return View(locker);
        }

        public IActionResult Create()
        {
            // Locker Size Options
            ViewBag.LockerSizeOptions = new List<SelectListItem>
            {
                new SelectListItem { Value = "Small", Text = "Small" },
                new SelectListItem { Value = "Medium", Text = "Medium" },
                new SelectListItem { Value = "Large", Text = "Large" }
            };

            // Housing Group Dropdown
            ViewBag.HousingGroupID = new SelectList(_context.HousingGroups, "ID", "Name");

            // Renter Dropdown (Include "None" option)
            var renters = _context.Renters.Include(r => r.Identity)
                .Select(r => new SelectListItem { Value = r.ID.ToString(), Text = r.Identity.Email })
                .ToList();
            renters.Insert(0, new SelectListItem { Value = "", Text = "None" }); // Allow "None"
            ViewBag.RenterID = new SelectList(renters, "Value", "Text");

            // Buildings Dropdown (Initially empty, updates dynamically)
            ViewBag.BuildingID = new SelectList(Enumerable.Empty<SelectListItem>(), "Value", "Text");

            return View();
        }

        // POST: Housing/Lockers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("LockerNumber,LockerSize,SuiteID,ID,HousingGroupID,RenterID,BuildingID,IsAvailable,RentAmount")] Locker locker)
        {
            if (ModelState.IsValid)
            {
                locker.ID = Guid.NewGuid();
                _context.Add(locker);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["BuildingID"] = new SelectList(_context.Buildings, "ID", "ID", locker.BuildingID);
            ViewData["HousingGroupID"] = new SelectList(_context.HousingGroups, "ID", "ManagerID", locker.HousingGroupID);
            ViewData["RenterID"] = new SelectList(_context.Renters, "ID", "IdentityID", locker.RenterID);
            return View(locker);
        }

        // GET: Housing/Lockers/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var locker = await _context.Lockers
                .Include(l => l.Building)
                .Include(l => l.HousingGroup)
                .Include(l => l.Suite)
                .Include(l => l.Renter)
                // .ThenInclude(r => r.Identity)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (locker == null)
            {
                return NotFound();
            }
            // Locker size dropdown options
            ViewBag.LockerSizeOptions = new List<SelectListItem>
            {
                new SelectListItem { Value = "Small", Text = "Small" },
                new SelectListItem { Value = "Medium", Text = "Medium" },
                new SelectListItem { Value = "Large", Text = "Large" }
            };

            ViewData["HousingGroupID"] = new SelectList(_context.HousingGroups, "ID", "Name", locker.HousingGroupID);
            // Buildings filtered by selected Housing Group
            var buildings = _context.Buildings
                .Where(b => b.HousingGroupID == locker.HousingGroupID)
                .Select(b => new SelectListItem { Value = b.ID.ToString(), Text = b.Address })
                .ToList();
            ViewBag.BuildingID = new SelectList(buildings, "Value", "Text", locker.BuildingID);

            var renters = _context.Renters.Include(r => r.Identity)
                .Select(r => new SelectListItem { Value = r.ID.ToString(), Text = r.Identity.Email })
                .ToList();
            renters.Insert(0, new SelectListItem { Value = "", Text = "None" });
            ViewData["RenterID"] = new SelectList(renters, "Value", "Text", locker.RenterID);

            return View(locker);
        }

        [HttpGet]
        public JsonResult GetBuildingsByHousingGroup(Guid housingGroupId)
        {
            var buildings = _context.Buildings
                .Where(b => b.HousingGroupID == housingGroupId)
                .Select(b => new SelectListItem { Value = b.ID.ToString(), Text = b.Address })
                .ToList();

            return Json(buildings);
        }

        // POST: Housing/Lockers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("LockerNumber,LockerSize,SuiteID,ID,HousingGroupID,RenterID,BuildingID,IsAvailable,RentAmount")] Locker locker)
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
            ViewData["BuildingID"] = new SelectList(_context.Buildings, "ID", "ID", locker.BuildingID);
            ViewData["HousingGroupID"] = new SelectList(_context.HousingGroups, "ID", "ManagerID", locker.HousingGroupID);
            ViewData["RenterID"] = new SelectList(_context.Renters, "ID", "IdentityID", locker.RenterID);
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
                .Include(l => l.Building)
                .Include(l => l.HousingGroup)
                .Include(l => l.Suite)
                .Include(l => l.Renter)
                .ThenInclude(r => r.Identity)
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
