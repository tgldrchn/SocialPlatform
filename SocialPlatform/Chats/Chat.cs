using SocialNetworkingPlatform.Models;
using SocialPlatform.Interfaces;

namespace SocialNetworkingPlatform.Chats;

public abstract class Chat: IChat
{
    private readonly List<IMessage> _messages = new();
    public Guid Id { get; private set; } = Guid.NewGuid();
    public DateTime CreatedAt { get; private set; } = DateTime.UtcNow;
    public IReadOnlyList<IMessage> Messages => _messages;

  
    /// <summary>Мессеж илгээх</summary>
    public virtual IMessage SendMessage(Guid senderId, string content, IAttachment ?attachment)
    {
        var message = new Message(senderId, Id, content, attachment);
        _messages.Add(message);
        return message;
    }
}