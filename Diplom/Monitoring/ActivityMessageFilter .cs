using System.Windows.Forms;

namespace Diplom
{
    public class ActivityMessageFilter : IMessageFilter
    {
        private readonly InactivityMonitor monitor;

        public ActivityMessageFilter(InactivityMonitor monitor)
        {
            this.monitor = monitor;
        }

        public bool PreFilterMessage(ref Message m)
        {
           
            if (m.Msg == 0x200 || 
                m.Msg == 0x201 || 
                m.Msg == 0x100 || 
                m.Msg == 0x101)  
            {
                monitor.ResetIdleTimer();
            }

            return false;
        }
    }
}
