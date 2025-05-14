using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using Diplom.Core.AntiCheat.Interfaces;
using Diplom.Core.AntiCheat.Models;

namespace Diplom.Core.AntiCheat.Services
{
    public class HiddenAntiCheatService
    {
        private readonly string connectionString = DatabaseConfig.connectionString;
        private readonly List<IInputMonitor> _monitors;
        private readonly List<IActivityAnalyzer> _analyzers;
        private System.Threading.Timer _scanTimer;
        private readonly int userId;

        public HiddenAntiCheatService(IEnumerable<IInputMonitor> monitors, IEnumerable<IActivityAnalyzer> analyzers, int userId)
        {
            _monitors = monitors.ToList();
            _analyzers = analyzers.ToList();
            this.userId = userId;
        }

        public void Start()
        {
            _monitors.ForEach(m => m.StartMonitoring());
            _scanTimer = new System.Threading.Timer(Scan, null, TimeSpan.Zero, TimeSpan.FromMinutes(5));
        }

        private void Scan(object state)
        {
            var allEvents = _monitors.SelectMany(m => m.GetEvents()).ToList();
            var mouseEvents = allEvents.Where(e => e.EventType == "Mouse").ToList();

            foreach (var analyzer in _analyzers)
            {
                if (mouseEvents.Count == 0)
                    continue;

                if (analyzer.IsSuspicious(mouseEvents))
                {
                    LogSuspiciousActivity();
                    break;
                }
            }

            _monitors.ForEach(m => m.StopMonitoring());
            _monitors.ForEach(m => m.StartMonitoring());
        }

        public void LogSuspiciousActivity()
        {
            try
            {
                int ruleId = new RuleRepository().GetRuleId("MousePatternDetection");
                const string adminNote = "Обнаружено использование программного обеспечения для имитации активности.";

                using (var connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    var insertSql = @"
                        INSERT INTO ViolationLog (UserID, RuleID, Severity, AdminNote)
                        VALUES (@userId, @ruleId, @severity, @adminNote)";

                    using (var cmd = new SqlCommand(insertSql, connection))
                    {
                        cmd.Parameters.AddWithValue("@userId", userId);
                        cmd.Parameters.AddWithValue("@ruleId", ruleId);
                        cmd.Parameters.AddWithValue("@severity", "ban");
                        cmd.Parameters.AddWithValue("@adminNote", adminNote);
                        cmd.ExecuteNonQuery();
                    }

                    var updateSql = @"
                        UPDATE Users
                        SET Warnings = Warnings + 1, IsBanned = 1, BanReasonID = 7
                        WHERE UserID = @userId";

                    using (var cmd = new SqlCommand(updateSql, connection))
                    {
                        cmd.Parameters.AddWithValue("@userId", userId);
                        cmd.ExecuteNonQuery();
                    }
                }

                MessageBox.Show(
                    "Обнаружено использование программного обеспечения для имитации активности.\n" +
                    "Ваш аккаунт был временно заблокирован.\n\n" +
                    "Для получения дополнительной информации или разблокировки обратитесь к администратору.",
                    "Блокировка аккаунта",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ошибка при логировании нарушения: " + ex.Message);
            }
        }

        public void Stop()
        {
            _scanTimer?.Dispose();
            _monitors.ForEach(m => m.StopMonitoring());
        }
    }
}
