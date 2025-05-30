using System;
using System.Data.SqlClient;
using System.IO;

namespace Diplom
{
    public class ApplicationRepository : IApplicationRepository
    {
        private readonly string logFilePath = "activity_log.txt";
        private string lastLoggedApp = string.Empty;
        private string lastLoggedWindow = string.Empty;

        public ApplicationRepository(string connectionString)
        {
            EnsureLogFile();
        }

        public void SaveApplicationUsage(int userId, string appName, string windowTitle, DateTime timestamp)
        {
            
            if (appName == lastLoggedApp && windowTitle == lastLoggedWindow)
                return;

            lastLoggedApp = appName;
            lastLoggedWindow = windowTitle;

            TrimLogIfTooBig();

            string logEntry = $"{timestamp:yyyy-MM-dd HH:mm:ss} - {appName} - {windowTitle}";
            File.AppendAllText(logFilePath, logEntry + Environment.NewLine);
        }

        private void EnsureLogFile()
        {
            if (!File.Exists(logFilePath))
                File.Create(logFilePath).Close();
        }

        private void TrimLogIfTooBig()
        {
            string[] lines = File.ReadAllLines(logFilePath);

            if (lines.Length >= 500)
            {
                File.WriteAllText(logFilePath, string.Empty);
                lastLoggedApp = string.Empty;
                lastLoggedWindow = string.Empty;
            }
        }
        public TimeSpan GetWorkedTimeToday(int userId)
        {
            var totalSeconds = 0;

            using (var connection = new SqlConnection(DatabaseConfig.connectionString))
            {
                connection.Open();

                var command = new SqlCommand(@"
            SELECT ISNULL(SUM(EffectiveTime), 0)
            FROM [EmployeeMonitoring].[dbo].[WorkSessions]
            WHERE 
                [UserID] = @UserId AND
                CAST([StartTime] AS DATE) = CAST(GETDATE() AS DATE)", connection);

                command.Parameters.AddWithValue("@UserId", userId);

                var result = command.ExecuteScalar();

                if (result != null && int.TryParse(result.ToString(), out int seconds))
                {
                    totalSeconds = seconds;
                }
            }

            return TimeSpan.FromSeconds(totalSeconds);
        }

    }
}
