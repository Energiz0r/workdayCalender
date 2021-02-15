using System;

namespace WorkdayCalender4
{
    public class RecurringHolidayEvaulator : IWorkdayEvaluater
    {
        private string _holiday;

        public RecurringHolidayEvaulator(DateTime dt)
        {
            _holiday = dt.ToString("MM-dd");
        }
        public bool IsWorkday(DateTime dt)
        {
            return _holiday != dt.ToString("MM-dd");
        }
    }
}
