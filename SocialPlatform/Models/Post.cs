using SocialNetworkingPlatform.Enums;
using SocialPlatform.Interfaces;

namespace SocialNetworkingPlatform.Models
{
    /// <summary>
    /// Бүх платформын постын суурь класс
    /// </summary>
    public class Post : IPost, IReactable, ICommentable
    {
        private readonly List<IReaction> _reactions = new();
        private readonly List<IComment> _comments = new();

        public Guid Id { get; private set; } = Guid.NewGuid();
        public Guid AuthorId { get; private set; }
        public string Content { get; private set; }
        public DateTime CreatedAt { get; private set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; private set; } = DateTime.UtcNow;
        public bool IsDeleted { get; private set; }

        public IReadOnlyList<IReaction> Reactions => _reactions;
        public IReadOnlyList<IComment> Comments => _comments;

        public Post(Guid authorId, string content)
        {
            AuthorId = authorId;
            Content = content;
            CreatedAt = DateTime.UtcNow;
            UpdatedAt = DateTime.UtcNow;
            Id = Guid.NewGuid();
        }

        private Post(
            Guid id,
            Guid authorId,
            string content,
            DateTime createdAt,
            DateTime updatedAt,
            bool isDeleted,
            IEnumerable<IComment>? comments = null,
            IEnumerable<IReaction>? reactions = null)
        {
            Id = id;
            AuthorId = authorId;
            Content = content;
            CreatedAt = createdAt;
            UpdatedAt = updatedAt;
            IsDeleted = isDeleted;

            if (comments != null)
                _comments.AddRange(comments);

            if (reactions != null)
                _reactions.AddRange(reactions);
        }

        public static Post FromPersistence(
            Guid id,
            Guid authorId,
            string content,
            DateTime createdAt,
            DateTime updatedAt,
            bool isDeleted,
            IEnumerable<IComment>? comments = null,
            IEnumerable<IReaction>? reactions = null)
        {
            return new Post(
                id,
                authorId,
                content,
                createdAt,
                updatedAt,
                isDeleted,
                comments,
                reactions);
        }

        /// <summary>Пост засах</summary>
        public void EditContent(string newContent)
        {
            Content = newContent;
            UpdatedAt = DateTime.UtcNow;
        }

        /// <summary>Пост устгах (Soft delete)</summary>
        public void Delete()
        {
            IsDeleted = true;
            UpdatedAt = DateTime.UtcNow;
        }

        public void AddReaction(Guid userId, ReactionType emoji)
        {
            if (!_reactions.Exists(r => r.UserId == userId && r.Emoji == emoji))
                _reactions.Add(new Reaction(userId, emoji));
        }

        public void RemoveReaction(Guid userId, ReactionType emoji) =>
            _reactions.RemoveAll(r => r.UserId == userId && r.Emoji == emoji);

        public int GetReactionCount(ReactionType emoji) =>
            _reactions.Count(r => r.Emoji == emoji);

        public bool HasReacted(Guid userId, ReactionType emoji) =>
            _reactions.Exists(r => r.UserId == userId && r.Emoji == emoji);

        public void AddComment(Guid userId, string content) =>
            _comments.Add(new Comment(userId, Id, content));

        public void RemoveComment(Guid commentId) =>
            _comments.RemoveAll(c => c.Id == commentId);

        public override string ToString() =>
            $"[Post] {Content} | reactions:{_reactions.Count} comments:{_comments.Count}{(IsDeleted ? " (deleted)" : "")}";
    }
}