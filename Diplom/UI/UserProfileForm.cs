using System;
using System.Drawing;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;
using Diplom.Services.Calendar;

namespace Diplom.UI
{
    public partial class UserProfileForm : Form
    {
        private int currentMonth;
        private int currentYear;
        private int userId;
        private readonly ICalendarRenderer _calendarRenderer;
        private readonly IWorkSessionService _workSessionService;
        private readonly IUserDataService _userDataService;
        private readonly IViolationService _violationService;
        private readonly IPdfReportService _pdfReportService;


        public UserProfileForm(
            IUserDataService userDataService,
            IViolationService violationService,
            IWorkSessionService workSessionService,
            ICalendarRenderer calendarRenderer,
            IPdfReportService pdfReportService,
            int userId)
        {
            InitializeComponent();
            this.SetStyle(ControlStyles.AllPaintingInWmPaint |
              ControlStyles.UserPaint |
              ControlStyles.OptimizedDoubleBuffer, true);
            this.UpdateStyles();
            this.DoubleBuffered = true;
            this.Icon = new Icon(Application.StartupPath + @"\Resources\icon.ico");


            _userDataService = userDataService;
            _violationService = violationService;
            _workSessionService = workSessionService;
            _calendarRenderer = calendarRenderer;
            _pdfReportService = pdfReportService;
            this.userId = userId;

            currentMonth = DateTime.Today.Month;
            currentYear = DateTime.Today.Year;

            LoadUserData();
            LoadViolations();
            UpdateCalendar();
            btnPrevMonth.Click += (s, e) => GoToPreviousMonth();
            btnNextMonth.Click += (s, e) => GoToNextMonth();
        }
        private void LoadUserData()
        {
            lblFullName.Text = $"ФИО: {_userDataService.GetFullName()}";
            lblTotalWorkTime.Text = $"Общее рабочее время: {_userDataService.GetTotalWorkTime()}";
        }
        private void LoadViolations()
        {
            dataGridViolations.DataSource = _violationService.GetViolations();
        }
        private void UpdateCalendar()
        {

            var workData = _workSessionService.GetUserDailyWorkTime(userId, currentMonth, currentYear);

            DayTileStylingDelegate tileStyler = (tile, date, workTime, isWeekend) =>
            {
                if (isWeekend)
                {
                    tile.BackColor = Color.White;
                    tile.ForeColor = Color.Red;
                    tile.FooterText = "Выходной";
                }
                else if (workTime.TotalHours >= 8)
                {
                    tile.BackColor = Color.LightGreen;
                    tile.FooterText = $"{workTime.TotalHours:F1} ч";
                }
                else if (workTime.TotalHours > 0)
                {
                    tile.BackColor = Color.Orange;
                    tile.FooterText = $"{workTime.TotalHours:F1} ч";
                }
                else
                {
                    tile.BackColor = Color.LightCoral;
                    tile.FooterText = "0 ч";
                }
            };

            _calendarRenderer.RenderCalendar(flowCalendar, lblMonthYear, currentYear, currentMonth, workData, tileStyler);
        }

        private void GoToPreviousMonth()
        {
            currentMonth--;
            if (currentMonth < 1)
            {
                currentMonth = 12;
                currentYear--;
            }

            UpdateCalendar();
        }

        private void GoToNextMonth()
        {
            currentMonth++;
            if (currentMonth > 12)
            {
                currentMonth = 1;
                currentYear++;
            }

            UpdateCalendar();
        }

        private async void btnExportPDF_Click(object sender, EventArgs e)
        {
            var settings = SettingsManager.LoadSettings();
            var form = new ExportPeriodForm(settings.DefaultExportPath); 

            if (form.ShowDialog() != DialogResult.OK)
                return;

            DateTime from, to;
            switch (form.SelectedRange)
            {
                case ExportRange.Week:
                    from = DateTime.Today.AddDays(-7);
                    to = DateTime.Today;
                    break;
                case ExportRange.Month:
                    from = DateTime.Today.AddMonths(-1);
                    to = DateTime.Today;
                    break;
                case ExportRange.Year:
                    from = DateTime.Today.AddYears(-1);
                    to = DateTime.Today;
                    break;
                default:
                    from = form.CustomFrom;
                    to = form.CustomTo;
                    break;
            }

            if (string.IsNullOrWhiteSpace(form.ExportPath))
            {
                MessageBox.Show("Пожалуйста, выберите путь для сохранения файла.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var progressForm = new ProgressForm();
            progressForm.Show();

            var progress = new Progress<int>(value => progressForm.SetProgress(value));

            await Task.Run(() =>
            {
                _pdfReportService.ExportFullReport(userId, from, to, form.ExportPath, progress);
            });

            progressForm.Close();
            MessageBox.Show("PDF-отчёт успешно создан!", "Готово", MessageBoxButtons.OK, MessageBoxIcon.Information);
            if (settings.OpenPdfAfterExport && File.Exists(form.ExportPath))
            {
                System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo
                {
                    FileName = form.ExportPath,
                    UseShellExecute = true
                });
            }

        }
    }
}