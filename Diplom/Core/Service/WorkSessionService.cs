using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using Diplom.Core;

namespace Diplom
{
    public class WorkSessionService : IWorkSessionService
    {
        private readonly string _connectionString;

        public WorkSessionService(string connectionString)
        {
            _connectionString = connectionString;
        }

        public Dictionary<DateTime, TimeSpan> GetUserDailyWorkTime(int userId)
        {
            var result = new Dictionary<DateTime, TimeSpan>();

            using (var conn = new SqlConnection(_connectionString))
            {
                conn.Open();

                string query = @"
                    SELECT 
                        CAST(StartTime AS DATE) as WorkDate,
                        SUM(EffectiveTime) as TotalSeconds
                    FROM WorkSessions
                    WHERE UserID = @userId
                    GROUP BY CAST(StartTime AS DATE)";

                using (var cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@userId", userId);
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var date = reader.GetDateTime(0);
                            var totalSeconds = reader.GetInt32(1);
                            result[date] = TimeSpan.FromSeconds(totalSeconds);
                        }
                    }
                }
            }

            return result;
        }
        public Dictionary<DateTime, TimeSpan> GetUserDailyWorkTime(int userId, int month, int year)
        {
            var result = new Dictionary<DateTime, TimeSpan>();

            using (var conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                string query = @"
            SELECT CAST(StartTime AS DATE) AS WorkDate,
                   SUM(EffectiveTime) AS TotalSeconds
            FROM WorkSessions
            WHERE UserID = @UserID
              AND MONTH(StartTime) = @Month
              AND YEAR(StartTime) = @Year
            GROUP BY CAST(StartTime AS DATE)";

                using (var cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@UserID", userId);
                    cmd.Parameters.AddWithValue("@Month", month);
                    cmd.Parameters.AddWithValue("@Year", year);

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            DateTime date = reader.GetDateTime(0);
                            int totalSeconds = reader.GetInt32(1);
                            result[date] = TimeSpan.FromSeconds(totalSeconds);
                        }
                    }
                }
            }

            return result;
        }
        public List<WorkSession> GetUserSessions(int userId)
        {
            var sessions = new List<WorkSession>();

            using (var conn = new SqlConnection(_connectionString))
            {
                conn.Open();

                string query = @"
            SELECT StartTime, EndTime
            FROM WorkSessions
            WHERE UserID = @userId
            ORDER BY StartTime";

                using (var cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@userId", userId);
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            sessions.Add(new WorkSession
                            {
                                StartTime = reader.GetDateTime(0),
                                EndTime = reader.IsDBNull(1) ? (DateTime?)null : reader.GetDateTime(1)
                            });
                        }
                    }
                }
            }

            return sessions;
        }
    }
}
