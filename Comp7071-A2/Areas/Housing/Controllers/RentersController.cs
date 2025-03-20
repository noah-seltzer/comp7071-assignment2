using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Comp7071_A2.Areas.Housing.Models;
using Comp7071_A2.Data;

namespace Comp7071_A2.Areas.Housing.Controllers
{
    [Area("Housing")]
    public class RentersController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public RentersController(ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Housing/Renters
        public async Task<IActionResult> Index()
        {
            var renters = await _context.Renters
                .Include(r => r.Identity)
                .Include(r => r.Applications)
                .ThenInclude(a => a.Asset)
                .ToListAsync();

            return View(renters);
        }


        // GET: Housing/Renters/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var renter = await _context.Renters
                .Include(r => r.Identity)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (renter == null)
            {
                return NotFound();
            }

            return View(renter);
        }

        // GET: Housing/Renters/Create
        public async Task<IActionResult> Create()
        {
            var renters = await _context.Renters.Select(r => r.IdentityID).ToListAsync();
            var users = await _userManager.Users.ToListAsync();
            var availableUsers = users
                .Where(u => !renters.Contains(u.Id))
                .Select(u => new SelectListItem
                {
                    Value = u.Id,
                    Text = u.Email
                })
                .ToList();

            ViewData["IdentityID"] = new SelectList(availableUsers, "Value", "Text");
            return View();
        }

        // POST: Housing/Renters/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,IdentityID,Name,DateOfBirth,Photo,PhoneNumber")] Renter renter)
        {
            if (ModelState.IsValid)
            {
                renter.ID = Guid.NewGuid();
                _context.Add(renter);
                await _context.SaveChangesAsync();

                var user = await _userManager.FindByIdAsync(renter.IdentityID);
                if (user != null && !await _userManager.IsInRoleAsync(user, "Renter"))
                {
                    await _userManager.AddToRoleAsync(user, "Renter");
                }

                return RedirectToAction(nameof(Index));
            }

            var renters = await _context.Renters.Select(r => r.IdentityID).ToListAsync();
            var users = await _userManager.Users.ToListAsync();
            var availableUsers = users
                .Where(u => !renters.Contains(u.Id))
                .Select(u => new SelectListItem
                {
                    Value = u.Id,
                    Text = u.Email
                })
                .ToList();

            ViewData["IdentityID"] = new SelectList(availableUsers, "Value", "Text");

            return View(renter);
        }

        // GET: Housing/Renters/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var renter = await _context.Renters
                .Include(r => r.Identity)
                .FirstOrDefaultAsync(m => m.ID == id);

            if (renter == null)
            {
                return NotFound();
            }
            ViewData["IdentityID"] = new SelectList(_context.Users, "Id", "Id", renter.IdentityID);
            return View(renter);
        }

        // POST: Housing/Renters/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("ID,IdentityID,Name,DateOfBirth,Photo,PhoneNumber")] Renter renter)
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
            ViewData["IdentityID"] = new SelectList(_context.Users, "Id", "Id", renter.IdentityID);
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
                .Include(r => r.Identity)
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
