using System;
using System.Windows.Forms;

namespace Diplom
{
    public class WorkTimer
    {
        protected System.Windows.Forms.Timer timer;
        protected int secondsElapsed;
        protected Label lblTimer;
        private bool isRunning; 

        public WorkTimer(Label timerLabel)
        {
            lblTimer = timerLabel;
            timer = new System.Windows.Forms.Timer();
            timer.Interval = 1000;
            timer.Tick += OnTimerTick;
            secondsElapsed = 0;
            isRunning = false; 
        }

        private void OnTimerTick(object sender, EventArgs e)
        {
            secondsElapsed++;
            UpdateTimerDisplay();
        }

        private void UpdateTimerDisplay()
        {
            int hours = secondsElapsed / 3600;
            int minutes = (secondsElapsed % 3600) / 60;
            int seconds = secondsElapsed % 60;

            if (lblTimer.InvokeRequired)
            {
                lblTimer.Invoke(new Action(() =>
                {
                    lblTimer.Text = $"{hours:D2}:{minutes:D2}:{seconds:D2}";
                }));
            }
            else
            {
                lblTimer.Text = $"{hours:D2}:{minutes:D2}:{seconds:D2}";
            }
        }

        public virtual void Start()
        {
            timer.Start();
            isRunning = true; 
        }

        public virtual void Stop()
        {
            timer.Stop();
            isRunning = false; 
        }

        public void Pause()
        {
            timer.Stop();
            isRunning = false; 
        }

        public void Resume()
        {
            timer.Start();
            isRunning = true; 
        }

        public bool IsRunning
        {
            get { return isRunning; } 
        }
    }
}
