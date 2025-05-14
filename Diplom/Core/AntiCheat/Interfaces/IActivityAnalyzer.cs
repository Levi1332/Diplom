using System.Collections.Generic;
using Diplom.Core.AntiCheat.Models;

namespace Diplom.Core.AntiCheat.Interfaces
{
    public interface IActivityAnalyzer
    {
        bool IsSuspicious(IEnumerable<InputEvent> events);
    }

}
