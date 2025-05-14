using System;
using Diplom.Enum;

namespace Diplom.Core.AntiCheat.Models
{
    public class InputEvent
    {
        public DateTime Timestamp { get; set; }
        public string EventType { get; set; } 
        public string Data { get; set; }

    }

}
