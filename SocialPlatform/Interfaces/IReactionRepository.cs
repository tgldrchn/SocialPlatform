using System;
using System.Collections.Generic;
using SocialNetworkingPlatform.Enums;

namespace SocialPlatform.Interfaces
{
    public interface IReactionRepository
    {
        IReaction? GetById(Guid id);
        IReadOnlyList<IReaction> GetAll();
        IEnumerable<IReaction> GetByUser(Guid userId);
        IEnumerable<IReaction> GetByPost(Guid postId);
        IEnumerable<IReaction> GetByComment(Guid commentId);
        void AddToPost(Guid postId, IReaction reaction);
        void AddToComment(Guid commentId, IReaction reaction);
        void Remove(IReaction reaction);
        void RemoveByUserAndPost(Guid userId, Guid postId, ReactionType emoji);
        void RemoveByUserAndComment(Guid userId, Guid commentId, ReactionType emoji);
    }
}