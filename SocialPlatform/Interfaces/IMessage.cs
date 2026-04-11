using SocialNetworkingPlatform.Enums;
using System;

namespace SocialPlatform.Interfaces
{
    /// <summary>
    /// Мессежийн интерфэйс.
    /// SingleChat, GroupChat, TextChannel ашиглана.
    /// </summary>
    public interface IMessage
    {
        Guid Id { get; }
        string Content { get; }
        Guid SenderId { get; }
        Guid ChatId { get; }
        bool IsRead { get; }
        bool IsEdited { get; }
        DateTime CreatedAt { get; }
        IReadOnlyList<IReaction> Reactions { get; }
        IReadOnlyList<IAttachment> Attachments { get; }
        void Edit(string newContent);
        void AddReaction(Guid userId, ReactionType emoji);
        void RemoveReaction(Guid userId, ReactionType emoji);
    }
}
