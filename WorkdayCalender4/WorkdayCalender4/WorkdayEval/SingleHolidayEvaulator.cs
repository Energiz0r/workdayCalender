using System;

namespace WorkdayCalender4
{
    public class SingleHolidayEvaulator : IWorkdayEvaluater
    {
        private string _holiday;

        public SingleHolidayEvaulator(DateTime dt)
        {
            _holiday = dt.ToString("yyyy-MM-dd");
        }
        public bool IsWorkday(DateTime dt)
        {
            return _holiday != dt.ToString("yyyy-MM-dd");
        }
    }
}
