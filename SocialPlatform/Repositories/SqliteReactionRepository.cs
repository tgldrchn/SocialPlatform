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
    /// SQLite ашигласан Reaction repository
    /// </summary>
    public class SqliteReactionRepository : IReactionRepository
    {
        private readonly string _connectionString;

        public SqliteReactionRepository(string connectionString)
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

        public IReaction? GetById(Guid id)
        {
            using var connection = CreateConnection();

            var row = connection.QuerySingleOrDefault<ReactionRow>(
                "SELECT * FROM Reactions WHERE Id = @Id",
                new { Id = id.ToString() });

            return row == null ? null : MapReaction(row);
        }

        public IReadOnlyList<IReaction> GetAll()
        {
            using var connection = CreateConnection();

            var reactions = connection.Query<ReactionRow>(
                "SELECT * FROM Reactions ORDER BY CreatedAt ASC")
                .Select(r => (IReaction)MapReaction(r))
                .ToList();

            return reactions.AsReadOnly();
        }

        public IEnumerable<IReaction> GetByUser(Guid userId)
        {
            using var connection = CreateConnection();

            return connection.Query<ReactionRow>(
                "SELECT * FROM Reactions WHERE UserId = @UserId ORDER BY CreatedAt ASC",
                new { UserId = userId.ToString() })
                .Select(r => (IReaction)MapReaction(r))
                .ToList();
        }

        public IEnumerable<IReaction> GetByPost(Guid postId)
        {
            using var connection = CreateConnection();

            return connection.Query<ReactionRow>(
                "SELECT * FROM Reactions WHERE PostId = @PostId ORDER BY CreatedAt ASC",
                new { PostId = postId.ToString() })
                .Select(r => (IReaction)MapReaction(r))
                .ToList();
        }

        public IEnumerable<IReaction> GetByComment(Guid commentId)
        {
            using var connection = CreateConnection();

            return connection.Query<ReactionRow>(
                "SELECT * FROM Reactions WHERE CommentId = @CommentId ORDER BY CreatedAt ASC",
                new { CommentId = commentId.ToString() })
                .Select(r => (IReaction)MapReaction(r))
                .ToList();
        }

        public void AddToPost(Guid postId, IReaction reaction)
        {
            if (reaction is not Reaction r)
                throw new ArgumentException("reaction must be of type Reaction");

            using var connection = CreateConnection();

            connection.Execute(@"
                INSERT INTO Reactions (Id, UserId, Emoji, CreatedAt, PostId, CommentId)
                VALUES (@Id, @UserId, @Emoji, @CreatedAt, @PostId, @CommentId)",
                new
                {
                    Id = r.Id.ToString(),
                    UserId = r.UserId.ToString(),
                    Emoji = r.Emoji.ToString(),
                    CreatedAt = r.CreatedAt.ToString("o"),
                    PostId = postId.ToString(),
                    CommentId = (string?)null
                });
        }

        public void AddToComment(Guid commentId, IReaction reaction)
        {
            if (reaction is not Reaction r)
                throw new ArgumentException("reaction must be of type Reaction");

            using var connection = CreateConnection();

            connection.Execute(@"
                INSERT INTO Reactions (Id, UserId, Emoji, CreatedAt, PostId, CommentId)
                VALUES (@Id, @UserId, @Emoji, @CreatedAt, @PostId, @CommentId)",
                new
                {
                    Id = r.Id.ToString(),
                    UserId = r.UserId.ToString(),
                    Emoji = r.Emoji.ToString(),
                    CreatedAt = r.CreatedAt.ToString("o"),
                    PostId = (string?)null,
                    CommentId = commentId.ToString()
                });
        }

        public void Remove(IReaction reaction)
        {
            using var connection = CreateConnection();

            connection.Execute(
                "DELETE FROM Reactions WHERE Id = @Id",
                new { Id = reaction.Id.ToString() });
        }

        public void RemoveByUserAndPost(Guid userId, Guid postId, ReactionType emoji)
        {
            using var connection = CreateConnection();

            connection.Execute(@"
                DELETE FROM Reactions
                WHERE UserId = @UserId
                  AND PostId = @PostId
                  AND Emoji = @Emoji",
                new
                {
                    UserId = userId.ToString(),
                    PostId = postId.ToString(),
                    Emoji = emoji.ToString()
                });
        }

        public void RemoveByUserAndComment(Guid userId, Guid commentId, ReactionType emoji)
        {
            using var connection = CreateConnection();

            connection.Execute(@"
                DELETE FROM Reactions
                WHERE UserId = @UserId
                  AND CommentId = @CommentId
                  AND Emoji = @Emoji",
                new
                {
                    UserId = userId.ToString(),
                    CommentId = commentId.ToString(),
                    Emoji = emoji.ToString()
                });
        }

        private static Reaction MapReaction(ReactionRow row)
        {
            return Reaction.FromPersistence(
                id: Guid.Parse(row.Id),
                userId: Guid.Parse(row.UserId),
                emoji: Enum.Parse<ReactionType>(row.Emoji),
                createdAt: DateTime.Parse(row.CreatedAt));
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