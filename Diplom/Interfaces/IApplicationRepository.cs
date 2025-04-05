using System;

namespace Diplom
{
    public interface IApplicationRepository
    {
        void SaveApplicationUsage(int userId, string appName, string windowTitle, DateTime timestamp);
    }
}
