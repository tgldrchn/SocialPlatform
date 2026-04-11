using System;
using System.Collections.Generic;
using System.Text;

namespace SocialPlatform.Interfaces
{
    public interface IPost
    {
        Guid Id { get; }
        string Content { get; }
        Guid AuthorId { get; }
        DateTime CreatedAt { get; }
        IReadOnlyList<IComment> Comments { get; }
        IReadOnlyList<IReaction> Reactions { get; }
    }
}
