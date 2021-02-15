using System;

namespace WorkdayCalender4
{
    public interface IWorkdayEvaluater
    {
        bool IsWorkday(DateTime dateTime);
    }
}
