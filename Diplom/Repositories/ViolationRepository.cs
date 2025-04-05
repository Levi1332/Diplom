using System;
using System.Collections.Generic;
using System.Data.SqlClient;


namespace Diplom
{
    public class ViolationRepository : IViolationRepository
    {
        private readonly string connectionString;

        public ViolationRepository(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public List<BlockedApp> GetBlockedApplications()
        {
            var apps = new List<BlockedApp>();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                var query = "SELECT id, AppName FROM BlockedApplications";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        apps.Add(new BlockedApp
                        {
                            AppId = reader.GetInt32(0),
                            AppName = reader.GetString(1)
                        });
                    }
                }
            }

            return apps;
        }

        public void LogViolation(int userId, int appId)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = @"
                        INSERT INTO ApplicationMonitor (UserID, AppID, Timestamp) 
                        VALUES (@UserID, @AppID, GETDATE())";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@UserID", userId);
                    cmd.Parameters.AddWithValue("@AppID", appId);
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}
