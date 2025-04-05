using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Timers;

namespace Diplom
{
    public class ApplicationTracker
    {
        private readonly int userId;
        private readonly IApplicationRepository repository;
        private readonly IBlockedAppsRepository blockedAppsRepo;
        private readonly IApplicationBlocker applicationBlocker;
        private readonly Timer trackingTimer;

        public ApplicationTracker(int userId, IApplicationRepository repository, IBlockedAppsRepository blockedAppsRepo, IApplicationBlocker applicationBlocker)
        {
            this.userId = userId;
            this.repository = repository;
            this.blockedAppsRepo = blockedAppsRepo;
            this.applicationBlocker = applicationBlocker;

            trackingTimer = new Timer(1000); 
            trackingTimer.Elapsed += TrackApplication;
        }

        public void StartTracking() => trackingTimer.Start();
        public void StopTracking() => trackingTimer.Stop();

        private void TrackApplication(object sender, ElapsedEventArgs e)
        {
            string activeApp = GetActiveApplication();
            if (string.IsNullOrEmpty(activeApp)) return;

           
            repository.SaveApplicationUsage(userId, activeApp, "", DateTime.Now);

           
            applicationBlocker.CheckAndBlock();
        }


        private string GetActiveApplication()
        {
            IntPtr hwnd = NativeMethods.GetForegroundWindow();
            if (hwnd == IntPtr.Zero) return null;

            NativeMethods.GetWindowThreadProcessId(hwnd, out uint processId);
            Process process = Process.GetProcessById((int)processId);

            return process.ProcessName;
        }
    }
}
