using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using Diplom.UI.Components;

namespace Diplom.Services.Calendar
{
    public class CalendarRenderer : ICalendarRenderer
    {
        public void RenderCalendar(
            FlowLayoutPanel panel,
            Label monthLabel,
            int year,
            int month,
            Dictionary<DateTime, TimeSpan> workData,
            DayTileStylingDelegate styleDelegate)
        {

            panel.SuspendLayout();
            panel.Controls.Clear();

            monthLabel.Text = new DateTime(year, month, 1).ToString("MMMM yyyy");

            int daysInMonth = DateTime.DaysInMonth(year, month);
            DateTime firstDay = new DateTime(year, month, 1);

            int dayOffset = ((int)firstDay.DayOfWeek + 6) % 7;

            for (int i = 0; i < dayOffset; i++)
                panel.Controls.Add(CreateEmptyTile());

            for (int day = 1; day <= daysInMonth; day++)
            {
                DateTime date = new DateTime(year, month, day);
                bool isWeekend = date.DayOfWeek == DayOfWeek.Saturday || date.DayOfWeek == DayOfWeek.Sunday;
                TimeSpan workedTime = workData.ContainsKey(date) ? workData[date] : TimeSpan.Zero;

                var tile = new DayTile(date, workedTime, isWeekend)
                {
                    Margin = new Padding(3)
                };

                styleDelegate?.Invoke(tile, date, workedTime, isWeekend);
                panel.Controls.Add(tile);
            }

            panel.ResumeLayout(); 
        }

        private Control CreateEmptyTile()
        {
            return new Panel
            {
                Size = new Size(85, 85),
                Margin = new Padding(3),
                BackColor = Color.Transparent 
            };
        }
    }
}
