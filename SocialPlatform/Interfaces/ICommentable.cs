namespace SocialPlatform.Interfaces
{
    public interface ICommentable
    {
        void AddComment(Guid userId, string content);
    }
}