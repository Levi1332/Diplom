using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diplom
{
    internal class CBanInfo
    {
        private static string connectionString = DatabaseConfig.connectionString;
        public string GetBanInfo(int banReasonID)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = "SELECT BanCode, Description FROM BanCodes WHERE BanReasonID = @BanReasonID";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@BanReasonID", banReasonID);

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        string banCode = reader.GetString(0);
                        string banDescription = reader.GetString(1);
                        return $"Код блокировки: {banCode}\nПричина: {banDescription}";
                    }
                }
            }
            return "Неизвестная причина блокировки.";
        }
    }
}
