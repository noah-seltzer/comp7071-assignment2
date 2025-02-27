using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Comp7071_A2.Data;

namespace Comp7071_A2.Areas.Housing.Models
{
    [Area("Housing")]
    public class RentersController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<RentersController> _logger;

        public RentersController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Housing/Renters
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Renters.Include(r => r.Application).Include(r => r.Asset);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Housing/Renters/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var renter = await _context.Renters
                .Include(r => r.Application)
                .Include(r => r.Asset)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (renter == null)
            {
                return NotFound();
            }

            return View(renter);
        }

        // GET: Housing/Renters/Create
        public IActionResult Create()
        {
            ViewData["ApplicationID"] = new SelectList(_context.Application, "ID", "ID");
            ViewData["AssetID"] = new SelectList(_context.Assets, "ID", "ID");
            return View();
        }

        // POST: Housing/Renters/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,ApplicationID,AssetID,Name,DateOfBirth,Photo,PhoneNumber,Email")] Renter renter)
        {
            if (ModelState.IsValid)
            {
                renter.ID = Guid.NewGuid();
                renter.DateOfBirth = renter.DateOfBirth.Date;
                _context.Add(renter);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ApplicationID"] = new SelectList(_context.Application, "ID", "Status", renter.ApplicationID);
            ViewData["AssetID"] = new SelectList(_context.Assets, "ID", "ID", renter.AssetID);
            return View(renter);
        }

        // GET: Housing/Renters/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var renter = await _context.Renters.FindAsync(id);
            if (renter == null)
            {
                return NotFound();
            }
            ViewData["ApplicationID"] = new SelectList(_context.Application, "ID", "Status", renter.ApplicationID);
            ViewData["AssetID"] = new SelectList(_context.Assets, "ID", "ID", renter.AssetID);
            return View(renter);
        }

        // POST: Housing/Renters/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("ID,ApplicationID,AssetID,Name,DateOfBirth,Photo,PhoneNumber,Email")] Renter renter)
        {
            if (id != renter.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(renter);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RenterExists(renter.ID))
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
            ViewData["ApplicationID"] = new SelectList(_context.Application, "ID", "Status", renter.ApplicationID);
            ViewData["AssetID"] = new SelectList(_context.Assets, "ID", "ID", renter.AssetID);
            return View(renter);
        }

        // GET: Housing/Renters/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var renter = await _context.Renters
                .Include(r => r.Application)
                .Include(r => r.Asset)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (renter == null)
            {
                return NotFound();
            }

            return View(renter);
        }

        // POST: Housing/Renters/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var renter = await _context.Renters.FindAsync(id);
            if (renter != null)
            {
                _context.Renters.Remove(renter);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RenterExists(Guid id)
        {
            return _context.Renters.Any(e => e.ID == id);
        }
    }
}
