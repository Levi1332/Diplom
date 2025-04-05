using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diplom
{
    public class UserDataService : IUserDataService
    {
        private int userId;
        private readonly string _connectionString;

        public UserDataService(string connectionString, int userId)
        {
            _connectionString = connectionString;
            this.userId = userId;
        }

        public string GetFullName()
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                string query = "SELECT FullName FROM Users WHERE UserID = @UserId"; 

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@UserId", userId);
                    return cmd.ExecuteScalar()?.ToString() ?? "Неизвестный пользователь";
                }
            }
        }

        public string GetTotalWorkTime()
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                string query = "SELECT SUM(EffectiveTime) FROM WorkSessions WHERE UserID = 1";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    var result = cmd.ExecuteScalar();
                    if (result != null && int.TryParse(result.ToString(), out int seconds))
                    {
                        TimeSpan time = TimeSpan.FromSeconds(seconds);
                        return $"{time.Hours} ч {time.Minutes} мин";
                    }
                }
            }
            return "Нет данных";
        }
    }
}
