using System;
using System.Collections.Generic;

namespace WorkdayCalender2
{
    public class WorkdayCalender
    {
        private HashSet<string> _holidays;
        private TimeSpan _workHoursStart = new TimeSpan(8, 0, 0);
        private TimeSpan _workhoursEnd = new TimeSpan(16, 0, 0);

        public WorkdayCalender()
        {
            _holidays = new HashSet<string>();
        }

        public void SetWorkdayStartEnd(TimeSpan start, TimeSpan end)
        {
            _workHoursStart = start;
            _workhoursEnd = end;
        }

        public void SetSingleHoliday(DateTime holiday)
        {
            _holidays.Add(holiday.ToString("yyyy-MM-dd"));
        }

        public void SetRecurringHoliday(DateTime recurringHoliday)
        {
            _holidays.Add(recurringHoliday.ToString("MM-dd"));
        }

        public DateTime GetNextWorkday(DateTime cdt, double workdaysToAdd = 0)
        {
            var isIncrementingMinutes = workdaysToAdd >= 0;
            var workdayMinutes = (_workhoursEnd - _workHoursStart).TotalMinutes;
            var minutesToAdd = (int)Math.Abs(workdaysToAdd * workdayMinutes);

            //Reset to nearest work hour
            while (!IsInsideWorkHours(cdt, isIncrementingMinutes))
            {
                cdt = cdt.AddMinutes(isIncrementingMinutes ? 1 : -1);
            }

            //Tick minutes until all minutes is added and currentDateTime is correct
            while (minutesToAdd > 0)
            {
                cdt = cdt.AddMinutes(isIncrementingMinutes ? 1 : -1);
                if (IsInsideWorkHours(cdt, isIncrementingMinutes) && !IsHoliday(cdt) && !IsWeekend(cdt))
                {
                    minutesToAdd--;
                }
            }

            return cdt;
        }

        private bool IsWeekend(DateTime dt)
        {
            return dt.DayOfWeek == DayOfWeek.Saturday || dt.DayOfWeek == DayOfWeek.Sunday;
        }

        private bool IsHoliday(DateTime dt)
        {
            return _holidays.Contains(dt.ToString("yyyy-MM-dd")) || _holidays.Contains(dt.ToString("MM-dd"));
        }

        private bool IsInsideWorkHours(DateTime dt, bool isIncrementingMinutes)
        {
            return isIncrementingMinutes
                ? dt.TimeOfDay >= _workHoursStart && dt.TimeOfDay < _workhoursEnd
                : dt.TimeOfDay > _workHoursStart && dt.TimeOfDay <= _workhoursEnd;
        }
    }
}