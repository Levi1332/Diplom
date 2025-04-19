using System;

namespace Diplom.Core
{
    public class WorkSession
    {
        public DateTime StartTime { get; set; }
        public DateTime? EndTime { get; set; } 
        public TimeSpan WorkDuration { get; set; }
    }
}
