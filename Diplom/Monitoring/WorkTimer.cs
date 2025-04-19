using System;
using System.Windows.Forms;

namespace Diplom { 
public class WorkTimer
{
    protected System.Windows.Forms.Timer timer;
    protected int secondsElapsed;
    protected Label lblTimer;
    private bool isRunning;
    protected DateTime? pauseTime = null;
    private bool isPaused = false;
    protected DateTime StartTime; 
    
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
        if (isPaused) return;

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
        StartTime = DateTime.Now; 
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
        pauseTime = DateTime.Now;
        timer.Stop();
        isRunning = false;
        isPaused = true;
    }

    public void Resume()
    {
        timer.Start();
        isRunning = true;
        isPaused = false;
    }


    public bool IsRunning
    {
        get { return isRunning; }
    }
    public void Disable() => timer.Stop();
    public void Enable() => timer.Start();

}
}
