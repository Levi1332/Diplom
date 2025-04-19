using Aspose.Pdf;
using Aspose.Pdf.Text;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;

namespace Diplom.Core.Service
{
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

        public void ExportFullReport(int userId, DateTime from, DateTime to, string filePath, IProgress<int> progress)
        {
            Document doc = new Document();
            var page = doc.Pages.Add();

            var headerFont = FontRepository.FindFont("Helvetica-Bold");
            var bodyFont = FontRepository.FindFont("Helvetica");

            AddTitle(page, "Отчёт о рабочем времени", headerFont, 16, Aspose.Pdf.Color.FromRgb(System.Drawing.Color.DarkBlue));
            AddParagraph(page, $"Период: {from:dd.MM.yyyy} - {to:dd.MM.yyyy}", bodyFont);
            AddParagraph(page, $"Дата генерации: {DateTime.Now:g}", bodyFont);
            page.Paragraphs.Add(new TextFragment(" "));

            // 1. Таблица учёта рабочего времени
            AddSubTitle(page, "1. Таблица учёта рабочего времени", headerFont);
            var sessions = _workSessionService.GetUserSessions(userId)
                .Where(s => s.StartTime.Date >= from.Date && s.StartTime.Date <= to.Date)
                .OrderBy(s => s.StartTime)
                .ToList();

            var tableWork = CreateTable(new[] { "Дата", "Начало", "Окончание", "Отработано" });

            if (sessions.Count == 0)
            {
                AddEmptyRow(tableWork, "Нет данных о рабочих сессиях", 4);
            }
            else
            {
                for (int i = 0; i < sessions.Count; i++)
                {
                    var s = sessions[i];
                    TimeSpan duration = s.EndTime.HasValue ? s.EndTime.Value - s.StartTime : TimeSpan.Zero;

                    var row = tableWork.Rows.Add();
                    row.Cells.Add(s.StartTime.ToString("dd.MM.yyyy"));
                    row.Cells.Add(s.StartTime.ToString("HH:mm"));
                    row.Cells.Add(s.EndTime?.ToString("HH:mm") ?? "-");
                    row.Cells.Add(duration.ToString(@"hh\:mm"));

                    progress?.Report((i + 1) * 20 / Math.Max(sessions.Count, 1));
                }
            }

            page.Paragraphs.Add(tableWork);
            page.Paragraphs.Add(new TextFragment(" "));

            // 2. Таблица сверхурочных
            AddSubTitle(page, "2. Таблица сверхурочных (по данным системы)", headerFont);
            var tableExtra = CreateTable(new[] { "Дата", "Время начала", "Сверхурочные (ч:мм)" });

            var overtimeSessions = _overtimeService.GetUserOvertimes(userId, from, to);

            if (overtimeSessions.Count == 0)
            {
                AddEmptyRow(tableExtra, "Нет данных о переработках", 3);
            }
            else
            {
                for (int i = 0; i < overtimeSessions.Count; i++)
                {
                    var s = overtimeSessions[i];
                    var row = tableExtra.Rows.Add();
                    row.Cells.Add(s.StartTime.ToString("dd.MM.yyyy"));
                    row.Cells.Add(s.StartTime.ToString("HH:mm"));
                    row.Cells.Add(TimeSpan.FromMinutes(s.ExtraTime).ToString(@"hh\:mm"));

                    progress?.Report(20 + (i + 1) * 20 / Math.Max(overtimeSessions.Count, 1));
                }
            }

            page.Paragraphs.Add(tableExtra);
            page.Paragraphs.Add(new TextFragment(" "));

            // 3. Таблица нарушений
            AddSubTitle(page, "3. Таблица нарушений режима", headerFont);
            var tableViol = CreateTable(new[] { "Время нарушения", "Нарушенное правило", "Степень", "Комментарий администратора" });

            var vData = _violationService.GetViolations()
                .AsEnumerable()
                .Where(v => v.Field<DateTime>("Время нарушения").Date >= from.Date && v.Field<DateTime>("Время нарушения").Date <= to.Date)
                .ToList();

            if (vData.Count == 0)
            {
                AddEmptyRow(tableViol, "Нет зафиксированных нарушений", 4);
            }
            else
            {
                for (int i = 0; i < vData.Count; i++)
                {
                    var rowData = vData[i];
                    var row = tableViol.Rows.Add();
                    row.Cells.Add(rowData.Field<DateTime>("Время нарушения").ToString("dd.MM.yyyy HH:mm"));
                    row.Cells.Add(rowData.Field<string>("Расшифровка нарушения") ?? "-");
                    row.Cells.Add(rowData.Field<string>("Степень") ?? "-");
                    row.Cells.Add(rowData.Field<string>("Комментарий администратора") ?? "-");

                    progress?.Report(40 + (i + 1) * 60 / Math.Max(vData.Count, 1));
                }
            }

            page.Paragraphs.Add(tableViol);

            page.Paragraphs.Add(new TextFragment(" "));
            var footer = new TextFragment("Документ сгенерирован автоматически.")
            {
                TextState =
                {
                    Font = bodyFont,
                    FontSize = 8,
                    ForegroundColor = Aspose.Pdf.Color.Gray
                }
            };
            page.Paragraphs.Add(footer);

            doc.Save(filePath);
        }

        private void AddTitle(Page page, string text, Aspose.Pdf.Text.Font font, int size, Aspose.Pdf.Color color)
        {
            var title = new TextFragment(text)
            {
                TextState =
                {
                    Font = font,
                    FontSize = size,
                    ForegroundColor = color
                }
            };
            page.Paragraphs.Add(title);
        }

        private void AddSubTitle(Page page, string text, Aspose.Pdf.Text.Font font)
        {
            var subtitle = new TextFragment(text)
            {
                TextState =
                {
                    Font = font,
                    FontSize = 13,
                    ForegroundColor = Aspose.Pdf.Color.Black
                }
            };
            page.Paragraphs.Add(subtitle);
        }

        private void AddParagraph(Page page, string text, Aspose.Pdf.Text.Font font)
        {
            var p = new TextFragment(text)
            {
                TextState =
                {
                    Font = font,
                    FontSize = 11,
                    ForegroundColor = Aspose.Pdf.Color.Black
                }
            };
            page.Paragraphs.Add(p);
        }

        private Aspose.Pdf.Table CreateTable(string[] headers)
        {
            var table = new Aspose.Pdf.Table
            {
                ColumnWidths = string.Join(" ", headers.Select(_ => "100")),
                Border = new BorderInfo(BorderSide.All, 0.5f),
                DefaultCellBorder = new BorderInfo(BorderSide.All, 0.5f),
                DefaultCellPadding = new MarginInfo(4, 2, 4, 2)
            };

            var headerRow = table.Rows.Add();
            foreach (var text in headers)
            {
                var cell = headerRow.Cells.Add(text);
                cell.BackgroundColor = Aspose.Pdf.Color.DarkGray;
                cell.DefaultCellTextState = new TextState
                {
                    Font = FontRepository.FindFont("Helvetica-Bold"),
                    FontSize = 10,
                    ForegroundColor = Aspose.Pdf.Color.White
                };
            }

            return table;
        }

        private void AddEmptyRow(Aspose.Pdf.Table table, string message, int colspan)
        {
            var row = table.Rows.Add();
            var cell = row.Cells.Add(message);
            cell.ColSpan = colspan;
            cell.Alignment = HorizontalAlignment.Center;
            cell.DefaultCellTextState = new TextState
            {
                Font = FontRepository.FindFont("Helvetica-Oblique"),
                FontSize = 10,
                ForegroundColor = Aspose.Pdf.Color.Gray
            };
        }
    }
}
