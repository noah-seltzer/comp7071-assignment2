using Comp7071_A2.Data;
using Comp7071_A2.Areas.ManageCare.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Comp7071_A2.Areas.ManageCare.Controllers
{
    public class ServicesController : ManageCareController
    {
        private readonly ApplicationDbContext _context;

        public ServicesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: ManageCare/Services
        public async Task<IActionResult> Index()
        {
            return View(await _context.Services.Include(s => s.Certifications).ToListAsync());
        }

        // GET: ManageCare/Services/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var service = await _context.Services
                .Include(s => s.Schedule)
                .Include(s => s.Certifications)
                .FirstOrDefaultAsync(m => m.Id == id);
                
            if (service == null)
            {
                return NotFound();
            }

            return View(service);
        }

        // GET: ManageCare/Services/Create
        public IActionResult Create()
        {
            ViewData["Certifications"] = new MultiSelectList(_context.Set<Certification>(), "Id", "Name");
            return View();
        }

        // POST: ManageCare/Services/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,Description,Rate")] Service service, Guid[] selectedCertifications)
        {
            if (ModelState.IsValid)
            {
                service.Id = Guid.NewGuid();
                service.Certifications = new List<Certification>();

                // Add selected certifications
                if (selectedCertifications != null && selectedCertifications.Length > 0)
                {
                    foreach (var certId in selectedCertifications)
                    {
                        var cert = await _context.Set<Certification>().FindAsync(certId);
                        if (cert != null)
                        {
                            service.Certifications.Add(cert);
                        }
                    }
                }

                _context.Add(service);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Certifications"] = new MultiSelectList(_context.Set<Certification>(), "Id", "Name");
            return View(service);
        }

        // GET: ManageCare/Services/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var service = await _context.Services
                .Include(s => s.Certifications)
                .FirstOrDefaultAsync(s => s.Id == id);
                
            if (service == null)
            {
                return NotFound();
            }

            var selectedCertifications = service.Certifications?.Select(c => c.Id).ToArray() ?? Array.Empty<Guid>();
            ViewData["Certifications"] = new MultiSelectList(_context.Set<Certification>(), "Id", "Name", selectedCertifications);
            return View(service);
        }

        // POST: ManageCare/Services/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,Name,Description,Rate")] Service service, Guid[] selectedCertifications)
        {
            if (id != service.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var existingService = await _context.Services
                        .Include(s => s.Certifications)
                        .FirstOrDefaultAsync(s => s.Id == id);

                    if (existingService == null)
                    {
                        return NotFound();
                    }

                    // Update scalar properties
                    existingService.Name = service.Name;
                    existingService.Description = service.Description;
                    existingService.Rate = service.Rate;

                    // Update certifications
                    existingService.Certifications.Clear();
                    if (selectedCertifications != null && selectedCertifications.Length > 0)
                    {
                        foreach (var certId in selectedCertifications)
                        {
                            var cert = await _context.Set<Certification>().FindAsync(certId);
                            if (cert != null)
                            {
                                existingService.Certifications.Add(cert);
                            }
                        }
                    }

                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ServiceExists(service.Id))
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

            ViewData["Certifications"] = new MultiSelectList(_context.Set<Certification>(), "Id", "Name", selectedCertifications);
            return View(service);
        }

        // GET: ManageCare/Services/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var service = await _context.Services
                .FirstOrDefaultAsync(m => m.Id == id);
            if (service == null)
            {
                return NotFound();
            }

            return View(service);
        }

        // POST: ManageCare/Services/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var service = await _context.Services.FindAsync(id);
            if (service != null)
            {
                _context.Services.Remove(service);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ServiceExists(Guid id)
        {
            return _context.Services.Any(e => e.Id == id);
        }
    }
} 