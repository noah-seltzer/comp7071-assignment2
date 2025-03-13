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
    public class AssetDamagesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AssetDamagesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Housing/AssetDamages
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.AssetDamages.Include(a => a.Asset).Include(a => a.Renter).Include(a => a.DamageImages);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Housing/AssetDamages/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var assetDamage = await _context.AssetDamages
                .Include(a => a.Asset)
                .Include(a => a.Renter)
                .Include(a => a.DamageImages)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (assetDamage == null)
            {
                return NotFound();
            }

            return View(assetDamage);
        }

        // GET: Housing/AssetDamages/Create
        public IActionResult Create()
        {
            ViewData["AssetID"] = new SelectList(_context.Assets, "ID", "AssetType2");
            ViewData["RenterID"] = new SelectList(_context.Renters, "ID", "Name");
            return View();
        }

        // POST: Housing/AssetDamages/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,AssetID,RenterID,Description,RecordedDate,FixedDate")] AssetDamage assetDamage, IFormFileCollection? damagePhotos)
        {
            if (ModelState.IsValid)
            {
                assetDamage.ID = Guid.NewGuid();
                _context.Add(assetDamage);
                await _context.SaveChangesAsync();
                if (damagePhotos == null || damagePhotos.Count == 0)
                {
                    return RedirectToAction("Index");
                }

                foreach (var photo in damagePhotos)
                {
                    using (var memoryStream = new MemoryStream())
                    {
                        await photo.CopyToAsync(memoryStream);

                        var damageImage = new DamageImage
                        {
                            AssetDamageID = assetDamage.ID,
                            Photo = memoryStream.ToArray(),
                        };

                        _context.DamageImages.Add(damageImage);
                    }
                }
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }
            ViewData["AssetID"] = new SelectList(_context.Assets, "ID", "AssetType2", assetDamage.AssetID);
            ViewData["RenterID"] = new SelectList(_context.Renters, "ID", "IdentityID", assetDamage.RenterID);
            return View(assetDamage);
        }

        // GET: Housing/AssetDamages/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var assetDamage = await _context.AssetDamages
                .Include(a => a.DamageImages)
                .FirstOrDefaultAsync(a => a.ID == id);
            if (assetDamage == null)
            {
                return NotFound();
            }
            ViewData["AssetID"] = new SelectList(_context.Assets, "ID", "AssetType2", assetDamage.AssetID);
            ViewData["RenterID"] = new SelectList(_context.Renters, "ID", "Name", assetDamage.RenterID);
            return View(assetDamage);
        }

        // POST: Housing/AssetDamages/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("ID,AssetID,RenterID,Description,RecordedDate,FixedDate")] AssetDamage assetDamage, IFormFileCollection damagePhotos)
        {
            if (id != assetDamage.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(assetDamage);
                    await _context.SaveChangesAsync();

                    if (damagePhotos == null || damagePhotos.Count == 0)
                    {
                        return RedirectToAction("Index");
                    }

                    foreach (var photo in damagePhotos)
                    {
                        using (var memoryStream = new MemoryStream())
                        {
                            await photo.CopyToAsync(memoryStream);

                            var damageImage = new DamageImage
                            {
                                AssetDamageID = assetDamage.ID,
                                Photo = memoryStream.ToArray(),
                            };
                            _context.DamageImages.Add(damageImage);
                        }
                    }
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AssetDamageExists(assetDamage.ID))
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
            ViewData["AssetID"] = new SelectList(_context.Assets, "ID", "AssetType", assetDamage.AssetID);
            ViewData["RenterID"] = new SelectList(_context.Renters, "ID", "IdentityID", assetDamage.RenterID);
            return View(assetDamage);
        }

        // GET: Housing/AssetDamages/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var assetDamage = await _context.AssetDamages
                .Include(a => a.Asset)
                .Include(a => a.Renter)
                .Include(a => a.DamageImages)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (assetDamage == null)
            {
                return NotFound();
            }

            return View(assetDamage);
        }

        // POST: Housing/AssetDamages/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var assetDamage = await _context.AssetDamages.FindAsync(id);
            if (assetDamage != null)
            {
                _context.AssetDamages.Remove(assetDamage);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AssetDamageExists(Guid id)
        {
            return _context.AssetDamages.Any(e => e.ID == id);
        }
    }
}
