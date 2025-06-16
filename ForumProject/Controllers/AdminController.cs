using ForumProject.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace ForumProject.Controllers
{
    [Authorize(Roles = "Admin")]
    [ApiController]
    [Route("api/[controller]")]
    public class AdminController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AdminController(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        [HttpGet("users")]
        public IActionResult Users()
        {
            var users = _userManager.Users.ToList();
            return Ok(users);
        }

        [HttpPost("toggleModerator/{id}")]
        public async Task<IActionResult> ToggleModerator(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user != null)
            {
                var isModerator = await _userManager.IsInRoleAsync(user, "Moderator");

                if (isModerator)
                    await _userManager.RemoveFromRoleAsync(user, "Moderator");
                else
                    await _userManager.AddToRoleAsync(user, "Moderator");

                return Ok(new { message = "Role updated successfully" });
            }

            return NotFound("User not found");
        }
    }
}