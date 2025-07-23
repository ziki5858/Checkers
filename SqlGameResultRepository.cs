using System;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace Checkers.Data
{
    /// <summary>
    /// Repository for storing and retrieving game results and player data.
    /// </summary>
    public class SqlGameResultRepository
    {
        private readonly SqlConnection _cnn;
        private const string ComputerUser = "computer";

        /// <summary>
        /// Initializes a new repository with the given SQL connection.
        /// </summary>
        public SqlGameResultRepository(SqlConnection connection)
        {
            _cnn = connection;
        }

        #region Public Methods

        /// <summary>
        /// Ensures that a "computer" user exists in the Player table.
        /// </summary>
        public void EnsureComputerExists()
        {
            if (!UsernameExists(ComputerUser))
            {
                // Insert a new computer user with default values
                ExecuteNonQuery(
                    "INSERT INTO Player (Username, Scoring, winNumber, loseNumber) VALUES (@user, 0, 0, 0)",
                    cmd => cmd.Parameters.AddWithValue("@user", ComputerUser)
                );
            }
        }

        /// <summary>
        /// Checks if a username exists.
        /// </summary>
        public bool UsernameExists(string username)
            => QuerySingle<int>(
                "SELECT COUNT(*) FROM Player WHERE Username = @user",
                cmd => cmd.Parameters.AddWithValue("@user", username)
            ) > 0;

        /// <summary>
        /// Creates a new player, storing password securely as "salt:hash".
        /// </summary>
        public void CreatePlayer(string username, string password, string privateQuestion, string answer)
        {
            // Generate salt and hash
            PasswordHelper.CreateHash(password, out var hashHex, out var saltHex);
            string combined = $"{saltHex}:{hashHex}";

            // Insert record
            ExecuteNonQuery(
                @"INSERT INTO Player (Username, Password, Scoring, [Private question], Answer, winNumber, loseNumber)
                  VALUES (@user, @pass, 0, @q, @ans, 0, 0)",
                cmd =>
                {
                    cmd.Parameters.AddWithValue("@user", username);
                    cmd.Parameters.AddWithValue("@pass", combined);
                    cmd.Parameters.AddWithValue("@q", privateQuestion);
                    cmd.Parameters.AddWithValue("@ans", answer);
                }
            );
        }

        /// <summary>
        /// Validates the plaintext password against stored credentials.
        /// Supports migrating legacy plaintext entries.
        /// </summary>
        public bool ValidatePassword(string username, string password)
        {
            // Retrieve stored password or salt:hash
            string stored = QuerySingle<string>(
                "SELECT Password FROM Player WHERE Username = @user",
                cmd => cmd.Parameters.AddWithValue("@user", username)
            )?.Trim();

            if (string.IsNullOrEmpty(stored))
                return false;

            if (stored.Contains(':'))
            {
                // salt:hash format
                var parts = stored.Split(new[] { ':' }, 2);
                string saltHex = parts[0];
                string hashHex = parts[1];
                return PasswordHelper.VerifyPassword(password, hashHex, saltHex);
            }

            // Legacy plaintext fallback and migrate
            if (string.Equals(password, stored, StringComparison.Ordinal))
            {
                ChangePassword(username, password);
                return true;
            }

            return false;
        }

        /// <summary>
        /// Changes a user's password, storing new salt:hash.
        /// </summary>
        public void ChangePassword(string username, string newPassword)
        {
            // Generate new salt and hash
            PasswordHelper.CreateHash(newPassword, out var hashHex, out var saltHex);
            string combined = $"{saltHex}:{hashHex}";

            ExecuteNonQuery(
                "UPDATE Player SET Password = @pass WHERE Username = @user",
                cmd =>
                {
                    cmd.Parameters.AddWithValue("@pass", combined);
                    cmd.Parameters.AddWithValue("@user", username);
                }
            );
        }

        /// <summary>
        /// Retrieves the raw stored password entry (plaintext or salt:hash).
        /// </summary>
        public string GetPassword(string username)
            => QuerySingle<string>(
                "SELECT Password FROM Player WHERE Username = @user",
                cmd => cmd.Parameters.AddWithValue("@user", username)
            );

        /// <summary>
        /// Retrieves a user's private security question.
        /// </summary>
        public string GetPrivateQuestion(string username)
            => QuerySingle<string>(
                "SELECT [Private question] FROM Player WHERE Username = @user",
                cmd => cmd.Parameters.AddWithValue("@user", username)
            );

        /// <summary>
        /// Retrieves a user's stored answer.
        /// </summary>
        public string GetAnswer(string username)
            => QuerySingle<string>(
                "SELECT Answer FROM Player WHERE Username = @user",
                cmd => cmd.Parameters.AddWithValue("@user", username)
            );

        /// <summary>
        /// Retrieves the security question as a DataTable for binding.
        /// </summary>
        public DataTable GetPrivateQuestionTable(string username)
            => ExecuteSelect(
                "SELECT [Private question] AS Question FROM Player WHERE Username = @user",
                cmd => cmd.Parameters.AddWithValue("@user", username)
            );

        /// <summary>
        /// Updates a user's scoring by a delta amount.
        /// </summary>
        public void UpdateScore(string username, int delta)
            => ExecuteNonQuery(
                "UPDATE Player SET Scoring = Scoring + @delta WHERE Username = @user",
                cmd =>
                {
                    cmd.Parameters.AddWithValue("@delta", delta);
                    cmd.Parameters.AddWithValue("@user", username);
                }
            );

        /// <summary>
        /// Records a completed game result into the Games table.
        /// </summary>
        public void RecordWin(string winner, string playerA, string playerB, DateTime timestamp)
        {
            // Ensure computer record exists
            if (winner.Equals(ComputerUser, StringComparison.OrdinalIgnoreCase))
                EnsureComputerExists();

            ExecuteNonQuery(
                "INSERT INTO Games (winner, PlayerA, PlayerB, Date) VALUES (@winner,@a,@b,@d)",
                cmd =>
                {
                    cmd.Parameters.AddWithValue("@winner", winner);
                    cmd.Parameters.AddWithValue("@a", playerA);
                    cmd.Parameters.AddWithValue("@b", playerB);
                    cmd.Parameters.AddWithValue("@d", timestamp);
                }
            );
        }

        /// <summary>
        /// Retrieves a user's current scoring value.
        /// </summary>
        public int GetScore(string username)
            => QuerySingle<int>(
                "SELECT Scoring FROM Player WHERE Username = @user",
                cmd => cmd.Parameters.AddWithValue("@user", username)
            );

        /// <summary>
        /// Retrieves all usernames.
        /// </summary>
        public DataTable GetAllUsernames()
            => ExecuteSelect("SELECT Username FROM Player");

        /// <summary>
        /// Retrieves all player stats ordered by scoring.
        /// </summary>
        public DataTable GetAllPlayerStats()
            => ExecuteSelect(
                "SELECT Username, Scoring, winNumber, loseNumber FROM Player ORDER BY Scoring DESC"
            );

        /// <summary>
        /// Retrieves win/lose counts for a user.
        /// </summary>
        public DataTable GetPlayerStats(string username)
            => ExecuteSelect(
                "SELECT winNumber, loseNumber FROM Player WHERE Username = @user",
                cmd => cmd.Parameters.AddWithValue("@user", username)
            );

        /// <summary>
        /// Retrieves all games ordered by date.
        /// </summary>
        public DataTable GetAllGames()
            => ExecuteSelect(
                "SELECT ROW_NUMBER() OVER(ORDER BY Date) AS [No],[Game number],PlayerA,PlayerB,Winner,Date FROM Games ORDER BY Date"
            );

        /// <summary>
        /// Retrieves a specific game by its number.
        /// </summary>
        public DataTable GetGameByNumber(int gameNumber)
            => ExecuteSelect(
                "SELECT ROW_NUMBER() OVER(ORDER BY Date) AS [No],[Game number],PlayerA,PlayerB,Winner,Date FROM Games WHERE [Game number]=@id ORDER BY Date",
                cmd => cmd.Parameters.AddWithValue("@id", gameNumber)
            );

        /// <summary>
        /// Deletes a game if the user is admin, then refreshes scores.
        /// </summary>
        public void DeleteGame(int gameNumber, string username)
        {
            if (!username.Equals("admin", StringComparison.OrdinalIgnoreCase))
                throw new InvalidOperationException("Only admin can delete games.");

            ExecuteNonQuery(
                "DELETE FROM Games WHERE [Game number]=@id",
                cmd => cmd.Parameters.AddWithValue("@id", gameNumber)
            );
            RefreshPlayerScores();
        }

        /// <summary>
        /// Recalculates win/loss counts and scoring for all players.
        /// </summary>
        public void RefreshPlayerScores()
        {
            ExecuteNonQuery("UPDATE Player SET winNumber=(SELECT COUNT(*) FROM Games WHERE Winner=Player.Username)");
            ExecuteNonQuery("UPDATE Player SET loseNumber=(SELECT COUNT(*) FROM Games WHERE (PlayerA=Player.Username OR PlayerB=Player.Username) AND Winner<>Player.Username)");
            ExecuteNonQuery("UPDATE Player SET Scoring=winNumber-loseNumber");
        }

        /// <summary>
        /// Logs an exception to the ErrorLog table with timestamp.
        /// </summary>
        public void LogError(Exception ex)
            => ExecuteNonQuery(
                "INSERT INTO ErrorLog (Message, StackTrace, LogDate) VALUES (@msg,@stack,@date)",
                cmd =>
                {
                    cmd.Parameters.AddWithValue("@msg", ex.Message);
                    cmd.Parameters.AddWithValue("@stack", ex.ToString());
                    cmd.Parameters.AddWithValue("@date", DateTime.Now);
                }
            );

        #endregion

        #region Private Helpers

        /// <summary>
        /// Executes a non-query SQL command.
        /// </summary>
        private void ExecuteNonQuery(string sql, Action<SqlCommand> configure = null)
        {
            using (var cmd = new SqlCommand(sql, _cnn))
            {
                configure?.Invoke(cmd);
                cmd.ExecuteNonQuery();
            }
        }

        /// <summary>
        /// Executes a scalar SQL query and returns the single result.
        /// </summary>
        private T QuerySingle<T>(string sql, Action<SqlCommand> configure = null)
        {
            using (var cmd = new SqlCommand(sql, _cnn))
            {
                configure?.Invoke(cmd);
                object result = cmd.ExecuteScalar();
                return result == null || result is DBNull ? default : (T)Convert.ChangeType(result, typeof(T));
            }
        }

        /// <summary>
        /// Executes a SELECT SQL query and returns the result as a DataTable.
        /// </summary>
        private DataTable ExecuteSelect(string sql, Action<SqlCommand> configure = null)
        {
            using (var cmd = new SqlCommand(sql, _cnn))
            {
                configure?.Invoke(cmd);
                using (var da = new SqlDataAdapter(cmd))
                {
                    var dt = new DataTable();
                    da.Fill(dt);
                    return dt;
                }
            }
        }

        #endregion
    }
}