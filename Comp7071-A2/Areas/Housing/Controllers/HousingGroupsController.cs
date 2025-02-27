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
    public class HousingGroupsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public HousingGroupsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Housing/HousingGroups
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.HousingGroups.Include(h => h.Manager);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Housing/HousingGroups/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var housingGroup = await _context.HousingGroups
                .Include(h => h.Manager)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (housingGroup == null)
            {
                return NotFound();
            }

            return View(housingGroup);
        }

        // GET: Housing/HousingGroups/Create
        public IActionResult Create()
        {
            ViewData["ManagerID"] = new SelectList(_context.Users, "Id", "Id");
            return View();
        }

        // POST: Housing/HousingGroups/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,ManagerID,Name,Address,City,PostalCode")] HousingGroup housingGroup)
        {
            if (ModelState.IsValid)
            {
                housingGroup.ID = Guid.NewGuid();
                _context.Add(housingGroup);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ManagerID"] = new SelectList(_context.Users, "Id", "Id", housingGroup.ManagerID);
            return View(housingGroup);
        }

        // GET: Housing/HousingGroups/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var housingGroup = await _context.HousingGroups.FindAsync(id);
            if (housingGroup == null)
            {
                return NotFound();
            }
            ViewData["ManagerID"] = new SelectList(_context.Users, "Id", "Id", housingGroup.ManagerID);
            return View(housingGroup);
        }

        // POST: Housing/HousingGroups/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("ID,ManagerID,Name,Address,City,PostalCode")] HousingGroup housingGroup)
        {
            if (id != housingGroup.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(housingGroup);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!HousingGroupExists(housingGroup.ID))
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
            ViewData["ManagerID"] = new SelectList(_context.Users, "Id", "Id", housingGroup.ManagerID);
            return View(housingGroup);
        }

        // GET: Housing/HousingGroups/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var housingGroup = await _context.HousingGroups
                .Include(h => h.Manager)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (housingGroup == null)
            {
                return NotFound();
            }

            return View(housingGroup);
        }

        // POST: Housing/HousingGroups/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var housingGroup = await _context.HousingGroups.FindAsync(id);
            if (housingGroup != null)
            {
                _context.HousingGroups.Remove(housingGroup);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool HousingGroupExists(Guid id)
        {
            return _context.HousingGroups.Any(e => e.ID == id);
        }
    }
}
