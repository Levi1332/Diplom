using System;
using System.Data.SqlClient;

namespace Diplom
{
    public class UserSession
    {
        private string connectionString = DatabaseConfig.connectionString;

        public int GetUserIdByLogin(string login)
        {
            int userId = -1;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    string query = "SELECT UserID FROM Users WHERE Login = @Login";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@Login", login);
                        object result = cmd.ExecuteScalar();

                        if (result != null)
                        {
                            userId = Convert.ToInt32(result);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Ошибка при получении UserID: " + ex.Message);
                }
            }
            return userId;
        }
    }
}
