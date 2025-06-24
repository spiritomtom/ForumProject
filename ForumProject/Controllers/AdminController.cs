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
        public async Task<IActionResult> Users()
        {
            var users = _userManager.Users.ToList();
            var userRoles = new List<UserWithRolesViewModel>();

            foreach (var user in users)
            {
                var roles = await _userManager.GetRolesAsync(user);
                userRoles.Add(new UserWithRolesViewModel
                {
                    User = user,
                    Roles = roles
                });
            }

            return Ok(userRoles);
        }

        [HttpPost("toggleModerator/{id}")]
        public async Task<IActionResult> ToggleModerator(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
                return NotFound("User not found");

            // Ensure the Moderator role exists
            if (!await _roleManager.RoleExistsAsync("Moderator"))
            {
                var result = await _roleManager.CreateAsync(new IdentityRole("Moderator"));
                if (!result.Succeeded)
                    return BadRequest("Could not create Moderator role");
            }

            var isModerator = await _userManager.IsInRoleAsync(user, "Moderator");

            IdentityResult roleResult;
            if (isModerator)
                roleResult = await _userManager.RemoveFromRoleAsync(user, "Moderator");
            else
                roleResult = await _userManager.AddToRoleAsync(user, "Moderator");

            if (!roleResult.Succeeded)
                return BadRequest(roleResult.Errors);

            return Ok(new { message = "Role updated successfully" });
        }
    }
}