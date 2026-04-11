using System;
using System.Collections.Generic;
using SocialNetworkingPlatform.Enums;
using SocialPlatform.Interfaces;

namespace SocialNetworkingPlatform.Models
{
    /// <summary>
    /// Чатын мессеж
    /// </summary>
    public class Message : IMessage
    {
        private readonly List<IReaction> _reactions = new();
        private readonly List<IAttachment> _attachments = new();
        public Guid Id { get; private set; } = Guid.NewGuid();
        public Guid SenderId { get; private set; }
        public Guid ChatId { get; private set; }
        public string Content { get; private set; }
        public bool IsRead { get; private set; }
        public bool IsEdited { get; private set; }

        public DateTime CreatedAt { get; private set; } = DateTime.UtcNow;

        // IMessage
        public IReadOnlyList<IReaction> Reactions => _reactions;
        public IReadOnlyList<IAttachment> Attachments => _attachments;

      
        public Message(Guid senderId, Guid chatId, string content, IAttachment ?attachment)
        {
            SenderId = senderId;
            ChatId = chatId;
            Content = content;
            IsEdited = false;
            CreatedAt = DateTime.UtcNow;
            if (attachment != null)
                _attachments.Add(attachment);
        }

        /// <summary>Уншсанаар тэмдэглэх</summary>
        public void MarkAsRead() => IsRead = true;

        /// <summary>Мессеж засах</summary>
        public void Edit(string newContent)
        {
            Content = newContent;
            IsEdited = true;
        }

        /// <summary>Reaction нэмэх</summary>
        public void AddReaction(Guid userId, ReactionType emoji)
        {
            if (!_reactions.Exists(r => r.UserId == userId && r.Emoji == emoji))
                _reactions.Add(new Reaction(userId, emoji));
        }

        /// <summary>Reaction хасах</summary>
        public void RemoveReaction(Guid userId, ReactionType emoji) =>
            _reactions.RemoveAll(r => r.UserId == userId && r.Emoji == emoji);

        /// <summary>Хавсралт нэмэх</summary>
        public void AddAttachment(IAttachment attachment) =>
            _attachments.Add(attachment);

        public override string ToString() =>
            $"[Message] {Content} {(IsEdited ? "(edited)" : "")} " +
            $"| reactions:{_reactions.Count}";
    }
}
