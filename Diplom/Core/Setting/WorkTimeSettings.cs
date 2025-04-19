using System;

namespace Diplom
{
    public class WorkTimeSettings
    {
        public TimeSpan LunchTime { get; set; }
        public TimeSpan LunchDuration { get; set; }
        public string PdfFileNameTemplate { get; set; } = "Отчет_{Дата}.pdf";
        public string DefaultExportPath { get; set; } = "";
        public bool OpenPdfAfterExport { get; set; } = true;
        public string Language { get; set; } = "ru";
        public string Theme { get; set; } = "system";
        public float TimerFontSize { get; set; } = 48f;
        public float ButtonFontSize { get; set; } = 18f;
        public string FontFamily { get; set; } = "Segoe UI";
        public bool NotifyStartDay { get; set; } = true;
        public bool NotifyEndDay { get; set; } = true;
        public bool NotifyViolations { get; set; } = true;
        public bool NotifyLogin { get; set; } = true;
        public bool NotifyLogout { get; set; } = true;

    }

}
