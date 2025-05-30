using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Diplom.Core;

namespace Diplom
{
    public interface IPdfReportService
    {
        void ExportFullReport(int userId, DateTime from, DateTime to, string filePath, IProgress<int> progress,string userName);
        void ExportCombinedReport(List<int> userIds, DateTime from, DateTime to, string filePath, IProgress<int> progress, List<string> userNames);
        void ExportCombinedReportForUsers(List<int> userIds, List<string> userNames, DateTime from, DateTime to, string filePath, IProgress<int> progress);
        void OpenPdfFile(string filePath);
    }

}
