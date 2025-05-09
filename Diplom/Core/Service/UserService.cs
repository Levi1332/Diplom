using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
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
                            //UserId = removed
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

    }
}
