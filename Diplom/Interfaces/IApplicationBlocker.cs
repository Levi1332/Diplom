using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diplom
{
    public interface IApplicationBlocker
    {
        void CheckAndBlock();
        void BlockApplication(string processName);
    }

}
