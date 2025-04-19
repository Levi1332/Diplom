using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Diplom.Core;

namespace Diplom
{
    public interface IPdfReportService
    {
        void ExportFullReport(int userId, DateTime from, DateTime to, string filePath, IProgress<int> progress);
    }

}
