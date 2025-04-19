using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diplom
{
    public interface IOvertimeService
    {
        List<OvertimeSession> GetUserOvertimes(int userId, DateTime from, DateTime to);
    }
}
