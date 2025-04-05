using Diplom;
using System.Collections.Generic;
using System.Data.SqlClient;
using System;

public class BlockedAppsRepository : IBlockedAppsRepository
{
    private readonly string connectionString;

    public BlockedAppsRepository(string connectionString)
    {
        this.connectionString = connectionString;
    }

    public List<string> GetBlockedApplications()
    {
        List<string> blockedApps = new List<string>();
        using (SqlConnection conn = new SqlConnection(connectionString))
        {
            try
            {
                conn.Open();
                string query = "SELECT AppName FROM BlockedApplications";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        blockedApps.Add(reader.GetString(0).ToLower());
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ошибка загрузки заблокированных приложений: " + ex.Message);
            }
        }
        return blockedApps;
    }
}
