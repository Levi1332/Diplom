using System;
using System.Collections.Generic;
using Diplom.Core;

namespace Diplom
{
    public interface IWorkSessionService
    {
        Dictionary<DateTime, TimeSpan> GetUserDailyWorkTime(int userId);
        Dictionary<DateTime, TimeSpan> GetUserDailyWorkTime(int userId, int month, int year);
        List<WorkSession> GetUserSessions(int userId);
    }
}
