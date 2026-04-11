using SocialNetworkingPlatform.Enums;

namespace SocialPlatform.Interfaces
{
    public interface IReaction
    {
        Guid Id { get; }
        DateTime CreatedAt { get; }
        Guid UserId { get; }
        ReactionType Emoji { get; }
    }

}
