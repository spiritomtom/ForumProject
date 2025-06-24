namespace ForumProject.Models
{
    public class UserWithRolesViewModel
    {
        public ApplicationUser User { get; set; }
        public IList<string> Roles { get; set; }
    }
}
