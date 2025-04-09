using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using ForumProject.Data;

namespace ForumProject.Controllers
{
    [Authorize(Roles = "Moderator")]
    public class ModeratorController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ModeratorController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Pending()
        {
            var comments = _context.Comments.Where(c => !c.IsApproved).ToList();
            return View(comments);
        }

        [HttpPost]
        public IActionResult Approve(int id)
        {
            var comment = _context.Comments.Find(id);
            if (comment != null)
            {
                comment.IsApproved = true;
                _context.SaveChanges();
            }

            return RedirectToAction("Pending");
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            var comment = _context.Comments.Find(id);
            if (comment != null)
            {
                _context.Comments.Remove(comment);
                _context.SaveChanges();
            }

            return RedirectToAction("Pending");
        }
    }
}
