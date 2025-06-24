using ForumProject.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ForumProject.Controllers
{
    [Authorize(Roles = "Moderator, Admin")]
    [ApiController]
    [Route("api/[controller]")]
    public class ModeratorController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ModeratorController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet("flagged")]
        public async Task<IActionResult> GetFlaggedComments()
        {
            var comments = await _context.Comments
                .Where(c => c.IsFlagged && !c.IsApproved)
                .Include(c => c.User)
                .ToListAsync();

            return Ok(comments);
        }

        [HttpPost("approve/{id}")]
        public async Task<IActionResult> ApproveComment(int id)
        {
            var comment = await _context.Comments.FindAsync(id);
            if (comment == null)
                return NotFound();

            comment.IsApproved = true;
            comment.IsFlagged = false;
            await _context.SaveChangesAsync();

            return Ok(new { message = "Comment approved and published." });
        }

        [HttpPost("delete/{id}")]
        public async Task<IActionResult> DeleteComment(int id)
        {
            var comment = await _context.Comments.FindAsync(id);
            if (comment == null)
                return NotFound();

            _context.Comments.Remove(comment);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Comment deleted." });
        }
    }
}