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
    public class AssetsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AssetsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Housing/Assets
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Assets.Include(a => a.Building).Include(a => a.HousingGroup).Include(a => a.Renter);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Housing/Assets/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var asset = await _context.Assets
                .Include(a => a.Building)
                .Include(a => a.HousingGroup)
                .Include(a => a.Renter)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (asset == null)
            {
                return NotFound();
            }

            return View(asset);
        }

        // GET: Housing/Assets/Create
        public IActionResult Create()
        {
            ViewData["BuildingID"] = new SelectList(_context.Buildings, "ID", "ID");
            ViewData["HousingGroupID"] = new SelectList(_context.HousingGroups, "ID", "ID");
            ViewData["RenterID"] = new SelectList(_context.Users, "Id", "Id");
            return View();
        }

        // POST: Housing/Assets/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,HousingGroupID,RenterID,BuildingID,IsAvailable,RentAmount")] Asset asset)
        {
            if (ModelState.IsValid)
            {
                asset.ID = Guid.NewGuid();
                _context.Add(asset);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["BuildingID"] = new SelectList(_context.Buildings, "ID", "ID", asset.BuildingID);
            ViewData["HousingGroupID"] = new SelectList(_context.HousingGroups, "ID", "ID", asset.HousingGroupID);
            ViewData["RenterID"] = new SelectList(_context.Users, "Id", "Id", asset.RenterID);
            return View(asset);
        }

        // GET: Housing/Assets/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var asset = await _context.Assets.FindAsync(id);
            if (asset == null)
            {
                return NotFound();
            }
            ViewData["BuildingID"] = new SelectList(_context.Buildings, "ID", "ID", asset.BuildingID);
            ViewData["HousingGroupID"] = new SelectList(_context.HousingGroups, "ID", "ID", asset.HousingGroupID);
            ViewData["RenterID"] = new SelectList(_context.Users, "Id", "Id", asset.RenterID);
            return View(asset);
        }

        // POST: Housing/Assets/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("ID,HousingGroupID,RenterID,BuildingID,IsAvailable,RentAmount")] Asset asset)
        {
            if (id != asset.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(asset);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AssetExists(asset.ID))
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
            ViewData["BuildingID"] = new SelectList(_context.Buildings, "ID", "ID", asset.BuildingID);
            ViewData["HousingGroupID"] = new SelectList(_context.HousingGroups, "ID", "ID", asset.HousingGroupID);
            ViewData["RenterID"] = new SelectList(_context.Users, "Id", "Id", asset.RenterID);
            return View(asset);
        }

        // GET: Housing/Assets/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var asset = await _context.Assets
                .Include(a => a.Building)
                .Include(a => a.HousingGroup)
                .Include(a => a.Renter)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (asset == null)
            {
                return NotFound();
            }

            return View(asset);
        }

        // POST: Housing/Assets/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var asset = await _context.Assets.FindAsync(id);
            if (asset != null)
            {
                _context.Assets.Remove(asset);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AssetExists(Guid id)
        {
            return _context.Assets.Any(e => e.ID == id);
        }
    }
}
