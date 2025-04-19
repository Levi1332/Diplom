using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diplom
{
    public class OvertimeService : IOvertimeService
    {
        private readonly string _connectionString;

        public OvertimeService(string connectionString)
        {
            _connectionString = connectionString;
        }

        public List<OvertimeSession> GetUserOvertimes(int userId, DateTime from, DateTime to)
        {
            var result = new List<OvertimeSession>();

            using (var connection = new SqlConnection(_connectionString))
            {
                var cmd = new SqlCommand(@"
                SELECT OvertimeID, UserID, StartTime, ExtraTime
                FROM OvertimeSessions
                WHERE UserID = @userId AND StartTime BETWEEN @from AND @to", connection);

                cmd.Parameters.AddWithValue("@userId", userId);
                cmd.Parameters.AddWithValue("@from", from);
                cmd.Parameters.AddWithValue("@to", to);

                connection.Open();
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        result.Add(new OvertimeSession
                        {
                            OvertimeID = reader.GetInt32(0),
                            UserID = reader.GetInt32(1),
                            StartTime = reader.GetDateTime(2),
                            ExtraTime = reader.GetInt32(3)
                        });
                    }
                }
            }

            return result;
        }
    }

}
