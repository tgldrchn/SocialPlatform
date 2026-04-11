namespace SocialNetworkingPlatform.Dtos
{
    public class ReactionRow
    {
        public string Id { get; set; } = "";
        public string PostId { get; set; } = "";
        public string UserId { get; set; } = "";
        public string Emoji { get; set; } = "";
        public string CreatedAt { get; set; } = "";
    }
}