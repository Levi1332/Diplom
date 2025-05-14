using System.Collections.Generic;
using Diplom.Core.AntiCheat.Models;

namespace Diplom.Core.AntiCheat.Interfaces
{
    public interface IInputMonitor
    {
        void StartMonitoring();
        void StopMonitoring();
        IEnumerable<InputEvent> GetEvents();
    }

}
