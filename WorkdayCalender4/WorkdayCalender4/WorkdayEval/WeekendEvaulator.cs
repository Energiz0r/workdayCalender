using System;

namespace WorkdayCalender4
{
    public class WeekendEvaulator : IWorkdayEvaluater
    {
        public bool IsWorkday(DateTime dt)
        {
            return dt.DayOfWeek != DayOfWeek.Saturday && dt.DayOfWeek != DayOfWeek.Sunday;
        }
    }
}
