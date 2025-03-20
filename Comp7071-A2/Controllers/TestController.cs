using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using System.Threading.Tasks;
using Comp7071_A2.Data;
using Microsoft.EntityFrameworkCore;

namespace Comp7071_A2.Controllers
{
    [Route("api/test")]
    [ApiController]
    public class TestController : ControllerBase
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ApplicationDbContext _context;

        public TestController(ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        [HttpDelete("delete-user/{email}")]
        public async Task<IActionResult> DeleteUser(string email)
        {
            if (email.StartsWith("testuser"))
            {
                return BadRequest("Only testuser can be deleted");
            }

            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
            {
                return NotFound($"User {email} not found.");
            }

            var result = await _userManager.DeleteAsync(user);
            var result2 = await _context.HREmployees
                .FirstOrDefaultAsync(e => e.UserId == user.Id);
            if (result2 != null)
            {
                _context.HREmployees.Remove(result2);
                await _context.SaveChangesAsync();
            }
            var result3 = await _context.HRManagers
                .FirstOrDefaultAsync(e => e.UserId == user.Id);
            if (result3 != null)
            {
                _context.HRManagers.Remove(result3);
                await _context.SaveChangesAsync();
            }
            var result4 = await _context.Renters
                .FirstOrDefaultAsync(e => e.IdentityID == user.Id);
            if (result4 != null)
            {
                _context.Renters.Remove(result4);
                await _context.SaveChangesAsync();
            }
            if (result.Succeeded)
            {
                return Ok($"User {email} deleted successfully.");
            }

            return BadRequest("Failed to delete user.");
        }
    }
}