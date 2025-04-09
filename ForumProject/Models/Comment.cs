namespace ForumProject.Models
{
    public class Comment
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public Sentiment Sentiment { get; set; }
        public bool IsApproved { get; set; }
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }
    }
    public enum Sentiment
{
    Positive,
    Neutral,
    Negative
}
}
