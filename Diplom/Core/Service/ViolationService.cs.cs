using System;
using System.Data;
using System.Data.SqlClient;

namespace Diplom
{
    public class ViolationService : IViolationService
    {
        private readonly string connectionString;
        private readonly int userId;

        public ViolationService(string connectionString, int userId)
        {
            this.connectionString = connectionString;
            this.userId = userId;
        }
        public ViolationService(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public DataTable GetViolations(int userId)
        {
            var table = new DataTable();

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string query = @"
                        SELECT 
                            V.ViolationID,
                            U.FullName AS [Сотрудник],
                            V.Timestamp AS [Время нарушения],
                            S.BanCode AS [Код нарушения],
                            S.Description AS [Расшифровка нарушения],
                            V.Severity AS [Степень],
                            V.AdminNote AS [Комментарий администратора]
                        FROM ViolationLog V
                        JOIN Users U ON V.UserID = U.UserID
                        JOIN BanCodes S ON V.RuleID = S.BanReasonID
                        WHERE V.UserID = @UserID";

                    using (SqlCommand command = new SqlCommand(query, conn))
                    {
                        command.Parameters.AddWithValue("@UserID", userId);

                        using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                        {
                            adapter.Fill(table);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при получении данных: {ex.Message}");
            }

            return table;
        }
    
        public DataTable GetViolations()
        {
            var table = new DataTable();

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string query = @"
                        SELECT 
                            V.ViolationID,
                            U.FullName AS [Сотрудник],
                            V.Timestamp AS [Время нарушения],
                            S.BanCode AS [Код нарушения],
							S.Description AS [Расшифровка нарушения],
                            V.Severity AS [Степень],
                            V.AdminNote AS [Комментарий администратора]
                        FROM ViolationLog V
                        JOIN Users U ON V.UserID = U.UserID
                        JOIN BanCodes S ON V.RuleID = S.BanReasonID
                        WHERE V.UserID = @UserID";

                    using (SqlCommand command = new SqlCommand(query, conn))
                    {
                        command.Parameters.AddWithValue("@UserID", userId);

                        using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                        {
                            adapter.Fill(table);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                
                Console.WriteLine($"Ошибка при получении данных: {ex.Message}");
            }

            return table;
        }
    }
}
