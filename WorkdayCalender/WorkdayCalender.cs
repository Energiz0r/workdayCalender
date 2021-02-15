using System;
using System.Collections.Generic;

namespace WorkdayCalender
{
    public class WorkdayCalender
    {
        private int _workdayMinutes;
        private DateTime _workHoursStart;
        private DateTime _WorkhoursEnd;
        private List<DateTime> _holidays;

        public void SetWorkdayStartEnd(DateTime start, DateTime end)
        {
            _workdayMinutes = (int)(end - start).TotalMinutes;
            _workHoursStart = start;
            _WorkhoursEnd = end;
        }

        public void SetSingleHoliday(DateTime holiday)
        {
            _holidays.Add(holiday);
        }

        public DateTime GetNextWorkday(DateTime dt, double workdaysToAdd = 0)
        {
            DateTime _nextWorkDay;
            bool inc = workdaysToAdd >= 0;

            //Workhours rule/decision
            if (dt.Hour < _workHoursStart.Hour)
            {
                _nextWorkDay = new DateTime(dt.Year, dt.Month, inc ? dt.Day : dt.Day + 1, inc ? _workHoursStart.Hour : _WorkhoursEnd.Hour, _workHoursStart.Minute, 0);
            }
            else if (dt.Hour > _WorkhoursEnd.Hour)
            {
                _nextWorkDay = new DateTime(dt.Year, dt.Month, inc ? dt.Day + 1 : dt.Day, inc ? _workHoursStart.Hour : _WorkhoursEnd.Hour, _workHoursStart.Minute, 0);
            }
            else
            {
                _nextWorkDay = dt;
            }

            var minutesToAdd = (workdaysToAdd % 1) * _workdayMinutes;
            _nextWorkDay = _nextWorkDay.AddMinutes((int)minutesToAdd);


            //Brute workDay rule
            while (workdaysToAdd >= 1)
            {
                _nextWorkDay = _nextWorkDay.AddDays(1);
                while (_nextWorkDay.DayOfWeek == DayOfWeek.Saturday || _nextWorkDay.DayOfWeek == DayOfWeek.Sunday)
                {
                    _nextWorkDay = _nextWorkDay.AddDays(1);
                }

                //Her begynner problemet.
                //while (_holidays.Any(h => h.Date == _nextWorkDay.Date))
                //{
                //    _nextWorkDay = _nextWorkDay.AddDays(1);
                //}

                workdaysToAdd = workdaysToAdd - 1;
            }

            //Negativ workdaysToAdd
            while (workdaysToAdd <= -1)
            {
                _nextWorkDay = _nextWorkDay.AddDays(-1);
                while (_nextWorkDay.DayOfWeek == DayOfWeek.Saturday || _nextWorkDay.DayOfWeek == DayOfWeek.Sunday)
                {
                    _nextWorkDay = _nextWorkDay.AddDays(-1);

                }
                workdaysToAdd = workdaysToAdd + 1;
            }

            return _nextWorkDay;
        }
    }
}

