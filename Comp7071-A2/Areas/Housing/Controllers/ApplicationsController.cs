using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Comp7071_A2.Data;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Comp7071_A2.Areas.Housing.Models
{
    [Area("Housing")]
    public class ApplicationsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ApplicationsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Housing/Applications
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Applications.Include(a => a.Renter);
            return View(await applicationDbContext.ToListAsync());
        }


        // GET: Housing/Applications/RenterApplications/5
        public async Task<IActionResult> RenterApplications(Guid? renterId)
        {
            if (renterId == null)
            {
                return NotFound();
            }

            var applications = await _context.Applications
                .Include(a => a.Asset)
                .Where(a => a.RenterID == renterId)
                .ToListAsync();

            if (!applications.Any())
            {
                return View("NoApplications"); 
            }

            return View(applications);
        }


        // GET: Housing/Applications/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var application = await _context.Applications
                .Include(a => a.Asset)
                .Include(a => a.Renter)
                .FirstOrDefaultAsync(m => m.ID == id);

            if (application == null)
            {
                return NotFound();
            }

            return View(application);
        }


        // GET: Housing/Applications/Create
        public IActionResult Create()
        {
            ViewData["RenterID"] = new SelectList(_context.Renters, "ID", "Name");
            ViewData["AssetID"] = new SelectList(_context.Assets, "ID", "ReadableName");
            return View();
        }

        // POST: Housing/Applications/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,RenterID,AssetID,Status")] Application application)
        {
            if (ModelState.IsValid)
            {
                var asset = await _context.Assets.FindAsync(application.AssetID);
                if (asset == null)
                {
                    ModelState.AddModelError("AssetID", "Selected Asset does not exist.");
                    ViewData["RenterID"] = new SelectList(_context.Renters, "ID", "Name", application.RenterID);
                    ViewData["AssetID"] = new SelectList(_context.Assets, "ID", "ReadableName", application.AssetID);
                    return View(application);
                }

                application.ID = Guid.NewGuid();
                _context.Add(application);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewData["RenterID"] = new SelectList(_context.Renters, "ID", "Name", application.RenterID);
            ViewData["AssetID"] = new SelectList(_context.Assets, "ID", "ReadableName", application.AssetID);
            return View(application);
        }

        [HttpGet]
        public async Task<IActionResult> GetRentAmount(Guid assetId)
        {
            var asset = await _context.Assets.FindAsync(assetId);
            if (asset == null)
            {
                return NotFound();
            }

            return Json(asset.RentAmount);
        }


        public async Task<IActionResult> ByAsset(Guid assetId)
        {
            var asset = await _context.Assets
                .Include(a => a.Applications)
                .ThenInclude(a => a.Renter) // Include Renter details
                .FirstOrDefaultAsync(a => a.ID == assetId);

            if (asset == null)
            {
                return NotFound();
            }

            return View(asset.Applications);
        }


        // GET: Housing/Applications/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var application = await _context.Applications.FindAsync(id);
            if (application == null)
            {
                return NotFound();
            }
            ViewData["RenterID"] = new SelectList(_context.Renters, "ID", "Email", application.RenterID);
            return View(application);
        }

        // POST: Housing/Applications/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("ID,RenterID,Status")] Application application)
        {
            if (id != application.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(application);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ApplicationExists(application.ID))
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
            ViewData["RenterID"] = new SelectList(_context.Renters, "ID", "Email", application.RenterID);
            return View(application);
        }

        // GET: Housing/Applications/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var application = await _context.Applications
                .Include(a => a.Renter)
                .ThenInclude(r => r.Identity)  
                .FirstOrDefaultAsync(m => m.ID == id);

            if (application == null)
            {
                return NotFound();
            }

            return View(application);
        }


        // POST: Housing/Applications/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var application = await _context.Applications.FindAsync(id);

            if (application != null)
            {
                _context.Applications.Remove(application);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }


        private bool ApplicationExists(Guid id)
        {
            return _context.Applications.Any(e => e.ID == id);
        }
    }
}
