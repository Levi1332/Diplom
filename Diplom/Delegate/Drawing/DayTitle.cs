using System;
using System.Drawing;
using System.Windows.Forms;

namespace Diplom
{
    public class DayTile : UserControl
    {
        private Label lblDate;
        private Label lblFooter;

        public DateTime Date { get; private set; }
        public TimeSpan WorkTime { get; private set; }
        public bool IsWeekend { get; private set; }

        private string _footerText;
        public string FooterText
        {
            get => _footerText;
            set
            {
                _footerText = value;
                lblFooter.Text = value;
            }
        }

        public DayTile(DateTime date, TimeSpan workTime, bool isWeekend)
        {
            Date = date;
            WorkTime = workTime;
            IsWeekend = isWeekend;

            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.Size = new Size(85, 85);
            this.BorderStyle = BorderStyle.FixedSingle;

            lblDate = new Label
            {
                Text = Date.Day.ToString(),
                Dock = DockStyle.Top,
                TextAlign = ContentAlignment.MiddleCenter,
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                Height = 25
            };

            lblFooter = new Label
            {
                Text = $"{WorkTime.TotalHours:F1} ч",
                Dock = DockStyle.Bottom,
                TextAlign = ContentAlignment.MiddleCenter,
                Font = new Font("Segoe UI", 8),
                Height = 20
            };

            this.Controls.Add(lblFooter);
            this.Controls.Add(lblDate);
        }
    }
}
