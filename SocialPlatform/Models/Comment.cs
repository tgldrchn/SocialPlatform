using SocialNetworkingPlatform.Enums;
using SocialNetworkingPlatform.Interfaces;

namespace SocialNetworkingPlatform.Models
{
    /// <summary>
    /// Постын сэтгэгдэл
    /// </summary>
    public class Comment : IComment, IReactable
    {
        private readonly List<Reaction> _reactions = new();
        private readonly List<Attachment> _attachments = new();
        public Guid Id { get; private set; } = Guid.NewGuid();
        public Guid AuthorId { get; private set; }
        public Guid PostId { get; private set; }
        public string Content { get; private set; }
        public DateTime CreatedAt { get; private set; } = DateTime.UtcNow;
        public IReadOnlyList<IReaction> Reactions => _reactions;

        public Comment(Guid authorId, Guid postId, string content)
        {
            AuthorId = authorId;
            PostId = postId;
            Content = content;
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

        public override string ToString() =>
            $"[Comment] {Content} | reactions:{_reactions.Count}";
    }
}
