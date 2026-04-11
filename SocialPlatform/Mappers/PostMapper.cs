using SocialNetworkingPlatform.Enums;
using SocialNetworkingPlatform.Interfaces;
using SocialNetworkingPlatform.Models;
using SocialPlatform.Interfaces;

namespace SocialNetworkingPlatform.Repositories
{
    public static class PostMapper
    {
        public static Post MapPost(
            PostRow postRow,
            IEnumerable<CommentRow> commentRows,
            IEnumerable<ReactionRow> reactionRows)
        {
            var comments = commentRows
                .Select(c => (IComment)Comment.FromPersistence(
                    Guid.Parse(c.Id),
                    Guid.Parse(c.AuthorId),
                    Guid.Parse(c.PostId),
                    c.Content,
                    DateTime.Parse(c.CreatedAt)))
                .ToList();

            var reactions = reactionRows
                .Select(r => (IReaction)Reaction.FromPersistence(
                    Guid.Parse(r.Id),
                    Guid.Parse(r.UserId),
                    Enum.Parse<ReactionType>(r.Emoji),
                    DateTime.Parse(r.CreatedAt)))
                .ToList();

            return Post.FromPersistence(
                Guid.Parse(postRow.Id),
                Guid.Parse(postRow.AuthorId),
                postRow.Content,
                DateTime.Parse(postRow.CreatedAt),
                DateTime.Parse(postRow.UpdatedAt),
                postRow.IsDeleted == 1,
                comments,
                reactions);
        }

        public static object ToPostParams(Post post) => new
        {
            Id = post.Id.ToString(),
            AuthorId = post.AuthorId.ToString(),
            post.Content,
            CreatedAt = post.CreatedAt.ToString("o"),
            UpdatedAt = post.UpdatedAt.ToString("o"),
            IsDeleted = post.IsDeleted ? 1 : 0
        };
    }

    public sealed class PostRow
    {
        public string Id { get; set; } = "";
        public string AuthorId { get; set; } = "";
        public string Content { get; set; } = "";
        public string CreatedAt { get; set; } = "";
        public string UpdatedAt { get; set; } = "";
        public int IsDeleted { get; set; }
    }

    public sealed class CommentRow
    {
        public string Id { get; set; } = "";
        public string PostId { get; set; } = "";
        public string AuthorId { get; set; } = "";
        public string Content { get; set; } = "";
        public string CreatedAt { get; set; } = "";
    }

    public sealed class ReactionRow
    {
        public string Id { get; set; } = "";
        public string UserId { get; set; } = "";
        public string Emoji { get; set; } = "";
        public string CreatedAt { get; set; } = "";
        public string? PostId { get; set; }
        public string? CommentId { get; set; }
    }
}