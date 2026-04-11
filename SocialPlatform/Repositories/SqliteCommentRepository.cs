using System;
using System.Collections.Generic;
using System.Linq;
using Dapper;
using Microsoft.Data.Sqlite;
using SocialNetworkingPlatform.Enums;
using SocialNetworkingPlatform.Models;
using SocialPlatform.Interfaces;

namespace SocialNetworkingPlatform.Repositories
{
    /// <summary>
    /// SQLite ашигласан Comment repository
    /// </summary>
    public class SqliteCommentRepository : ICommentRepository
    {
        private readonly string _connectionString;

        public SqliteCommentRepository(string connectionString)
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

        public IComment? GetById(Guid id)
        {
            using var connection = CreateConnection();

            var row = connection.QuerySingleOrDefault<CommentRow>(
                "SELECT * FROM Comments WHERE Id = @Id",
                new { Id = id.ToString() });

            if (row == null)
                return null;

            return Hydrate(connection, row);
        }

        public IReadOnlyList<IComment> GetAll()
        {
            using var connection = CreateConnection();

            var rows = connection.Query<CommentRow>(
                "SELECT * FROM Comments ORDER BY CreatedAt ASC").ToList();

            var comments = rows
                .Select(r => (IComment)Hydrate(connection, r))
                .ToList();

            return comments.AsReadOnly();
        }

        public IEnumerable<IComment> GetByPost(Guid postId)
        {
            using var connection = CreateConnection();

            var rows = connection.Query<CommentRow>(
                "SELECT * FROM Comments WHERE PostId = @PostId ORDER BY CreatedAt ASC",
                new { PostId = postId.ToString() }).ToList();

            return rows
                .Select(r => (IComment)Hydrate(connection, r))
                .ToList();
        }

        public IEnumerable<IComment> GetByAuthor(Guid authorId)
        {
            using var connection = CreateConnection();

            var rows = connection.Query<CommentRow>(
                "SELECT * FROM Comments WHERE AuthorId = @AuthorId ORDER BY CreatedAt ASC",
                new { AuthorId = authorId.ToString() }).ToList();

            return rows
                .Select(r => (IComment)Hydrate(connection, r))
                .ToList();
        }

        public void Add(IComment comment)
        {
            if (comment is not Comment c)
                throw new ArgumentException("comment must be of type Comment");

            using var connection = CreateConnection();
            using var tx = connection.BeginTransaction();

            connection.Execute(@"
                INSERT INTO Comments (Id, PostId, AuthorId, Content, CreatedAt)
                VALUES (@Id, @PostId, @AuthorId, @Content, @CreatedAt)",
                new
                {
                    Id = c.Id.ToString(),
                    PostId = c.PostId.ToString(),
                    AuthorId = c.AuthorId.ToString(),
                    c.Content,
                    CreatedAt = c.CreatedAt.ToString("o")
                }, tx);

            foreach (var reactionInterface in c.Reactions)
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
                        CommentId = c.Id.ToString()
                    }, tx);
            }

            tx.Commit();
        }

        public void Update(IComment comment)
        {
            if (comment is not Comment c)
                throw new ArgumentException("comment must be of type Comment");

            using var connection = CreateConnection();
            using var tx = connection.BeginTransaction();

            connection.Execute(@"
                UPDATE Comments
                SET Content = @Content
                WHERE Id = @Id",
                new
                {
                    Id = c.Id.ToString(),
                    c.Content
                }, tx);

            connection.Execute(
                "DELETE FROM Reactions WHERE CommentId = @CommentId",
                new { CommentId = c.Id.ToString() }, tx);

            foreach (var reactionInterface in c.Reactions)
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
                        CommentId = c.Id.ToString()
                    }, tx);
            }

            tx.Commit();
        }

        public void Remove(IComment comment)
        {
            using var connection = CreateConnection();
            using var tx = connection.BeginTransaction();

            connection.Execute(
                "DELETE FROM Reactions WHERE CommentId = @CommentId",
                new { CommentId = comment.Id.ToString() }, tx);

            connection.Execute(
                "DELETE FROM Comments WHERE Id = @Id",
                new { Id = comment.Id.ToString() }, tx);

            tx.Commit();
        }

        private static Comment Hydrate(SqliteConnection connection, CommentRow row)
        {
            var reactionRows = connection.Query<ReactionRow>(
                "SELECT * FROM Reactions WHERE CommentId = @CommentId ORDER BY CreatedAt ASC",
                new { CommentId = row.Id }).ToList();

            var reactions = reactionRows
                .Select(MapReaction)
                .ToList();

            return Comment.FromPersistence(
                id: Guid.Parse(row.Id),
                authorId: Guid.Parse(row.AuthorId),
                postId: Guid.Parse(row.PostId),
                content: row.Content,
                createdAt: DateTime.Parse(row.CreatedAt),
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