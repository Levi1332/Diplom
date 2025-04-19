using System;
using System.Drawing;
using System.Windows.Forms;

namespace Diplom.UI.Components
{
    public class DayTile : Panel
    {
        public DayTile(DateTime date, TimeSpan workTime, bool isWeekend)
        {
            this.Size = new Size(85, 85);
            this.BorderStyle = BorderStyle.FixedSingle;

            Label lblDate = new Label
            {
                Text = date.Day.ToString(),
                AutoSize = false,
                TextAlign = ContentAlignment.TopRight,
                Dock = DockStyle.Top,
                Font = new Font("Segoe UI", 9, FontStyle.Bold),
                Height = 20,
                ForeColor = isWeekend ? Color.Red : Color.Black
            };

            Label lblHours = new Label
            {
                Text = $"{workTime.Hours}ч {workTime.Minutes}м",
                AutoSize = false,
                Dock = DockStyle.Bottom,
                TextAlign = ContentAlignment.MiddleCenter,
                Height = 20
            };

            if (isWeekend)
            {
                this.BackColor = Color.White;
            }
            else if (workTime.TotalHours >= 8)
            {
                this.BackColor = Color.LightGreen;
            }
            else if (workTime.TotalMinutes > 0)
            {
                this.BackColor = Color.LightCoral;
            }
            else
            {
                this.BackColor = Color.Gainsboro;
            }

            this.Controls.Add(lblDate);
            this.Controls.Add(lblHours);
        }
    }
}
