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
    public class DamageImagesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public DamageImagesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Housing/DamageImages
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.DamageImages.Include(d => d.AssetDamage);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Housing/DamageImages/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var damageImage = await _context.DamageImages
                .Include(d => d.AssetDamage)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (damageImage == null)
            {
                return NotFound();
            }

            return View(damageImage);
        }

        // GET: Housing/DamageImages/Create
        public IActionResult Create()
        {
            ViewData["AssetDamageID"] = new SelectList(_context.AssetDamages, "ID", "ID");
            return View();
        }

        // POST: Housing/DamageImages/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,AssetDamageID,Photo")] DamageImage damageImage)
        {
            if (ModelState.IsValid)
            {
                damageImage.ID = Guid.NewGuid();
                _context.Add(damageImage);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AssetDamageID"] = new SelectList(_context.AssetDamages, "ID", "ID", damageImage.AssetDamageID);
            return View(damageImage);
        }

        // GET: Housing/DamageImages/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var damageImage = await _context.DamageImages.FindAsync(id);
            if (damageImage == null)
            {
                return NotFound();
            }
            ViewData["AssetDamageID"] = new SelectList(_context.AssetDamages, "ID", "ID", damageImage.AssetDamageID);
            return View(damageImage);
        }

        // POST: Housing/DamageImages/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("ID,AssetDamageID,Photo")] DamageImage damageImage)
        {
            if (id != damageImage.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(damageImage);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DamageImageExists(damageImage.ID))
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
            ViewData["AssetDamageID"] = new SelectList(_context.AssetDamages, "ID", "ID", damageImage.AssetDamageID);
            return View(damageImage);
        }

        // GET: Housing/DamageImages/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var damageImage = await _context.DamageImages
                .Include(d => d.AssetDamage)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (damageImage == null)
            {
                return NotFound();
            }

            return View(damageImage);
        }

        // POST: Housing/DamageImages/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var damageImage = await _context.DamageImages.FindAsync(id);
            if (damageImage != null)
            {
                _context.DamageImages.Remove(damageImage);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DamageImageExists(Guid id)
        {
            return _context.DamageImages.Any(e => e.ID == id);
        }
    }
}
