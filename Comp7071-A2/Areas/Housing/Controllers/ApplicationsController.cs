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
            var applicationDbContext = _context.Application.Include(a => a.Renter);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Housing/Applications/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var application = await _context.Application
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
            ViewData["AssetID"] = new SelectList(_context.Assets, "ID", "ID");
            return View();
        }

        // POST: Housing/Applications/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,RenterID,Status")] Application application)
        {
            
            if (ModelState.IsValid)
            {
                // Assign the Renter using RenterID
                if (application.RenterID.HasValue)
                {
                    application.Renter = await _context.Renters.FindAsync(application.RenterID);
                }
                else
                {
                    application.Renter = null;
                }
                
                application.ID = Guid.NewGuid();
                _context.Add(application);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewData["RenterID"] = new SelectList(_context.Renters, "ID", "Name", application.RenterID);
            return View(application);
        }

        // GET: Housing/Applications/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var application = await _context.Application.FindAsync(id);
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

            var application = await _context.Application
                .Include(a => a.Renter)
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
            var application = await _context.Application.FindAsync(id);
            if (application != null)
            {
                _context.Application.Remove(application);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ApplicationExists(Guid id)
        {
            return _context.Application.Any(e => e.ID == id);
        }
    }
}
