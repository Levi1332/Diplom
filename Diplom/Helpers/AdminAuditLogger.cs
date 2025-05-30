using System;
using System.Data.SqlClient;
using Diplom.Properties;

namespace Diplom.Helpers
{
    public class AdminAuditLogger
    {
        public static void LogToDb(string action, int userID,string details="")
        {
            try
            {
                using (var connection = new SqlConnection(DatabaseConfig.connectionString))
                using (var cmd = new SqlCommand(@"
                    INSERT INTO AdminAuditLog (Timestamp, UserID, Action, Details) 
                    VALUES (@time, @user, @action, @details)", connection))
                {
                    
                    cmd.Parameters.AddWithValue("@time", DateTime.Now);
                    cmd.Parameters.AddWithValue("@user", userID);
                    cmd.Parameters.AddWithValue("@action", action);
                    cmd.Parameters.AddWithValue("@details", details ?? string.Empty);

                    connection.Open();
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ошибка при логировании действия администратора: " + ex.Message);
            }
        }
    }
}
