using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using ForumProject.Models;

namespace ForumProject.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AdminController(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public IActionResult Users()
        {
            var users = _userManager.Users.ToList();
            return View(users);
        }

        [HttpPost]
        public async Task<IActionResult> ToggleStatus(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user != null)
            {
                user.IsActive = !user.IsActive;
                await _userManager.UpdateAsync(user);
            }

            return RedirectToAction("Users");
        }

        [HttpPost]
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
            }

            return RedirectToAction("Users");
        }
    }
}
