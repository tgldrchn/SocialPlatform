namespace SocialPlatform.Interfaces
{
    /// <summary>
    /// Мессеж илгээх чадвартай объектын интерфэйс.
    /// Chat, Channel хэрэгжүүлнэ.
    /// </summary>
    public interface IMessageable
    {
        /// <summary>Мессежүүдийн жагсаалт</summary>
        IReadOnlyList<IMessage> Messages { get; }

        /// <summary>Мессеж илгээх</summary>
        IMessage SendMessage(Guid senderId, string content, IAttachment attachment);
    }
}
