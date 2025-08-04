using Microsoft.Data.Sqlite;
using System;
using System.Data;
using System.IO;

namespace LandDesignAIDesktop
{
    public sealed class AISqliteFunctions
    {
        private const string BaseFolder = @"C:\ProgramData\LandDesignGPT";
        private readonly string _connectionString;

        // Builds C:\ProgramData\LandDesignGPT\LandDesignAIMemory-{username}.sqlite
        public static string BuildDefaultPath()
        {
            string fileName = $"LandDesignAIMemory-{Environment.UserName}.sqlite";
            return Path.Combine(BaseFolder, fileName);
        }

        public AISqliteFunctions(string? dbPath = null)
        {
            string path = string.IsNullOrWhiteSpace(dbPath) ? BuildDefaultPath() : dbPath;

            Directory.CreateDirectory(BaseFolder);

            _connectionString = new SqliteConnectionStringBuilder
            {
                DataSource = path,
                Mode = SqliteOpenMode.ReadWriteCreate,
                Cache = SqliteCacheMode.Shared
            }.ToString();

            EnsureSchema();
        }

        public SqliteConnection GetOpenConnection()
        {
            var conn = new SqliteConnection(_connectionString);
            conn.Open();
            return conn;
        }

        /* ─────────────────────────  CRUD helpers  ───────────────────────── */

        public void AppendMessage(
            string? chatName = null,
            string? chatTone = null,
            string? chatRole = null,
            string? chatMessage = null,
            string? chatUser = null)
        {
            using var conn = GetOpenConnection();
            using var cmd = conn.CreateCommand();

            cmd.CommandText = @"
                INSERT INTO Messages
                      (ChatName, ChatTone, ChatRole, ChatMessage, ChatUser)
                VALUES ($name, $tone, $role, $message, $user);";

            AddNullable(cmd, "$name", chatName);
            AddNullable(cmd, "$tone", chatTone);
            AddNullable(cmd, "$role", chatRole);
            AddNullable(cmd, "$message", chatMessage);
            AddNullable(cmd, "$user", chatUser);

            cmd.ExecuteNonQuery();
        }

        public int GetMessageCount()
        {
            using var conn = GetOpenConnection();
            using var cmd = conn.CreateCommand();
            cmd.CommandText = "SELECT COUNT(*) FROM Messages;";
            return Convert.ToInt32(cmd.ExecuteScalar());
        }

        /// <summary>
        /// Removes the row at the specified zero-based index (ordered by ROWID).
        /// Returns true if a row was deleted, false if the index is out of range.
        /// </summary>
        public bool RemoveMessageAt(int index)
        {
            if (index < 0) return false;

            using var conn = GetOpenConnection();
            using var cmd = conn.CreateCommand();

            cmd.CommandText = @"
                DELETE FROM Messages
                WHERE rowid = (
                    SELECT rowid
                    FROM Messages
                    ORDER BY rowid
                    LIMIT 1 OFFSET $idx
                );";
            cmd.Parameters.AddWithValue("$idx", index);

            int affected = cmd.ExecuteNonQuery();
            return affected > 0;
        }

        /* ─────────────────────────  internals  ───────────────────────── */

        private static void AddNullable(SqliteCommand cmd, string name, string? value)
        {
            var p = cmd.Parameters.Add(name, (SqliteType)DbType.String);
            p.Value = value ?? (object)DBNull.Value;
        }

        private void EnsureSchema()
        {
            using var conn = GetOpenConnection();
            using var cmd = conn.CreateCommand();

            cmd.CommandText = @"
                PRAGMA journal_mode = WAL;
                PRAGMA foreign_keys = ON;

                CREATE TABLE IF NOT EXISTS Messages (
                    ChatName     TEXT,
                    ChatTone     TEXT,
                    ChatRole     TEXT,
                    ChatMessage  TEXT,
                    ChatUser     TEXT
                );
            ";
            cmd.ExecuteNonQuery();
        }
    }
}

