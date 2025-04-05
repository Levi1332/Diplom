using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace Diplom
{
    public class InactivityMonitor
    {
        private Timer inactivityTimer;
        private WorkSessionManager workSession;
        private bool wasPaused = false; 
        private const int InactivityThreshold = 60; 

        public InactivityMonitor(WorkSessionManager sessionManager)
        {
            workSession = sessionManager;
            inactivityTimer = new Timer();
            inactivityTimer.Interval = 1000; 
            inactivityTimer.Tick += CheckInactivity;
            inactivityTimer.Start();

            HookActivityEvents();
        }

        private void CheckInactivity(object sender, EventArgs e)
        {
            int idleTime = GetIdleTimeInSeconds();

            if (idleTime >= InactivityThreshold && workSession.IsRunning)
            {
                workSession.Pause(); 
                wasPaused = true;
                MessageBox.Show("Таймер поставлен на паузу из-за бездействия.", "Бездействие", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void HookActivityEvents()
        {
            Application.Idle += (s, e) => ResumeIfPaused();
        }

        private void ResumeIfPaused()
        {
            if (wasPaused)
            {
                workSession.Resume();
                wasPaused = false;
                MessageBox.Show("Рабочий таймер продолжен.", "Возобновление", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private int GetIdleTimeInSeconds()
        {
            LASTINPUTINFO lastInput = new LASTINPUTINFO();
            lastInput.cbSize = (uint)Marshal.SizeOf(typeof(LASTINPUTINFO));

            if (GetLastInputInfo(ref lastInput))
            {
                return (Environment.TickCount - (int)lastInput.dwTime) / 1000;
            }
            return 0;
        }

        [StructLayout(LayoutKind.Sequential)]
        private struct LASTINPUTINFO
        {
            public uint cbSize;
            public uint dwTime;
        }

        [DllImport("user32.dll")]
        private static extern bool GetLastInputInfo(ref LASTINPUTINFO plii);
    }
}
