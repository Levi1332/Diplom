using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace Diplom.Repository
{
    public class BannedSoftwareRepository
    {
        private readonly string _connectionString;

        public BannedSoftwareRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public List<string> LoadBannedProcesses()
        {
            var result = new List<string>();

            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                var command = new SqlCommand("SELECT AppName FROM BlockedApplications", connection);
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        result.Add(reader.GetString(0).ToLower());
                    }
                }
            }

            return result;
        }

        public bool AddBannedProcess(string appName)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                var checkCmd = new SqlCommand("SELECT COUNT(*) FROM BlockedApplications WHERE AppName = @AppName", connection);
                checkCmd.Parameters.AddWithValue("@AppName", appName);
                int count = (int)checkCmd.ExecuteScalar();

                if (count > 0)
                    return false;

                var insertCmd = new SqlCommand("INSERT INTO BlockedApplications (AppName) VALUES (@AppName)", connection);
                insertCmd.Parameters.AddWithValue("@AppName", appName);
                insertCmd.ExecuteNonQuery();

                return true;
            }
        }

        public void RemoveBannedProcess(string appName)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                var deleteCmd = new SqlCommand("DELETE FROM BlockedApplications WHERE AppName = @AppName", connection);
                deleteCmd.Parameters.AddWithValue("@AppName", appName);
                deleteCmd.ExecuteNonQuery();
            }
        }
    }
}
