using ForumProject.Data;
using ForumProject.Models;
using ForumProject.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ForumProject.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CommentController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly SentimentService _sentiment;
        private readonly UserManager<ApplicationUser> _userManager;

        public CommentController(ApplicationDbContext context, SentimentService sentiment, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _sentiment = sentiment;
            _userManager = userManager;
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> PostComment([FromBody] Comment model)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var isToxic = await _sentiment.IsToxicAsync(model.Content);

            var comment = new Comment
            {
                Content = model.Content,
                UserId = userId,
                IsFlagged = isToxic,
                IsApproved = !isToxic
            };

            _context.Comments.Add(comment);
            await _context.SaveChangesAsync();

            return Ok(new
            {
                message = isToxic ? "Comment is pending moderation." : "Comment posted successfully.",
                commentId = comment.Id
            });
        }

        [HttpGet]
        public async Task<IActionResult> GetApprovedComments()
        {
            var comments = await _context.Comments
                .Where(c => c.IsApproved)
                .Include(c => c.User)
                .OrderByDescending(c => c.CreatedAt)
                .Select(c => new
                {
                    c.Id,
                    c.Content,
                    c.CreatedAt,
                    User = c.User.UserName
                })
                .ToListAsync();

            return Ok(comments);
        }
    }
}
