using System;

namespace WorkdayCalender4
{
    public class WorkhourController : IWorkhourController
    {
        private TimeSpan start;
        private TimeSpan end;

        public int GetWorkdayMinutes()
        {
            return (int)(end - start).TotalMinutes;
        }

        public bool IsInsideWorkHours(DateTime dt, bool inc)
        {
            return inc ? dt.TimeOfDay >= start && dt.TimeOfDay < end : dt.TimeOfDay > start && dt.TimeOfDay <= end;
        }

        public void SetWorkdayStartEnd(TimeSpan start, TimeSpan end)
        {
            this.start = start;
            this.end = end;
        }
    }
}