using ForumProject.Data;
using ForumProject.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace ForumProject.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CommentController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly SentimentService _sentimentService;

        public CommentController(ApplicationDbContext context, SentimentService sentimentService)
        {
            _context = context;
            _sentimentService = sentimentService;
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> PostComment([FromBody] CommentData model)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null)
                return Unauthorized();

            var isToxic = await _sentimentService.IsToxicAsync(model.Content).ConfigureAwait(false);

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

        [HttpPost("analyze")]
        public async Task<OkObjectResult> Analyze([FromBody] string comment)
        {
            bool isToxic = await _sentimentService.IsToxicAsync(comment).ConfigureAwait(false);
            return Ok(new { isToxic });
        }
    }
}