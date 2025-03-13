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
    public class ApplicationReferencesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ApplicationReferencesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Housing/ApplicationReferences
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.ApplicationReferences.Include(a => a.Application).Include(a => a.Contact);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Housing/ApplicationReferences/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var applicationReference = await _context.ApplicationReferences
                .Include(a => a.Application)
                .Include(a => a.Contact)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (applicationReference == null)
            {
                return NotFound();
            }

            return View(applicationReference);
        }

        // GET: Housing/ApplicationReferences/Create
        public IActionResult Create()
        {
            ViewData["ApplicationID"] = new SelectList(_context.Applications, "ID", "RenterID");
            ViewData["ContactID"] = new SelectList(_context.Contacts, "ID", "Email");
            return View();
        }

        // POST: Housing/ApplicationReferences/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,ApplicationID,ContactID")] ApplicationReference applicationReference)
        {
            if (ModelState.IsValid)
            {
                applicationReference.ID = Guid.NewGuid();
                _context.Add(applicationReference);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ApplicationID"] = new SelectList(_context.Applications, "ID", "RenterID", applicationReference.ApplicationID);
            ViewData["ContactID"] = new SelectList(_context.Contacts, "ID", "Email", applicationReference.ContactID);
            return View(applicationReference);
        }

        // GET: Housing/ApplicationReferences/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var applicationReference = await _context.ApplicationReferences.FindAsync(id);
            if (applicationReference == null)
            {
                return NotFound();
            }
            ViewData["ApplicationID"] = new SelectList(_context.Applications, "ID", "RenterID", applicationReference.ApplicationID);
            ViewData["ContactID"] = new SelectList(_context.Contacts, "ID", "Email", applicationReference.ContactID);
            return View(applicationReference);
        }

        // POST: Housing/ApplicationReferences/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("ID,ApplicationID,ContactID")] ApplicationReference applicationReference)
        {
            if (id != applicationReference.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(applicationReference);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ApplicationReferenceExists(applicationReference.ID))
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
            ViewData["ApplicationID"] = new SelectList(_context.Applications, "ID", "RenterID", applicationReference.ApplicationID);
            ViewData["ContactID"] = new SelectList(_context.Contacts, "ID", "Email", applicationReference.ContactID);
            return View(applicationReference);
        }

        // GET: Housing/ApplicationReferences/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var applicationReference = await _context.ApplicationReferences
                .Include(a => a.Application)
                .Include(a => a.Contact)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (applicationReference == null)
            {
                return NotFound();
            }

            return View(applicationReference);
        }

        // POST: Housing/ApplicationReferences/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var applicationReference = await _context.ApplicationReferences.FindAsync(id);
            if (applicationReference != null)
            {
                _context.ApplicationReferences.Remove(applicationReference);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ApplicationReferenceExists(Guid id)
        {
            return _context.ApplicationReferences.Any(e => e.ID == id);
        }
    }
}
