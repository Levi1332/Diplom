using System.Collections.Generic;

namespace Diplom
{
    public interface IViolationRepository
    {
        List<BlockedApp> GetBlockedApplications();
        void LogViolation(int userId, int appId);
    }
}
