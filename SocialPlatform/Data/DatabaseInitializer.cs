using Dapper;
using Microsoft.Data.Sqlite;

namespace SocialPlatform.Database;

public static class DatabaseInitializer
{
    public static void Initialize(string connectionString)
    {
        using var conn = new SqliteConnection(connectionString);
        conn.Open();
        conn.Execute(@"
            PRAGMA foreign_keys = ON;

            -- ── Posts ──────────────────────────────────────────
            CREATE TABLE IF NOT EXISTS Posts (
                Id        TEXT NOT NULL PRIMARY KEY,
                AuthorId  TEXT NOT NULL,
                Content   TEXT NOT NULL,
                CreatedAt TEXT NOT NULL,
                UpdatedAt TEXT NOT NULL,
                IsDeleted INTEGER NOT NULL DEFAULT 0
            );

            -- ── Comments ───────────────────────────────────────
            CREATE TABLE IF NOT EXISTS Comments (
                Id        TEXT NOT NULL PRIMARY KEY,
                PostId    TEXT NOT NULL REFERENCES Posts(Id) ON DELETE CASCADE,
                AuthorId  TEXT NOT NULL,
                Content   TEXT NOT NULL,
                CreatedAt TEXT NOT NULL
            );

            -- ── Reactions ──────────────────────────────────────
            -- PostId эсвэл CommentId-ын аль нэг нь NULL биш байна
            CREATE TABLE IF NOT EXISTS Reactions (
                Id        TEXT NOT NULL PRIMARY KEY,
                UserId    TEXT NOT NULL,
                Emoji     TEXT NOT NULL,          -- ReactionType.ToString()
                CreatedAt TEXT NOT NULL,
                PostId    TEXT REFERENCES Posts(Id)    ON DELETE CASCADE,
                CommentId TEXT REFERENCES Comments(Id) ON DELETE CASCADE,
                CHECK (
                    (PostId IS NOT NULL AND CommentId IS NULL) OR
                    (PostId IS NULL    AND CommentId IS NOT NULL)
                )
            );

            -- Нэг хэрэглэгч нэг emoji-г давхардуулж оруулахгүй
            CREATE UNIQUE INDEX IF NOT EXISTS uq_reaction_post
                ON Reactions (UserId, Emoji, PostId)
                WHERE PostId IS NOT NULL;

            CREATE UNIQUE INDEX IF NOT EXISTS uq_reaction_comment
                ON Reactions (UserId, Emoji, CommentId)
                WHERE CommentId IS NOT NULL;

            CREATE INDEX IF NOT EXISTS idx_comments_post
                ON Comments (PostId);

            CREATE INDEX IF NOT EXISTS idx_reactions_post
                ON Reactions (PostId);

            CREATE INDEX IF NOT EXISTS idx_reactions_comment
                ON Reactions (CommentId);
        ");
    }
}