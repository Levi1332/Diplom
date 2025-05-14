using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Diplom.Core.AntiCheat.Interfaces;
using Diplom.Core.AntiCheat.Models;

namespace Diplom.Core.AntiCheat.Monitors
{
    public class MouseMonitor : IInputMonitor
    {
        private readonly ConcurrentBag<InputEvent> _events = new ConcurrentBag<InputEvent>();
        private Timer _timer;

        public void StartMonitoring()
        {
            _timer = new Timer(CheckMouse, null, 0, 100);
        }

        private void CheckMouse(object state)
        {
            if (GetCursorPos(out Point p))
            {
                _events.Add(new InputEvent
                {
                    Timestamp = DateTime.Now,
                    EventType = "Mouse",
                    Data = $"{p.X},{p.Y}"
                });
            }
        }

        public IEnumerable<InputEvent> GetEvents() => _events.ToList();
        public void StopMonitoring() => _timer?.Dispose();

        [DllImport("user32.dll")]
        private static extern bool GetCursorPos(out Point lpPoint);

        [StructLayout(LayoutKind.Sequential)]
        private struct Point
        {
            public int X;
            public int Y;
        }
    }

}
