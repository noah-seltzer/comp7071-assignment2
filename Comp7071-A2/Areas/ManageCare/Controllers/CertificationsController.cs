using Comp7071_A2.Data;
using Comp7071_A2.Areas.ManageCare.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Comp7071_A2.Areas.ManageCare.Controllers
{
    public class CertificationsController : ManageCareController
    {
        private readonly ApplicationDbContext _context;

        public CertificationsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: ManageCare/Certifications
        public async Task<IActionResult> Index()
        {
            return View(await _context.Set<Certification>().ToListAsync());
        }

        // GET: ManageCare/Certifications/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var certification = await _context.Set<Certification>()
                .Include(c => c.Services)
                .Include(c => c.Employees)
                .FirstOrDefaultAsync(m => m.Id == id);
                
            if (certification == null)
            {
                return NotFound();
            }

            return View(certification);
        }

        // GET: ManageCare/Certifications/Create
        public IActionResult Create()
        {
            ViewData["Services"] = new MultiSelectList(_context.Services, "Id", "Name");
            return View();
        }

        // POST: ManageCare/Certifications/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,Description")] Certification certification, Guid[] selectedServices)
        {
            if (ModelState.IsValid)
            {
                certification.Id = Guid.NewGuid();
                certification.Services = new List<Service>(); // Initialize the Services collection
                
                // Add selected services
                if (selectedServices != null && selectedServices.Length > 0)
                {
                    foreach (var serviceId in selectedServices)
                    {
                        var service = await _context.Services.FindAsync(serviceId);
                        if (service != null)
                        {
                            certification.Services.Add(service);
                        }
                    }
                }
                
                _context.Add(certification);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            
            ViewData["Services"] = new MultiSelectList(_context.Services, "Id", "Name");
            return View(certification);
        }

        // GET: ManageCare/Certifications/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var certification = await _context.Set<Certification>()
                .Include(c => c.Services)
                .FirstOrDefaultAsync(c => c.Id == id);
                
            if (certification == null)
            {
                return NotFound();
            }
            
            var selectedServices = certification.Services.Select(s => s.Id).ToArray();
            ViewData["Services"] = new MultiSelectList(_context.Services, "Id", "Name", selectedServices);
            
            return View(certification);
        }

        // POST: ManageCare/Certifications/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,Name,Description")] Certification certification, Guid[] selectedServices)
        {
            if (id != certification.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    // Get the existing certification with its relationships
                    var existingCertification = await _context.Set<Certification>()
                        .Include(c => c.Services)
                        .FirstOrDefaultAsync(c => c.Id == id);
                    
                    if (existingCertification == null)
                    {
                        return NotFound();
                    }
                    
                    // Update scalar properties
                    existingCertification.Name = certification.Name;
                    
                    // Update services
                    existingCertification.Services.Clear();
                    if (selectedServices != null && selectedServices.Length > 0)
                    {
                        foreach (var serviceId in selectedServices)
                        {
                            var service = await _context.Services.FindAsync(serviceId);
                            if (service != null)
                            {
                                existingCertification.Services.Add(service);
                            }
                        }
                    }
                    
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CertificationExists(certification.Id))
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
            
            var selectedServicesIds = await _context.Set<Certification>()
                .Include(c => c.Services)
                .Where(c => c.Id == id)
                .SelectMany(c => c.Services.Select(s => s.Id))
                .ToArrayAsync();
                
            ViewData["Services"] = new MultiSelectList(_context.Services, "Id", "Name", selectedServicesIds);
            return View(certification);
        }

        // GET: ManageCare/Certifications/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var certification = await _context.Set<Certification>()
                .FirstOrDefaultAsync(m => m.Id == id);
                
            if (certification == null)
            {
                return NotFound();
            }

            return View(certification);
        }

        // POST: ManageCare/Certifications/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var certification = await _context.Set<Certification>().FindAsync(id);
            if (certification != null)
            {
                _context.Set<Certification>().Remove(certification);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CertificationExists(Guid id)
        {
            return _context.Set<Certification>().Any(e => e.Id == id);
        }
    }
} 