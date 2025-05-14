using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using Diplom.Security;
using Diplom.UI;

namespace Diplom.Core.Service
{
    public class UserService : IUserService
    {
        private readonly string _connectionString = DatabaseConfig.connectionString;

        public DataTable GetAllUsers()
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();

                string query = @"
                    SELECT 
                        u.UserID,
                        u.FullName,
                        u.Login,
                        u.Email,
                        u.Warnings,
                        u.CreatedAt,
                        u.Role,
                        u.IsBanned,
                        ISNULL(SUM(CASE 
                            WHEN CAST(ws.StartTime AS DATE) = CAST(GETDATE() AS DATE) THEN ws.EffectiveTime ELSE 0 END), 0) / 60 AS TodayWorkMinutes,
                        ISNULL(SUM(ws.EffectiveTime), 0) / 60 AS TotalWorkMinutes
                    FROM Users u
                    LEFT JOIN WorkSessions ws ON u.UserID = ws.UserID
                    WHERE Role = 'employee'
                    GROUP BY u.UserID, u.FullName, u.Login, u.Email, u.Warnings, u.Role, u.IsBanned, u.CreatedAt
                ";


                SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
                DataTable usersTable = new DataTable();
                adapter.Fill(usersTable);

                return usersTable;
            }
        }
     
        public LoginResult GetUserByLogin(string login)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                string query = "SELECT FullName, Login, Role, IsBanned FROM Users WHERE Login = @Login";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Login", login);

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return new LoginResult
                        {
                            Login = reader["Login"].ToString(),
                            Role = reader["Role"].ToString(),
                            IsBanned = Convert.ToBoolean(reader["IsBanned"])
                        };
                    }
                    return null;
                }
            }
        }
        public void SetUserBanStatus(int userId, bool isBanned)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                string query = "UPDATE Users SET IsBanned = @IsBanned , BanReasonID = NULL WHERE UserID = @UserId";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@IsBanned", isBanned);
                cmd.Parameters.AddWithValue("@UserId", userId);
               
                cmd.ExecuteNonQuery();
            }
        }
        public void BanUser(int userId, int reasonId, string adminNote)
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                conn.Open();

              
                var updateUser = new SqlCommand("UPDATE Users SET IsBanned = 1, BanReasonID = @ReasonId WHERE UserID = @UserId", conn);
                updateUser.Parameters.AddWithValue("@ReasonId", reasonId);
                updateUser.Parameters.AddWithValue("@UserId", userId);
                updateUser.ExecuteNonQuery();

                
                var insertLog = new SqlCommand("INSERT INTO ViolationLog (UserID, Timestamp, RuleID, Severity, AdminNote) VALUES (@UserId, GETDATE(), @RuleId, @Severity, @Note)", conn);
                insertLog.Parameters.AddWithValue("@UserId", userId);
                insertLog.Parameters.AddWithValue("@RuleId", reasonId); 
                insertLog.Parameters.AddWithValue("@Severity", "ban");
                insertLog.Parameters.AddWithValue("@Note", adminNote ?? "");
                insertLog.ExecuteNonQuery();
            }
        }

        public List<BanReasonItem> GetBanReasons()
        {
            var list = new List<BanReasonItem>();

            using (var conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                var cmd = new SqlCommand("SELECT BanReasonID, BanCode FROM BanCodes", conn);
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(new BanReasonItem
                        {
                            Id = reader.GetInt32(0),
                            Reason = reader.GetString(1)
                        });
                    }
                }
            }

            return list;
        }
        public void DeleteUserByLogin(string login)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                var cmd = new SqlCommand("DELETE FROM Users WHERE Login = @Login", conn);
                cmd.Parameters.AddWithValue("@Login", login);
                cmd.ExecuteNonQuery();
            }
        }
        public DataTable SearchUsers(string keyword)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                string query = @"SELECT UserID, FullName, Login, Email, Role, IsBanned, CreatedAt
                         FROM Users
                         WHERE FullName LIKE @kw OR Login LIKE @kw";

                SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
                adapter.SelectCommand.Parameters.AddWithValue("@kw", "%" + keyword + "%");

                DataTable result = new DataTable();
                adapter.Fill(result);
                return result;
            }
        }
        public bool ValidateFields(string login, string fullName, string email, string passwordHash, string salt, bool isEditMode)
        {
            if (string.IsNullOrWhiteSpace(login) ||
                string.IsNullOrWhiteSpace(fullName) ||
                string.IsNullOrWhiteSpace(email))
            {
                MessageBox.Show("Пожалуйста, заполните все обязательные поля.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (!IsValidEmail(email))
            {
                MessageBox.Show("Некорректный формат электронной почты.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (!isEditMode && (string.IsNullOrWhiteSpace(passwordHash) || string.IsNullOrWhiteSpace(salt)))
            {
                MessageBox.Show("Сначала сгенерируйте хеш пароля.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            return true;
        }

        public void SaveUserToDatabase(string login, string fullName, string email, string passwordHash, string salt, bool isEditMode, bool isBanned)
        {
            if (!IsValidEmail(email))
            {
                MessageBox.Show("Некорректный формат электронной почты. Данные не были сохранены.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                SqlCommand command;

                if (isEditMode)
                {
                    command = new SqlCommand(@"
                UPDATE Users 
                SET FullName = @FullName, Email = @Email, IsBanned = @IsBanned
                WHERE Login = @Login", connection);
                }
                else
                {
                    command = new SqlCommand(@"
                INSERT INTO Users (FullName, Login, Email, PasswordHash, Salt, CreatedAt, IsBanned)
                VALUES (@FullName, @Login, @Email, @PasswordHash, @Salt, @CreatedAt, @IsBanned)", connection);

                    command.Parameters.AddWithValue("@PasswordHash", passwordHash);
                    command.Parameters.AddWithValue("@Salt", salt);
                    command.Parameters.AddWithValue("@CreatedAt", DateTime.Now);
                }

                command.Parameters.AddWithValue("@FullName", fullName);
                command.Parameters.AddWithValue("@Login", login);
                command.Parameters.AddWithValue("@Email", email);
                command.Parameters.AddWithValue("@IsBanned", isBanned);

                command.ExecuteNonQuery();
            }
        }

        private bool IsValidEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return false;

            var pattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
            return Regex.IsMatch(email, pattern, RegexOptions.IgnoreCase);
        }
    }
}
