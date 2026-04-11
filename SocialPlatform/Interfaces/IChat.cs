namespace SocialPlatform.Interfaces
{
    /// <summary>
    /// Чатын үндсэн интерфэйс.
    /// SingleChat, GroupChat хэрэгжүүлнэ.
    /// </summary>
    public interface IChat
    {
        Guid Id { get; }
        DateTime CreatedAt { get; }
        IReadOnlyList<IMessage> Messages { get; }
        IMessage SendMessage(Guid senderId, string content, IAttachment ? attachment);
    }
}