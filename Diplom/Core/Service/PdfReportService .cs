using QuestPDF.Fluent;
using QuestPDF.Helpers;
using System;
using System.Linq;
using Diplom;
using System.Data;
using System.Collections.Generic;
using Diplom.Core;
using System.Diagnostics;
using System.Windows.Forms;
using System.IO;


public class PdfReportService : IPdfReportService
{
    private readonly IWorkSessionService _workSessionService;
    private readonly IViolationService _violationService;
    private readonly IOvertimeService _overtimeService;

    public PdfReportService(IWorkSessionService ws, IViolationService vs, IOvertimeService os)
    {
        _workSessionService = ws;
        _violationService = vs;
        _overtimeService = os;
    }
    public PdfReportService(){ }

    public void ExportCombinedReportForUsers(List<int> userIds, List<string> userNames, DateTime from, DateTime to, string filePath, IProgress<int> progress)
    {
        var userCount = userIds.Count;
        var progressStep = 100 / Math.Max(userCount, 1);
        int completed = 0;

        var document = Document.Create(doc =>
        {
            foreach (var i in Enumerable.Range(0, userCount))
            {
                int userId = userIds[i];
                string userName = userNames.Count > i ? userNames[i] : $"ID {userId}";

                var sessions = _workSessionService.GetUserSessions(userId)
                    .Where(s => s.StartTime.Date >= from.Date && s.StartTime.Date <= to.Date)
                    .OrderBy(s => s.StartTime)
                    .ToList();

                var overtimes = _overtimeService.GetUserOvertimes(userId, from, to);

                var violations = _violationService.GetViolations(userId)
                    .AsEnumerable()
                    .Where(v => v.Field<DateTime>("Время нарушения").Date >= from.Date && v.Field<DateTime>("Время нарушения").Date <= to.Date)
                    .ToList();

                doc.Page(page =>
                {
                    page.Margin(30);

                    page.Header().Text($"📄 Отчёт пользователя {userName}")
                        .FontSize(20)
                        .Bold()
                        .FontColor(Colors.Blue.Darken2);

                    page.Content().Column(col =>
                    {
                        col.Spacing(20);

                        col.Item().Text($"📅 Период: {from:dd.MM.yyyy} - {to:dd.MM.yyyy}")
                            .FontSize(12);
                        col.Item().Text($"🕒 Сформировано: {DateTime.Now:g}")
                            .FontSize(10)
                            .FontColor(Colors.Grey.Darken1);

                        col.Item().Element(Block).Column(inner =>
                        {
                            inner.Item().Text("1. Учёт рабочего времени")
                                .Bold()
                                .FontSize(14);
                            inner.Item().Element(c => AddSessionTable(c, sessions));
                        });

                        col.Item().Element(Block).Column(inner =>
                        {
                            inner.Item().Text("2. Переработки")
                                .Bold()
                                .FontSize(14);
                            inner.Item().Element(c => AddOvertimeTable(c, overtimes));
                        });

                        col.Item().Element(Block).Column(inner =>
                        {
                            inner.Item().Text("3. Нарушения")
                                .Bold()
                                .FontSize(14);
                            inner.Item().Element(c => AddViolationTable(c, violations));
                        });
                    });

                    page.Footer().AlignCenter()
                        .Text($"📌 Документ сгенерирован автоматически. Общий отчёт | {DateTime.Now:g}")
                        .FontSize(9)
                        .FontColor(Colors.Grey.Medium);
                });

                completed++;
                progress?.Report(completed * progressStep);
            }
        });

        document.GeneratePdf(filePath);
        progress?.Report(100);
    }
    public void ExportFullReport(int userId, DateTime from, DateTime to, string filePath, IProgress<int> progress,string userName)
    {
        var sessions = _workSessionService.GetUserSessions(userId)
            .Where(s => s.StartTime.Date >= from.Date && s.StartTime.Date <= to.Date)
            .OrderBy(s => s.StartTime)
            .ToList();

        var overtimes = _overtimeService.GetUserOvertimes(userId, from, to);
        var violations = _violationService.GetViolations(userId)
            .AsEnumerable()
            .Where(v => v.Field<DateTime>("Время нарушения").Date >= from.Date && v.Field<DateTime>("Время нарушения").Date <= to.Date)
            .ToList();

        var document = Document.Create(doc =>
        {
            doc.Page(page =>
            {
                page.Margin(30);
                page.Header().Text($"📄 Отчёт пользователя {userName}").FontSize(20).Bold().FontColor(Colors.Blue.Darken2);

                page.Content().Column(col =>
                {
                    col.Spacing(20);

                    col.Item().Text($"📅 Период: {from:dd.MM.yyyy} - {to:dd.MM.yyyy}").FontSize(12);
                    col.Item().Text($"🕒 Сформировано: {DateTime.Now:g}").FontSize(10).FontColor(Colors.Grey.Darken1);

                    col.Item().Element(Block).Column(inner =>
                    {
                        inner.Item().Text("1. Учёт рабочего времени").Bold().FontSize(14);
                        inner.Item().Element(c => AddSessionTable(c, sessions));
                    });

                    progress?.Report(20);

                    col.Item().Element(Block).Column(inner =>
                    {
                        inner.Item().Text("2. Переработки").Bold().FontSize(14);
                        inner.Item().Element(c => AddOvertimeTable(c, overtimes));
                    });

                    progress?.Report(40);

                    col.Item().Element(Block).Column(inner =>
                    {
                        inner.Item().Text("3. Нарушения").Bold().FontSize(14);
                        inner.Item().Element(c => AddViolationTable(c, violations));
                    });

                    progress?.Report(70);
                });

                page.Footer().AlignCenter().Text("📌 Документ сгенерирован автоматически.")
                    .FontSize(9).FontColor(Colors.Grey.Medium);
            });
        });

        document.GeneratePdf(filePath);
        progress?.Report(100);
    }



    public void ExportCombinedReport(List<int> userIds, DateTime from, DateTime to, string filePath, IProgress<int> progress, List<string> userNames)
    {
        var userCount = userIds.Count;
        var progressStep = 100 / Math.Max(userCount, 1);
        int completed = 0;

        var document = Document.Create(doc =>
        {
            foreach (var i in Enumerable.Range(0, userCount))
            {
                int userId = userIds[i];
                string userName = userNames.Count > i ? userNames[i] : $"ID {userId}";

                var sessions = _workSessionService.GetUserSessions(userId)
                    .Where(s => s.StartTime.Date >= from.Date && s.StartTime.Date <= to.Date)
                    .OrderBy(s => s.StartTime)
                    .ToList();

                var overtimes = _overtimeService.GetUserOvertimes(userId, from, to);
                var violations = _violationService.GetViolations(userId)
                    .AsEnumerable()
                    .Where(v => v.Field<DateTime>("Время нарушения").Date >= from.Date && v.Field<DateTime>("Время нарушения").Date <= to.Date)
                    .ToList();

                doc.Page(page =>
                {
                    page.Margin(30);

                    page.Header().Text($"📄 Отчёт пользователя {userName}").FontSize(20).Bold().FontColor(Colors.Blue.Darken2);

                    page.Content().Column(col =>
                    {
                        col.Spacing(20);

                        col.Item().Text($"📅 Период: {from:dd.MM.yyyy} - {to:dd.MM.yyyy}").FontSize(12);
                        col.Item().Text($"🕒 Сформировано: {DateTime.Now:g}").FontSize(10).FontColor(Colors.Grey.Darken1);

                        col.Item().Element(Block).Column(inner =>
                        {
                            inner.Item().Text("1. Учёт рабочего времени").Bold().FontSize(14);
                            inner.Item().Element(c => AddSessionTable(c, sessions));
                        });

                        col.Item().Element(Block).Column(inner =>
                        {
                            inner.Item().Text("2. Переработки").Bold().FontSize(14);
                            inner.Item().Element(c => AddOvertimeTable(c, overtimes));
                        });

                        col.Item().Element(Block).Column(inner =>
                        {
                            inner.Item().Text("3. Нарушения").Bold().FontSize(14);
                            inner.Item().Element(c => AddViolationTable(c, violations));
                        });
                    });

                    page.Footer().AlignCenter().Text($"📌 Документ сгенерирован автоматически. Общий отчёт | {DateTime.Now:g}")
                        .FontSize(9).FontColor(Colors.Grey.Medium);
                });

                completed++;
                progress?.Report(completed * progressStep);
            }
        });

        document.GeneratePdf(filePath);
        progress?.Report(100);

    }

    private void AddSessionTable(QuestPDF.Infrastructure.IContainer container, List<WorkSession> sessions)
    {
        container.Column(col =>
        {
          
            col.Item().Element(TableStyle).Table(table =>
            {
                table.ColumnsDefinition(columns =>
                {
                    columns.RelativeColumn(2); // Дата
                    columns.RelativeColumn(1); // Начало
                    columns.RelativeColumn(1); // Окончание
                    columns.RelativeColumn(2); // Отработано
                });

                table.Header(header =>
                {
                    header.Cell().Element(CellHeader).Text("Дата");
                    header.Cell().Element(CellHeader).Text("Начало");
                    header.Cell().Element(CellHeader).Text("Окончание");
                    header.Cell().Element(CellHeader).Text("Отработано");
                });

                if (sessions.Count == 0)
                {
                    table.Cell().ColumnSpan(4).Element(CellContent).Text("Нет данных").Italic();
                    return;
                }

                foreach (var s in sessions)
                {
                    var dur = s.EndTime.HasValue ? s.EndTime.Value - s.StartTime : TimeSpan.Zero;
                    table.Cell().Element(CellContent).Text(s.StartTime.ToString("dd.MM.yyyy"));
                    table.Cell().Element(CellContent).Text(s.StartTime.ToString("HH:mm"));
                    table.Cell().Element(CellContent).Text(s.EndTime?.ToString("HH:mm") ?? "-");
                    table.Cell().Element(CellContent).Text(dur.ToString(@"hh\:mm"));
                }
            });
        });
    }

    private void AddOvertimeTable(QuestPDF.Infrastructure.IContainer container, List<OvertimeSession> overtimes)
    {
        container.Column(col =>
        {
          
            col.Item().Element(TableStyle).Table(table =>
            {
                table.ColumnsDefinition(c =>
                {
                    c.RelativeColumn();
                    c.RelativeColumn();
                    c.RelativeColumn();
                });

                table.Header(h =>
                {
                    h.Cell().Element(CellHeader).Text("Дата");
                    h.Cell().Element(CellHeader).Text("Начало");
                    h.Cell().Element(CellHeader).Text("Сверхурочно");
                });

                if (overtimes.Count == 0)
                {
                    table.Cell().ColumnSpan(3).Element(CellContent).Text("Нет данных").Italic();
                    return;
                }

                foreach (var ot in overtimes)
                {
                    table.Cell().Element(CellContent).Text(ot.StartTime.ToString("dd.MM.yyyy"));
                    table.Cell().Element(CellContent).Text(ot.StartTime.ToString("HH:mm"));
                    table.Cell().Element(CellContent).Text(TimeSpan.FromMinutes(ot.ExtraTime).ToString(@"hh\:mm"));
                }
            });
        });
    }

    private void AddViolationTable(QuestPDF.Infrastructure.IContainer container, List<DataRow> violations)
    {
        container.Column(col =>
        {
          
            col.Item().Element(TableStyle).Table(table =>
            {
                table.ColumnsDefinition(c =>
                {
                    c.RelativeColumn();
                    c.RelativeColumn();
                    c.RelativeColumn();
                    c.RelativeColumn();
                });

                table.Header(h =>
                {
                    h.Cell().Element(CellHeader).Text("Время");
                    h.Cell().Element(CellHeader).Text("Нарушение");
                    h.Cell().Element(CellHeader).Text("Степень");
                    h.Cell().Element(CellHeader).Text("Комментарий");
                });

                if (violations.Count == 0)
                {
                    table.Cell().ColumnSpan(4).Element(CellContent).Text("Нет данных").Italic();
                    return;
                }

                foreach (var row in violations)
                {
                    table.Cell().Element(CellContent).Text(row.Field<DateTime>("Время нарушения").ToString("dd.MM.yyyy HH:mm"));
                    table.Cell().Element(CellContent).Text(row.Field<string>("Расшифровка нарушения") ?? "-");
                    table.Cell().Element(CellContent).Text(row.Field<string>("Степень") ?? "-");
                    table.Cell().Element(CellContent).Text(row.Field<string>("Комментарий администратора") ?? "-");
                }
            });
        });
    }
    public void OpenPdfFile(string filePath)
    {
        try
        {
            if (File.Exists(filePath))
            {
                Process.Start(new ProcessStartInfo
                {
                    FileName = filePath,
                    UseShellExecute = true
                });
            }
            else
            {
                MessageBox.Show("Файл не найден для открытия.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Не удалось открыть файл: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }


    private static QuestPDF.Infrastructure.IContainer CellHeader(QuestPDF.Infrastructure.IContainer container) =>
         container.Border(1)
                  .BorderColor(Colors.Grey.Lighten2)
                  .Background(Colors.Grey.Lighten3)
                  .Padding(5)
                  .DefaultTextStyle(x => x.SemiBold().FontColor(Colors.Black));

    private static QuestPDF.Infrastructure.IContainer CellContent(QuestPDF.Infrastructure.IContainer container) =>
        container.Border(1)
                 .BorderColor(Colors.Grey.Lighten2)
                 .Padding(5);

    private static QuestPDF.Infrastructure.IContainer TableStyle(QuestPDF.Infrastructure.IContainer container) =>
     container.Border(1)
              .BorderColor(Colors.Grey.Lighten1)
              .PaddingVertical(5)
              .Background(Colors.White); 

    private static QuestPDF.Infrastructure.IContainer Block(QuestPDF.Infrastructure.IContainer container) =>
        container.Padding(10).Border(1).BorderColor(Colors.Grey.Lighten2).Background(Colors.White);
}
