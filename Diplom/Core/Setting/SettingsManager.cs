using Diplom;
using System;
using System.IO;

public static class SettingsManager
{
    private static readonly string FilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "settings.json");

    public static WorkTimeSettings LoadSettings()
    {
        if (!File.Exists(FilePath))
            return new WorkTimeSettings();

        string json = File.ReadAllText(FilePath);
        var obj = Newtonsoft.Json.Linq.JObject.Parse(json);

        return new WorkTimeSettings
        {
            LunchTime = TimeSpan.Parse((string)(obj["LunchTime"] ?? "12:00:00")),
            LunchDuration = TimeSpan.Parse((string)(obj["LunchDuration"] ?? "00:30:00")),
            PdfFileNameTemplate = (string)(obj["PdfFileNameTemplate"] ?? "Отчет_{Дата}.pdf"),
            DefaultExportPath = (string)(obj["DefaultExportPath"] ?? ""),
            OpenPdfAfterExport = (bool?)obj["OpenPdfAfterExport"] ?? true,
            Language = (string)(obj["Language"] ?? "ru"),
            Theme = (string)(obj["Theme"] ?? "system"),
            FontFamily = (string)(obj["FontFamily"] ?? "Arial"),
            TimerFontSize = (float?)(obj["TimerFontSize"] ?? 36f) ?? 36f,
            ButtonFontSize = (float?)(obj["ButtonFontSize"] ?? 14f) ?? 14f,
            NotifyStartDay = (bool?)obj["NotifyStartDay"] ?? true,
            NotifyEndDay = (bool?)obj["NotifyEndDay"] ?? true,
            NotifyViolations = (bool?)obj["NotifyViolations"] ?? true,
            NotifyLogin = (bool?)obj["NotifyLogin"] ?? true,
            NotifyLogout = (bool?)obj["NotifyLogout"] ?? true,

        };
    }

    public static void SaveSettings(WorkTimeSettings settings)
    {
        var obj = new Newtonsoft.Json.Linq.JObject
        {
            ["LunchTime"] = settings.LunchTime.ToString(),
            ["LunchDuration"] = settings.LunchDuration.ToString(),
            ["PdfFileNameTemplate"] = settings.PdfFileNameTemplate,
            ["DefaultExportPath"] = settings.DefaultExportPath,
            ["OpenPdfAfterExport"] = settings.OpenPdfAfterExport,
            ["Language"] = settings.Language,
            ["Theme"] = settings.Theme,
            ["FontFamily"] = settings.FontFamily,
            ["TimerFontSize"] = settings.TimerFontSize,
            ["ButtonFontSize"] = settings.ButtonFontSize,
            ["NotifyStartDay"] = settings.NotifyStartDay,
            ["NotifyEndDay"] = settings.NotifyEndDay,
            ["NotifyViolations"] = settings.NotifyViolations,
            ["NotifyLogin"] = settings.NotifyLogin,
            ["NotifyLogout"] = settings.NotifyLogout,

        };

        File.WriteAllText(FilePath, obj.ToString());
    }

}
