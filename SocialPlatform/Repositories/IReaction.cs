
using SocialNetworkingPlatform.Enums;

namespace SocialNetworkingPlatform.Interfaces
{
    public interface IReaction
    {
        Guid Id { get; }
        DateTime CreatedAt { get; }
        Guid UserId { get; }
        ReactionType Emoji { get; }
    }

}
