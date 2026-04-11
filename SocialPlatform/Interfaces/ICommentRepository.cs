using System;
using System.Collections.Generic;

namespace SocialPlatform.Interfaces
{
    public interface ICommentRepository
    {
        IComment? GetById(Guid id);
        IReadOnlyList<IComment> GetAll();
        IEnumerable<IComment> GetByPost(Guid postId);
        IEnumerable<IComment> GetByAuthor(Guid authorId);
        void Add(IComment comment);
        void Update(IComment comment);
        void Remove(IComment comment);
    }
}