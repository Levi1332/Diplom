using System;
using System.Data.SqlClient;
using Diplom;

public class RuleRepository
{
    private readonly string _connectionString = DatabaseConfig.connectionString;

    public int GetRuleId(string ruleName)
    {
        using (var connection = new SqlConnection(_connectionString))
        {
            connection.Open();

            string sql = "SELECT RuleID FROM SecurityRules WHERE RuleName = @ruleName";

            using (var command = new SqlCommand(sql, connection))
            {
                command.Parameters.AddWithValue("@ruleName", ruleName);

                var result = command.ExecuteScalar();

                if (result != null && int.TryParse(result.ToString(), out int ruleId))
                {
                    return ruleId;
                }
                else
                {
                    throw new Exception($"Правило с именем '{ruleName}' не найдено в базе.");
                }
            }
        }
    }
}