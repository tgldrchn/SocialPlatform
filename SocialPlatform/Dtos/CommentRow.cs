namespace SocialNetworkingPlatform.Dtos
{
    public class CommentRow
    {
        public string Id { get; set; } = "";
        public string PostId { get; set; } = "";
        public string AuthorId { get; set; } = "";
        public string Content { get; set; } = "";
        public string CreatedAt { get; set; } = "";
    }
}