using SocialNetworkingPlatform.Enums;

namespace SocialNetworkingPlatform.Interfaces
{
    /// <summary>
    /// Reaction хүлээн авах чадвартай объектын интерфэйс.
    /// Post, Message, Comment хэрэгжүүлнэ.
    /// </summary>
    public interface IReactable
    {
        void AddReaction(Guid userId, ReactionType emoji);
        void RemoveReaction(Guid userId, ReactionType emoji);
        int GetReactionCount(ReactionType emoji);
        bool HasReacted(Guid userId, ReactionType emoji);
    }
}