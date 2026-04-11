using Dapper;
using Microsoft.Data.Sqlite;
using SocialNetworkingPlatform.Enums;
using SocialNetworkingPlatform.Interfaces;
using SocialNetworkingPlatform.Models;
using SocialPlatform.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SocialNetworkingPlatform.Repositories
{
    /// <summary>
    /// SQLite ашигласан Post repository
    /// </summary>
    public class SqlitePostRepository : IPostRepository
    {
        private readonly string _connectionString;

        public SqlitePostRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        private SqliteConnection CreateConnection()
        {
            var connection = new SqliteConnection(_connectionString);
            connection.Open();
            connection.Execute("PRAGMA foreign_keys = ON;");
            return connection;
        }

        /// <summary>ID-гаар хайх</summary>
        public IPost? GetById(Guid id)
        {
            using var connection = CreateConnection();

            var postRow = connection.QuerySingleOrDefault<PostRow>(
                "SELECT * FROM Posts WHERE Id = @Id",
                new { Id = id.ToString() });

            if (postRow == null)
                return null;

            return Hydrate(connection, postRow);
        }

        /// <summary>Бүгдийг авах</summary>
        public IReadOnlyList<IPost> GetAll()
        {
            using var connection = CreateConnection();

            var postRows = connection.Query<PostRow>(
                "SELECT * FROM Posts ORDER BY CreatedAt DESC").ToList();

            var posts = postRows
                .Select(row => (IPost)Hydrate(connection, row))
                .ToList();

            return posts.AsReadOnly();
        }

        /// <summary>Нэмэх</summary>
        public void Add(IPost post)
        {
            if (post is not Post p)
                throw new ArgumentException("post must be of type Post");

            using var connection = CreateConnection();
            using var tx = connection.BeginTransaction();

            connection.Execute(@"
                INSERT INTO Posts (Id, AuthorId, Content, CreatedAt, UpdatedAt, IsDeleted)
                VALUES (@Id, @AuthorId, @Content, @CreatedAt, @UpdatedAt, @IsDeleted)",
                new
                {
                    Id = p.Id.ToString(),
                    AuthorId = p.AuthorId.ToString(),
                    p.Content,
                    CreatedAt = p.CreatedAt.ToString("o"),
                    UpdatedAt = p.UpdatedAt.ToString("o"),
                    IsDeleted = p.IsDeleted ? 1 : 0
                }, tx);

            foreach (var commentInterface in p.Comments)
            {
                if (commentInterface is not Comment comment)
                    continue;

                connection.Execute(@"
                    INSERT INTO Comments (Id, PostId, AuthorId, Content, CreatedAt)
                    VALUES (@Id, @PostId, @AuthorId, @Content, @CreatedAt)",
                    new
                    {
                        Id = comment.Id.ToString(),
                        PostId = comment.PostId.ToString(),
                        AuthorId = comment.AuthorId.ToString(),
                        comment.Content,
                        CreatedAt = comment.CreatedAt.ToString("o")
                    }, tx);

                foreach (var reactionInterface in comment.Reactions)
                {
                    if (reactionInterface is not Reaction reaction)
                        continue;

                    connection.Execute(@"
                        INSERT INTO Reactions (Id, UserId, Emoji, CreatedAt, PostId, CommentId)
                        VALUES (@Id, @UserId, @Emoji, @CreatedAt, @PostId, @CommentId)",
                        new
                        {
                            Id = reaction.Id.ToString(),
                            UserId = reaction.UserId.ToString(),
                            Emoji = reaction.Emoji.ToString(),
                            CreatedAt = reaction.CreatedAt.ToString("o"),
                            PostId = (string?)null,
                            CommentId = comment.Id.ToString()
                        }, tx);
                }
            }

            foreach (var reactionInterface in p.Reactions)
            {
                if (reactionInterface is not Reaction reaction)
                    continue;

                connection.Execute(@"
                    INSERT INTO Reactions (Id, UserId, Emoji, CreatedAt, PostId, CommentId)
                    VALUES (@Id, @UserId, @Emoji, @CreatedAt, @PostId, @CommentId)",
                    new
                    {
                        Id = reaction.Id.ToString(),
                        UserId = reaction.UserId.ToString(),
                        Emoji = reaction.Emoji.ToString(),
                        CreatedAt = reaction.CreatedAt.ToString("o"),
                        PostId = p.Id.ToString(),
                        CommentId = (string?)null
                    }, tx);
            }

            tx.Commit();
        }

        /// <summary>Шинэчлэх</summary>
        public void Update(IPost post)
        {
            if (post is not Post p)
                throw new ArgumentException("post must be of type Post");

            using var connection = CreateConnection();
            using var tx = connection.BeginTransaction();

            connection.Execute(@"
                UPDATE Posts
                SET Content = @Content,
                    UpdatedAt = @UpdatedAt,
                    IsDeleted = @IsDeleted
                WHERE Id = @Id",
                new
                {
                    Id = p.Id.ToString(),
                    p.Content,
                    UpdatedAt = p.UpdatedAt.ToString("o"),
                    IsDeleted = p.IsDeleted ? 1 : 0
                }, tx);

            connection.Execute(@"
                DELETE FROM Reactions
                WHERE PostId = @PostId
                   OR CommentId IN (SELECT Id FROM Comments WHERE PostId = @PostId)",
                new { PostId = p.Id.ToString() }, tx);

            connection.Execute(
                "DELETE FROM Comments WHERE PostId = @PostId",
                new { PostId = p.Id.ToString() }, tx);

            foreach (var commentInterface in p.Comments)
            {
                if (commentInterface is not Comment comment)
                    continue;

                connection.Execute(@"
                    INSERT INTO Comments (Id, PostId, AuthorId, Content, CreatedAt)
                    VALUES (@Id, @PostId, @AuthorId, @Content, @CreatedAt)",
                    new
                    {
                        Id = comment.Id.ToString(),
                        PostId = comment.PostId.ToString(),
                        AuthorId = comment.AuthorId.ToString(),
                        comment.Content,
                        CreatedAt = comment.CreatedAt.ToString("o")
                    }, tx);

                foreach (var reactionInterface in comment.Reactions)
                {
                    if (reactionInterface is not Reaction reaction)
                        continue;

                    connection.Execute(@"
                        INSERT INTO Reactions (Id, UserId, Emoji, CreatedAt, PostId, CommentId)
                        VALUES (@Id, @UserId, @Emoji, @CreatedAt, @PostId, @CommentId)",
                        new
                        {
                            Id = reaction.Id.ToString(),
                            UserId = reaction.UserId.ToString(),
                            Emoji = reaction.Emoji.ToString(),
                            CreatedAt = reaction.CreatedAt.ToString("o"),
                            PostId = (string?)null,
                            CommentId = comment.Id.ToString()
                        }, tx);
                }
            }

            foreach (var reactionInterface in p.Reactions)
            {
                if (reactionInterface is not Reaction reaction)
                    continue;

                connection.Execute(@"
                    INSERT INTO Reactions (Id, UserId, Emoji, CreatedAt, PostId, CommentId)
                    VALUES (@Id, @UserId, @Emoji, @CreatedAt, @PostId, @CommentId)",
                    new
                    {
                        Id = reaction.Id.ToString(),
                        UserId = reaction.UserId.ToString(),
                        Emoji = reaction.Emoji.ToString(),
                        CreatedAt = reaction.CreatedAt.ToString("o"),
                        PostId = p.Id.ToString(),
                        CommentId = (string?)null
                    }, tx);
            }

            tx.Commit();
        }

        /// <summary>Устгах</summary>
        public void Remove(IPost post)
        {
            using var connection = CreateConnection();
            using var tx = connection.BeginTransaction();

            connection.Execute(@"
                DELETE FROM Reactions
                WHERE PostId = @PostId
                   OR CommentId IN (SELECT Id FROM Comments WHERE PostId = @PostId)",
                new { PostId = post.Id.ToString() }, tx);

            connection.Execute(
                "DELETE FROM Comments WHERE PostId = @PostId",
                new { PostId = post.Id.ToString() }, tx);

            connection.Execute(
                "DELETE FROM Posts WHERE Id = @Id",
                new { Id = post.Id.ToString() }, tx);

            tx.Commit();
        }

        /// <summary>Зохиогчоор хайх</summary>
        public IEnumerable<IPost> GetByAuthor(Guid authorId)
        {
            using var connection = CreateConnection();

            var postRows = connection.Query<PostRow>(
                "SELECT * FROM Posts WHERE AuthorId = @AuthorId ORDER BY CreatedAt DESC",
                new { AuthorId = authorId.ToString() }).ToList();

            return postRows
                .Select(row => (IPost)Hydrate(connection, row))
                .ToList();
        }

        /// <summary>Feed авах</summary>
        public IEnumerable<IPost> GetFeed(Guid userId)
        {
            using var connection = CreateConnection();

            var postRows = connection.Query<PostRow>(
                "SELECT * FROM Posts WHERE IsDeleted = 0 ORDER BY CreatedAt DESC").ToList();

            return postRows
                .Select(row => (IPost)Hydrate(connection, row))
                .ToList();
        }

        private static Post Hydrate(SqliteConnection connection, PostRow postRow)
        {
            var commentRows = connection.Query<CommentRow>(
                "SELECT * FROM Comments WHERE PostId = @PostId ORDER BY CreatedAt ASC",
                new { PostId = postRow.Id }).ToList();

            var postReactions = connection.Query<ReactionRow>(
                "SELECT * FROM Reactions WHERE PostId = @PostId ORDER BY CreatedAt ASC",
                new { PostId = postRow.Id }).ToList();

            var comments = new List<IComment>();

            foreach (var commentRow in commentRows)
            {
                var commentReactionRows = connection.Query<ReactionRow>(
                    "SELECT * FROM Reactions WHERE CommentId = @CommentId ORDER BY CreatedAt ASC",
                    new { CommentId = commentRow.Id }).ToList();

                var commentReactions = commentReactionRows
                    .Select(MapReaction)
                    .Cast<Reaction>()
                    .ToList();

                var comment = Comment.FromPersistence(
                    id: Guid.Parse(commentRow.Id),
                    authorId: Guid.Parse(commentRow.AuthorId),
                    postId: Guid.Parse(commentRow.PostId),
                    content: commentRow.Content,
                    createdAt: DateTime.Parse(commentRow.CreatedAt),
                    reactions: commentReactions);

                comments.Add(comment);
            }

            var reactions = postReactions
                .Select(MapReaction)
                .Cast<IReaction>()
                .ToList();

            return Post.FromPersistence(
                id: Guid.Parse(postRow.Id),
                authorId: Guid.Parse(postRow.AuthorId),
                content: postRow.Content,
                createdAt: DateTime.Parse(postRow.CreatedAt),
                updatedAt: DateTime.Parse(postRow.UpdatedAt),
                isDeleted: postRow.IsDeleted == 1,
                comments: comments,
                reactions: reactions);
        }

        private static Reaction MapReaction(ReactionRow row)
        {
            return Reaction.FromPersistence(
                id: Guid.Parse(row.Id),
                userId: Guid.Parse(row.UserId),
                emoji: Enum.Parse<ReactionType>(row.Emoji),
                createdAt: DateTime.Parse(row.CreatedAt));
        }

        private sealed class PostRow
        {
            public string Id { get; set; } = "";
            public string AuthorId { get; set; } = "";
            public string Content { get; set; } = "";
            public string CreatedAt { get; set; } = "";
            public string UpdatedAt { get; set; } = "";
            public int IsDeleted { get; set; }
        }

        private sealed class CommentRow
        {
            public string Id { get; set; } = "";
            public string PostId { get; set; } = "";
            public string AuthorId { get; set; } = "";
            public string Content { get; set; } = "";
            public string CreatedAt { get; set; } = "";
        }

        private sealed class ReactionRow
        {
            public string Id { get; set; } = "";
            public string UserId { get; set; } = "";
            public string Emoji { get; set; } = "";
            public string CreatedAt { get; set; } = "";
            public string? PostId { get; set; }
            public string? CommentId { get; set; }
        }
    }
}