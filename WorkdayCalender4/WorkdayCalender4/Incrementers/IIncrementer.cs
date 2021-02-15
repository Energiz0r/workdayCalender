using System;

namespace WorkdayCalender4
{
    public interface IIncrementer
    {
        public DateTime Increment(DateTime cdt, double timeToAdd);
    }
}
