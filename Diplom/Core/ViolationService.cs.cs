using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diplom
{
    public class ViolationService : IViolationService
    {
        private int userId;
        private readonly string connectionString;

        public ViolationService(string connectionString, int userId)
        {
            this.connectionString = connectionString;
            this.userId = userId;
        }

        public DataTable GetViolations()
        {
            var table = new DataTable();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = @"
                   SELECT 
                         V.ViolationID,
                         U.FullName AS [Сотрудник],
                         V.Timestamp AS [Время нарушения],
                         S.RuleName AS [Нарушенное правило],
                         V.Severity AS [Степень],
                         V.AdminNote AS [Комментарий администратора]
                   FROM ViolationLog V
                   JOIN Users U ON V.UserID = U.UserID
                   JOIN SecurityRules S ON V.RuleID = S.RuleID
                   ORDER BY V.Timestamp DESC;";

                using (SqlDataAdapter adapter = new SqlDataAdapter(query, conn))
                {
                    adapter.Fill(table);
                }
            }

            return table;
        }
    }
}