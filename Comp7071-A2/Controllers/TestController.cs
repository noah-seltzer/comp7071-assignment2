using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc; 
using Microsoft.AspNetCore.Identity; 
using Microsoft.AspNetCore.Authorization;
using System.Threading.Tasks;

namespace Comp7071_A2.Controllers
{
    [Route("api/test")]
    [ApiController]
    public class TestController : ControllerBase
    {
        private readonly UserManager<IdentityUser> _userManager;

        public TestController(UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
        }

        [HttpDelete("delete-user/{email}")]
        public async Task<IActionResult> DeleteUser(string email)
        {
            if (email != "testuser4@housing.com")
            {
                return BadRequest("Only testuser can be deleted");
            }

            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
            {
                return NotFound($"User {email} not found.");
            }

            var result = await _userManager.DeleteAsync(user);
            if (result.Succeeded)
            {
                return Ok($"User {email} deleted successfully.");
            }

            return BadRequest("Failed to delete user.");
        }
    }
}