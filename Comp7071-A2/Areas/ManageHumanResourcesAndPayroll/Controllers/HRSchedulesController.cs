using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Comp7071_A2.Areas.ManageHumanResourcesAndPayroll.Models;
using Comp7071_A2.Data;
using Comp7071_A2.Areas.ManageHumanResourcesAndPayroll.Enums;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;
using NuGet.Protocol;
using Newtonsoft.Json;

namespace Comp7071_A2.Areas.ManageHumanResourcesAndPayroll.Controllers
{
    [Area("ManageHumanResourcesAndPayroll")]
    public class HRSchedulesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public HRSchedulesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: ManageHumanResourcesAndPayroll/HRSchedules
        public async Task<IActionResult> Index()
        {
            var schedules = await _context.HRSchedules.Include(s => s.Shifts).ToListAsync();
            return View(schedules);
        }

        // GET: ManageHumanResourcesAndPayroll/HRSchedules/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var hRSchedule = await _context.HRSchedules
                .Include(s => s.Shifts)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (hRSchedule == null)
            {
                return NotFound();
            }

            return View(hRSchedule);
        }

        // GET: ManageHumanResourcesAndPayroll/HRSchedules/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ManageHumanResourcesAndPayroll/HRSchedules/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Start_Date,End_Date,Hours_Scheduled,Status,Recurrance,Start_Time,Name")] HRSchedule hRSchedule)
        {

            if (ModelState.IsValid)
            {
                var (StartDate, EndDate) = hRSchedule;

                int result = StartDate.CompareTo(EndDate);
                if (result >= 0)
                {
                    ModelState.AddModelError("End_Date", $"End_Date {EndDate} was before Start_Date {StartDate}");
                    return BadRequest(ModelState);
                }

                var total_scheduled_time = EndDate - StartDate;
                if (total_scheduled_time > new TimeSpan(365, 0, 0, 0, 0))
                {
                    ModelState.AddModelError("End_Date", "Schedule cannot be longer than one year");
                    return BadRequest(ModelState);
                }

                if (hRSchedule.Recurrance == Recurrence.Once && hRSchedule.End_Date != hRSchedule.Start_Date)
                {
                    ModelState.AddModelError("End_Date", "If schedule recurrence is set to once, start date and end date must be the same");
                    return BadRequest(ModelState);
                }

                var CurrentDate = DateOnly.FromDateTime(StartDate);
                var EndDateOnly = DateOnly.FromDateTime(EndDate);


                List<Shift> shifts = new List<Shift>();
                int compare = CurrentDate.CompareTo(EndDateOnly);
                while (compare < 0)
                {

                    Shift shift = new Shift();
                    shift.ID = Guid.NewGuid();
                    shift.Hours_Worked = 0;
                    var shiftStart = CurrentDate.ToDateTime(hRSchedule.Start_Time);
                    var shiftEnd = CurrentDate.ToDateTime(hRSchedule.Start_Time).AddHours(hRSchedule.Hours_Scheduled);

                    shift.Start_Time = shiftStart;
                    shift.End_Time = shiftEnd;
                    shift.Hours_Scheduled = hRSchedule.Hours_Scheduled;
                    shifts.Add(shift);
                    _context.Shifts.Add(shift);
                    switch (hRSchedule.Recurrance)
                    {
                        case Recurrence.Once:
                        case Recurrence.Daily:
                            CurrentDate = CurrentDate.AddDays(1);
                            break;
                        case Recurrence.Weekly:
                            CurrentDate = CurrentDate.AddDays(7);
                            break;
                        case Recurrence.Monthly:
                            CurrentDate = CurrentDate.AddMonths(1);
                            break;
                        case Recurrence.Weekdays:
                            if (CurrentDate.DayOfWeek == DayOfWeek.Friday)
                            {
                                CurrentDate = CurrentDate.AddDays(3);
                            }
                            else
                            {
                                CurrentDate = CurrentDate.AddDays(1);
                            }
                            break;
                    }
                    compare = CurrentDate.CompareTo(EndDateOnly);
                }



                hRSchedule.ID = Guid.NewGuid();
                hRSchedule.Shifts = shifts;
                _context.Add(hRSchedule);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(hRSchedule);
        }

        // GET: ManageHumanResourcesAndPayroll/HRSchedules/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var hRSchedule = await _context.HRSchedules.FindAsync(id);
            if (hRSchedule == null)
            {
                return NotFound();
            }
            return View(hRSchedule);
        }

        // POST: ManageHumanResourcesAndPayroll/HRSchedules/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("ID,Start_Date,End_Date,Hours_Scheduled,Status,Recurrance")] HRSchedule hRSchedule)
        {
            if (id != hRSchedule.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(hRSchedule);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!HRScheduleExists(hRSchedule.ID))
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
            return View(hRSchedule);
        }

        // GET: ManageHumanResourcesAndPayroll/HRSchedules/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var hRSchedule = await _context.HRSchedules
                .FirstOrDefaultAsync(m => m.ID == id);
            if (hRSchedule == null)
            {
                return NotFound();
            }

            return View(hRSchedule);
        }

        // POST: ManageHumanResourcesAndPayroll/HRSchedules/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var hRSchedule = await _context.HRSchedules.Include(s => s.Shifts).FirstOrDefaultAsync(s => s.ID == id);
            if (hRSchedule != null)
            {
                _context.HRSchedules.Remove(hRSchedule);
            }

            foreach (var shift in hRSchedule.Shifts)
            {
                _context.Shifts.Remove(shift);
            }
            hRSchedule.Shifts = [];

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool HRScheduleExists(Guid id)
        {
            return _context.HRSchedules.Any(e => e.ID == id);
        }
    }
}
