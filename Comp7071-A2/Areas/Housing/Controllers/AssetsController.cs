using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Comp7071_A2.Data;
using Comp7071_A2.Areas.Housing.Models;

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

        public async Task<IActionResult> Index(string search, string assetType, string sortOrder)
        {
            var assets = _context.Assets
                .Include(a => a.Renter)
                .Include(a => a.HousingGroup)
                .Include(a => a.AssetDamages)
                .Include(a => a.Applications)
                .AsQueryable();

            // Searching across multiple columns
            if (!string.IsNullOrEmpty(search))
            {
                search = search.ToLower();
                assets = assets.Where(a =>
                    (a.Renter != null && EF.Functions.Like(a.Renter.Name.ToLower(), $"%{search}%")) ||
                    (a.HousingGroup != null && EF.Functions.Like(a.HousingGroup.Name.ToLower(), $"%{search}%")) ||
                    (a.HousingGroup != null && EF.Functions.Like(a.HousingGroup.Address.ToLower(), $"%{search}%"))
                );
            }

            // Sorting Logic (Preserve existing filters)
            ViewData["CurrentSort"] = sortOrder;
            ViewData["SearchFilter"] = search;
            ViewData["AssetTypeFilter"] = assetType;

            ViewData["AvailabilitySort"] = sortOrder == "availability_asc" ? "availability_desc" : "availability_asc";
            ViewData["RentSort"] = sortOrder == "rentAmount_asc" ? "rentAmount_desc" : "rentAmount_asc";
            ViewData["RenterSort"] = sortOrder == "renter_asc" ? "renter_desc" : "renter_asc";
            ViewData["HousingGroupSort"] = sortOrder == "housingGroup_asc" ? "housingGroup_desc" : "housingGroup_asc";
            ViewData["AddressSort"] = sortOrder == "address_asc" ? "address_desc" : "address_asc";
            ViewData["AssetTypeSort"] = sortOrder == "assetType_asc" ? "assetType_desc" : "assetType_asc";

            // Sorting Logic
            assets = sortOrder switch
            {
                "availability_asc" => assets.OrderBy(a => a.IsAvailable),
                "availability_desc" => assets.OrderByDescending(a => a.IsAvailable),
                "rentAmount_asc" => assets.OrderBy(a => (double)a.RentAmount),
                "rentAmount_desc" => assets.OrderByDescending(a => (double)a.RentAmount),
                "renter_asc" => assets.OrderBy(a => a.Renter != null ? a.Renter.Name : ""),
                "renter_desc" => assets.OrderByDescending(a => a.Renter != null ? a.Renter.Name : ""),
                "housingGroup_asc" => assets.OrderBy(a => a.HousingGroup != null ? a.HousingGroup.Name : ""),
                "housingGroup_desc" => assets.OrderByDescending(a => a.HousingGroup != null ? a.HousingGroup.Name : ""),
                _ => assets.OrderBy(a => a.ID),
            };

            // This is a custom filter for the AssetType2 property is not part of the database
            var assetList = await assets.ToListAsync();
            assetList = assetList.Where(a => assetType == null || a.AssetType2 == assetType).ToList();
            if (sortOrder != null && sortOrder.StartsWith("assetType"))
            {
                assetList = assetList.OrderBy(a => a.AssetType2).ToList();
                if (sortOrder.EndsWith("_desc"))
                {
                    assetList.Reverse();
                }
            }


            return View(assetList);
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
            ViewData["HousingGroupID"] = new SelectList(_context.HousingGroups, "ID", "ManagerID");
            ViewData["RenterID"] = new SelectList(_context.Renters, "ID", "IdentityID");
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
            ViewData["HousingGroupID"] = new SelectList(_context.HousingGroups, "ID", "ManagerID", asset.HousingGroupID);
            ViewData["RenterID"] = new SelectList(_context.Renters, "ID", "IdentityID", asset.RenterID);
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
            ViewData["HousingGroupID"] = new SelectList(_context.HousingGroups, "ID", "ManagerID", asset.HousingGroupID);
            ViewData["RenterID"] = new SelectList(_context.Renters, "ID", "IdentityID", asset.RenterID);
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
            ViewData["HousingGroupID"] = new SelectList(_context.HousingGroups, "ID", "ManagerID", asset.HousingGroupID);
            ViewData["RenterID"] = new SelectList(_context.Renters, "ID", "IdentityID", asset.RenterID);
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
