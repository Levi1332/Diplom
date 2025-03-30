using System;
using System.Windows.Forms;

namespace Diplom
{
    public class WorkTimer
    {
        private System.Windows.Forms.Timer timer;
        private int secondsElapsed;
        private Label lblTimer;

        public WorkTimer(Label timerLabel)
        {
            lblTimer = timerLabel;
            timer = new System.Windows.Forms.Timer(); 
            timer.Interval = 1000; 
            timer.Tick += OnTimerTick;
            secondsElapsed = 0;
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

        public void Start()
        {
            secondsElapsed = 0;
            timer.Start();
        }

        public void Stop()
        {
            timer.Stop();
        }
    }
}
