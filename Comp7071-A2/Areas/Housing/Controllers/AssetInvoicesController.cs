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
    public class AssetInvoicesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AssetInvoicesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Housing/AssetInvoices
        public async Task<IActionResult> Index()
        {
            var assetInvoices = await _context.AssetInvoice
                .Include(a => a.Asset)
                .Include(a => a.Renter)
                .Include(a => a.Lines)
                .ToListAsync();
            return View(assetInvoices);
        }

        // GET: Housing/AssetInvoices/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var assetInvoice = await _context.AssetInvoice
                .Include(a => a.Asset)
                .Include(a => a.Renter)
                .Include(a => a.Lines)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (assetInvoice == null)
            {
                return NotFound();
            }

            return View(assetInvoice);
        }

        // GET: Housing/AssetInvoices/Create
        public IActionResult Create()
        {
            ViewData["AssetId"] = new SelectList(_context.Assets, "ID", "ReadableName");
            ViewData["RenterId"] = new SelectList(_context.Renters, "ID", "Name");
            return View();
        }

        [HttpGet]
        public JsonResult GetAssetsByRenter(Guid renterId)
        {
            var assets = _context.Assets
                .Where(a => a.RenterID == renterId)
                .Select(a => new { ID = a.ID, Name = a.ReadableName })
                .ToList();
            return Json(assets);
        }

        [HttpGet]
        public JsonResult GetRentersByAsset(Guid assetId)
        {
            var renter = _context.Renters
                .Where(r => _context.Assets.Any(a => a.ID == assetId && a.RenterID == r.ID))
                .Select(r => new { r.ID, r.Name})
                .FirstOrDefault();
            return Json(renter);
        }

        // POST: Housing/AssetInvoices/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,RenterId,AssetId,StartDate,EndDate")] AssetInvoice assetInvoice)
        {
            if (ModelState.IsValid)
            {
                assetInvoice.Id = Guid.NewGuid();

                var renter = await _context.Renters
                    .Include(r => r.Assets)
                    .FirstOrDefaultAsync(r => r.ID == assetInvoice.RenterId);

                if (renter == null)
                {
                    return NotFound();
                }
                
                // Calculate number of months of rent
                int months = (int)Math.Ceiling((assetInvoice.EndDate - assetInvoice.StartDate).TotalDays / 30);
                
                foreach (var asset in renter.Assets)
                {
                    var line = new AssetInvoiceLine
                    {
                        Id = Guid.NewGuid(),
                        InvoiceId = assetInvoice.Id,
                        Description = asset.ReadableName,
                        Quantity = months,
                        UnitPrice = asset.RentAmount,
                        Amount = months * asset.RentAmount
                    };
                    
                    assetInvoice.Lines.Add(line);
                }
                
                _context.Add(assetInvoice);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AssetId"] = new SelectList(_context.Assets, "ID", "ReadableName", assetInvoice.AssetId);
            ViewData["RenterId"] = new SelectList(_context.Renters, "ID", "Name", assetInvoice.RenterId);
            return View(assetInvoice);
        }

        // GET: Housing/AssetInvoices/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var assetInvoice = await _context.AssetInvoice.FindAsync(id);
            if (assetInvoice == null)
            {
                return NotFound();
            }
            ViewData["AssetId"] = new SelectList(_context.Assets, "ID", "ReadableName", assetInvoice.AssetId);
            ViewData["RenterId"] = new SelectList(_context.Renters, "ID", "Name", assetInvoice.RenterId);
            return View(assetInvoice);
        }

        // POST: Housing/AssetInvoices/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,RenterId,AssetId,StartDate,EndDate")] AssetInvoice assetInvoice)
        {
            if (id != assetInvoice.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(assetInvoice);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AssetInvoiceExists(assetInvoice.Id))
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
            ViewData["AssetId"] = new SelectList(_context.Assets, "ID", "ReadableName", assetInvoice.AssetId);
            ViewData["RenterId"] = new SelectList(_context.Renters, "ID", "Name", assetInvoice.RenterId);
            return View(assetInvoice);
        }

        // GET: Housing/AssetInvoices/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var assetInvoice = await _context.AssetInvoice
                .Include(a => a.Asset)
                .Include(a => a.Renter)
                .Include(a => a.Lines)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (assetInvoice == null)
            {
                return NotFound();
            }

            return View(assetInvoice);
        }

        // POST: Housing/AssetInvoices/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var assetInvoice = await _context.AssetInvoice.FindAsync(id);
            if (assetInvoice != null)
            {
                _context.AssetInvoice.Remove(assetInvoice);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AssetInvoiceExists(Guid id)
        {
            return _context.AssetInvoice.Any(e => e.Id == id);
        }
    }
}
