using System;

namespace WorkdayCalender4
{
    public interface IWorkdayController
    {
        bool IsWorkDay(DateTime dt);
        void AddWorkdayEvaluator(IWorkdayEvaluater workdayEvaluater);
    }
}