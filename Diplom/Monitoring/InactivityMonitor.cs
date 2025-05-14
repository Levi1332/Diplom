using System;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using Diplom.Helpers;
using Diplom.Core.AntiCheat.Services;
using Diplom.Core.AntiCheat.Analyzers;
using Diplom.Core.AntiCheat.Interfaces;
using Diplom.Core.AntiCheat.Monitors;
using System.Collections.Generic;

namespace Diplom
{
    public class InactivityMonitor
    {
        private Timer inactivityTimer;
        private WorkSessionManager workSession;
        private bool wasPaused = false;
        private bool wasPausedByInactivity = false;
        private WorkTimeSettings _settings;
        private const int InactivityThreshold = 40; 
        private bool isMonitoring = true;
        private readonly MainForm mainForm;
       
        public InactivityMonitor(WorkSessionManager sessionManager, MainForm form, int userId)
        {
            workSession = sessionManager;
            mainForm = form;
            _settings = SettingsManager.LoadSettings();

            inactivityTimer = new Timer { Interval = 1000 };
            inactivityTimer.Tick += CheckInactivity;
            inactivityTimer.Start();

            HookActivityEvents();

            
            
        }
        [StructLayout(LayoutKind.Sequential)]
        private struct LASTINPUTINFO
        {
            public uint cbSize;
            public uint dwTime;
        }

        [DllImport("user32.dll")]
        private static extern bool GetLastInputInfo(ref LASTINPUTINFO plii);

        private int GetIdleTimeInSeconds()
        {
            LASTINPUTINFO lastInputInfo = new LASTINPUTINFO();
            lastInputInfo.cbSize = (uint)Marshal.SizeOf(lastInputInfo);
            GetLastInputInfo(ref lastInputInfo);

            uint lastInputTick = lastInputInfo.dwTime;
            uint currentTick = (uint)Environment.TickCount;

            return (int)((currentTick - lastInputTick) / 1000);
        }


        private void CheckInactivity(object sender, EventArgs e)
        {
            if (mainForm.IsLunchBreak || !workSession.IsRunning || !isMonitoring || wasPausedByInactivity)
                return;

            int idleTime = GetIdleTimeInSeconds();

            if (idleTime >= InactivityThreshold)
            {
                workSession.Pause();
                wasPaused = true;
                wasPausedByInactivity = true;
                if (_settings.NotifyViolations)
                { 
                    MessageBox.Show("Таймер поставлен на паузу из-за бездействия.", "Бездействие", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        public void ResetInactivityFlag()
        {
            wasPausedByInactivity = false;
        }

        public void PauseMonitoring() => isMonitoring = false;
        public void ResumeMonitoring() => isMonitoring = true;
    
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
                wasPausedByInactivity = false;
                if (_settings.NotifyViolations)
                {
                    MessageBox.Show("Рабочий таймер продолжен.", "Возобновление", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }
    }
}
