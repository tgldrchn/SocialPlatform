using SocialNetworkingPlatform.Enums;
using SocialPlatform.Interfaces;
using System;

namespace SocialNetworkingPlatform.Models
{
    /// <summary>
    /// Мессеж, пост, сэтгэгдэл дээрх reaction
    /// </summary>
    public class Reaction : IReaction
    {
        public Guid Id { get; private set; } = Guid.NewGuid();
        public Guid UserId { get; private set; }
        public ReactionType Emoji { get; private set; }
        public DateTime CreatedAt { get; private set; } = DateTime.UtcNow;

        public Reaction(Guid userId, ReactionType emoji)
        {
            UserId = userId;
            Emoji = emoji;
        }

        private Reaction(Guid id, Guid userId, ReactionType emoji, DateTime createdAt)
        {
            Id = id;
            UserId = userId;
            Emoji = emoji;
            CreatedAt = createdAt;
        }

        public static Reaction FromPersistence(
            Guid id,
            Guid userId,
            ReactionType emoji,
            DateTime createdAt)
        {
            return new Reaction(id, userId, emoji, createdAt);
        }

        public override string ToString() =>
            $"{Emoji} by {UserId}";
    }
}