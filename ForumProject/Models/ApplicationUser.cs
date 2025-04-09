using Microsoft.AspNetCore.Identity;

namespace ForumProject.Models
{
    public class ApplicationUser : IdentityUser
    {
        public bool IsActive { get; set; } = true;
    }
}
