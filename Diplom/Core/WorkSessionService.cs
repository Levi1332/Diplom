using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diplom
{
    public class WorkSessionService : IWorkSessionService
    {
        private int userId;
        private readonly string _connectionString;

        public WorkSessionService(string connectionString, int userId)
        {
            _connectionString = connectionString;
            this.userId = userId;
        }

        public DataTable GetWorkSessions()
        {
            DataTable table = new DataTable();
            table.Columns.Add("Дата начала", typeof(string));
            table.Columns.Add("Дата окончания", typeof(string));
            table.Columns.Add("Длительность", typeof(string));

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                string query = "SELECT StartTime, EndTime, EffectiveTime FROM WorkSessions WHERE UserID = @UserId";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@UserId", userId);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            DateTime start = reader.GetDateTime(0);
                            DateTime end = reader.IsDBNull(1) ? DateTime.MinValue : reader.GetDateTime(1);
                            int seconds = reader.GetInt32(2);
                            TimeSpan duration = TimeSpan.FromSeconds(seconds);

                            string durationFormatted = $"{duration.Hours}ч {duration.Minutes}м";
                            string endFormatted = end != DateTime.MinValue ? end.ToShortDateString() : "—";

                            table.Rows.Add(start.ToShortDateString(), endFormatted, durationFormatted);
                        }
                    }
                }

                return table;
            }
        }
    }
}
