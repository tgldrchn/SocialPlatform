using SocialNetworkingPlatform.Enums;
using SocialNetworkingPlatform.Interfaces;
using SocialNetworkingPlatform.Models;
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
        public override string ToString() =>
            $"{Emoji} by {UserId}";
    }
}
