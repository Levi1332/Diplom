using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diplom
{
    public interface IViolationService
    {
        DataTable GetViolations();
        DataTable GetViolations(int userId);
    }
}
