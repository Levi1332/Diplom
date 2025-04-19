using System;
using System.Windows.Forms;

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
        private DateTime lastInteraction = DateTime.Now;

        public InactivityMonitor(WorkSessionManager sessionManager, MainForm form)
        {
            workSession = sessionManager;
            mainForm = form;
            _settings = SettingsManager.LoadSettings();

            inactivityTimer = new Timer
            {
                Interval = 1000
            };
            inactivityTimer.Tick += CheckInactivity;
            inactivityTimer.Start();

            HookActivityEvents();
        }

        private void CheckInactivity(object sender, EventArgs e)
        {
            if (mainForm.IsLunchBreak || !workSession.IsRunning || !isMonitoring || wasPausedByInactivity)
                return;

            int idleTime = (int)(DateTime.Now - lastInteraction).TotalSeconds;

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

        public void ResetIdleTimer()
        {
            lastInteraction = DateTime.Now;
        }

        public void PauseMonitoring() => isMonitoring = false;
        public void ResumeMonitoring() => isMonitoring = true;
        public bool IsMonitoringActive => isMonitoring;

        private void HookActivityEvents()
        {
            Application.Idle += (s, e) => ResumeIfPaused();
            Application.AddMessageFilter(new ActivityMessageFilter(this));
        }

        private void ResumeIfPaused()
        {
            if (wasPaused)
            {
                workSession.Resume();
                wasPaused = false;
                wasPausedByInactivity = false; 
                MessageBox.Show("Рабочий таймер продолжен.", "Возобновление", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}
