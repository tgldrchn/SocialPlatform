namespace SocialPlatform.Interfaces
{
    public interface IComment
    {
        Guid Id { get; }
        string Content { get; }
        Guid AuthorId { get; }
        DateTime CreatedAt { get; }
        IReadOnlyList<IReaction> Reactions { get; }
    }
}
