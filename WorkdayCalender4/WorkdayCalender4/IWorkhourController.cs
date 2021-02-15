using System;

namespace WorkdayCalender4
{
    public interface IWorkhourController
    {
        void SetWorkdayStartEnd(TimeSpan start, TimeSpan end);
        int GetWorkdayMinutes();
        bool IsInsideWorkHours(DateTime dt, bool incrementing);
    }
}