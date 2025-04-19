using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Diplom.Services.Calendar
{
    public interface ICalendarRenderer
    {
        void RenderCalendar(
            FlowLayoutPanel panel,
            Label monthLabel,
            int year,
            int month,
            Dictionary<DateTime, TimeSpan> workData,
            DayTileStylingDelegate styleDelegate);
    }
}
