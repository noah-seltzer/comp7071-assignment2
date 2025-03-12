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
    public class BuildingsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public BuildingsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Housing/Buildings
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Buildings.Include(b => b.HousingGroup);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Housing/Buildings/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var building = await _context.Buildings
                .Include(b => b.HousingGroup)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (building == null)
            {
                return NotFound();
            }

            return View(building);
        }

        // GET: Housing/Buildings/Create
        public IActionResult Create()
        {
            ViewData["HousingGroupID"] = new SelectList(_context.HousingGroups, "ID", "Name");
            return View();
        }

        // POST: Housing/Buildings/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,HousingGroupID,NumUnits,NumLockers,NumParking,Address,City,PostalCode")] Building building)
        {
            if (ModelState.IsValid)
            {
                building.ID = Guid.NewGuid();
                _context.Add(building);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["HousingGroupID"] = new SelectList(_context.HousingGroups, "ID", "Name", building.HousingGroupID);
            return View(building);
        }

        // GET: Housing/Buildings/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var building = await _context.Buildings.FindAsync(id);
            if (building == null)
            {
                return NotFound();
            }
            ViewData["HousingGroupID"] = new SelectList(_context.HousingGroups, "ID", "Name", building.HousingGroupID);
            return View(building);
        }

        // POST: Housing/Buildings/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("ID,HousingGroupID,NumUnits,NumLockers,NumParking,Address,City,PostalCode")] Building building)
        {
            if (id != building.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(building);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BuildingExists(building.ID))
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
            ViewData["HousingGroupID"] = new SelectList(_context.HousingGroups, "ID", "Name", building.HousingGroupID);
            return View(building);
        }

        // GET: Housing/Buildings/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var building = await _context.Buildings
                .Include(b => b.HousingGroup)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (building == null)
            {
                return NotFound();
            }

            return View(building);
        }

        // POST: Housing/Buildings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var building = await _context.Buildings.FindAsync(id);
            if (building != null)
            {
                _context.Buildings.Remove(building);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BuildingExists(Guid id)
        {
            return _context.Buildings.Any(e => e.ID == id);
        }
    }
}
