using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using ForumProject.Data;
using ForumProject.Services;
using ForumProject.Models;

namespace ForumProject.Controllers
{
    [Authorize]
    public class CommentsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SentimentService _sentimentService;

        public CommentsController(ApplicationDbContext context, UserManager<ApplicationUser> userManager, SentimentService sentimentService)
        {
            _context = context;
            _userManager = userManager;
            _sentimentService = sentimentService;
        }

        [HttpPost]
        public async Task<IActionResult> Post(string content)
        {
            var isToxic = _sentimentService.Predict(content);

            var comment = new Comment
            {
                Content = content,
                Sentiment = isToxic ? Sentiment.Negative : Sentiment.Positive,
                IsApproved = !isToxic,
                UserId = _userManager.GetUserId(User)
            };

            _context.Comments.Add(comment);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }

        public IActionResult Index()
        {
            var comments = _context.Comments.Where(c => c.IsApproved).ToList();
            return View(comments);
        }
    }
}
