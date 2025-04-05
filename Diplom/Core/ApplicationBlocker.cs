using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Windows.Forms;


namespace Diplom
{
    public class ApplicationBlocker : IApplicationBlocker
    {
        private readonly int userId;
        private readonly IViolationRepository repository;
        private readonly List<BlockedApp> blockedApps;

        public ApplicationBlocker(int userId, IViolationRepository repository)
        {
            this.userId = userId;
            this.repository = repository;
            this.blockedApps = repository.GetBlockedApplications();
        }

        public void CheckAndBlock()
        {
            string activeProcess = GetActiveProcessName();
            var blockedApp = blockedApps.FirstOrDefault(app =>
                app.AppName.Equals(activeProcess, StringComparison.OrdinalIgnoreCase));

            if (blockedApp != null)
            {
                BlockApplication(activeProcess);

                repository.LogViolation(userId, blockedApp.AppId);

                ShowWarningMessage(activeProcess);
            }
        }

        public void BlockApplication(string processName)
        {
            foreach (var process in Process.GetProcessesByName(processName))
            {
                try
                {
                    process.Kill();
                    Console.WriteLine($"✅ Приложение {processName} закрыто.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"⚠ Ошибка при закрытии {processName}: {ex.Message}");
                }
            }
        }

        private void ShowWarningMessage(string processName)
        {
            MessageBox.Show(
                $"✅ Приложение {processName} было закрыто. Использование данного ПО в рабочее время запрещено.",
                "Запрещённое ПО",
                MessageBoxButtons.OK,
                MessageBoxIcon.Warning
            );
        }

        private string GetActiveProcessName()
        {
            IntPtr hwnd = NativeMethods.GetForegroundWindow();
            NativeMethods.GetWindowThreadProcessId(hwnd, out uint pid);

            try
            {
                var proc = Process.GetProcessById((int)pid);
                return proc.ProcessName;
            }
            catch
            {
                return string.Empty;
            }
        }
    }
}
