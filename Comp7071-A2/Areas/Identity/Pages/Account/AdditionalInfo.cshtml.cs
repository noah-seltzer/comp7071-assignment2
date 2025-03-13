using Comp7071_A2.Areas.ManageCare.Models;
using Comp7071_A2.Areas.ManageHumanResourcesAndPayroll.Models;
using Comp7071_A2.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Comp7071_A2.Areas.Identity.Pages.Account
{
    public class AdditionalInfoModel : PageModel
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ApplicationDbContext _context;

        [BindProperty]
        public HREmployee Employee { get; set; }

        [BindProperty]
        [BindNever]
        public string UserId { get; set; }

        [TempData]
        public string StatusMessage { get; set; }

        public AdditionalInfoModel(UserManager<IdentityUser> userManager, ApplicationDbContext context)
        {
            _userManager = userManager;
            _context = context;
            Employee = new HREmployee();
        }

        public async Task<IActionResult> OnGetAsync(string userId)
        {
            if (!string.IsNullOrEmpty(userId))
            {
                UserId = userId;
                TempData["UserId"] = userId;
            }
            else if (TempData["UserId"] != null)
            {
                UserId = TempData["UserId"].ToString();
                TempData.Keep("UserId");
            }

            if (string.IsNullOrEmpty(UserId))
            {
                ModelState.AddModelError(string.Empty, "User ID is missing. Please start the registration process again.");
                return Page();
            }

            var user = await _userManager.FindByIdAsync(UserId);
            if (user == null)
            {
                ModelState.AddModelError(string.Empty, "User not found. Please start the registration process again.");
                return Page();
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var userIdFromForm = Request.Form["UserId"].ToString();

            if (string.IsNullOrEmpty(UserId) && !string.IsNullOrEmpty(userIdFromForm))
            {
                UserId = userIdFromForm;
            }
            else if (string.IsNullOrEmpty(UserId) && TempData["UserId"] != null)
            {
                UserId = TempData["UserId"].ToString();
                TempData.Keep("UserId");
            }

            if (string.IsNullOrEmpty(Employee.Name))
                ModelState.AddModelError("Employee.Name", "Name is required");

            if (string.IsNullOrEmpty(Employee.Adderess))
                ModelState.AddModelError("Employee.Adderess", "Address is required");

            if (string.IsNullOrEmpty(Employee.Emergency_Contact))
                ModelState.AddModelError("Employee.Emergency_Contact", "Emergency Contact is required");

            if (string.IsNullOrEmpty(Employee.Job_Title))
                ModelState.AddModelError("Employee.Job_Title", "Job Title is required");

            if (string.IsNullOrEmpty(Employee.Employment_Type))
                ModelState.AddModelError("Employee.Employment_Type", "Employment Type is required");

            if (string.IsNullOrEmpty(UserId))
            {
                ModelState.AddModelError(string.Empty, "User ID is missing. Please start registration again.");
                return Page();
            }

            if (!ModelState.IsValid)
            {
                return Page();
            }

            var user = await _userManager.FindByIdAsync(UserId);

            if (user == null)
            {
                ModelState.AddModelError(string.Empty, "User not found. Please start registration again.");
                StatusMessage = "Error: User not found. Please start registration again.";
                return Page();
            }

            try
            {
                bool isManager = Employee.Job_Title?.ToLower() == "manager";

                using (var transaction = await _context.Database.BeginTransactionAsync())
                {
                    if (isManager)
                    {
                        var manager = new HRManager
                        {
                            ID = Guid.NewGuid(),
                            Name = Employee.Name,
                            Adderess = Employee.Adderess,
                            Emergency_Contact = Employee.Emergency_Contact,
                            Employment_Type = Employee.Employment_Type,
                            Job_Title = "Manager",
                            UserId = UserId
                        };

                        _context.HRManagers.Add(manager);
                    }
                    else
                    {
                        Employee.ID = Guid.NewGuid();
                        Employee.UserId = UserId;

                        _context.HREmployees.Add(Employee);
                    }

                    await _context.SaveChangesAsync();
                    await transaction.CommitAsync();
                }

                StatusMessage = "Your information has been successfully saved.";
                return RedirectToPage("/Index");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, "An error occurred while saving your information: " + ex.Message);
                StatusMessage = "Error: " + ex.Message;
                return Page();
            }
        }
    }
}