using System;
using System.Collections.Generic;

namespace WorkdayCalender4
{
    public class WorkdayController : IWorkdayController
    {
        private List<IWorkdayEvaluater> workwayEvaluaters;

        public WorkdayController()
        {
            workwayEvaluaters = new List<IWorkdayEvaluater>();
        }

        public bool IsWorkDay(DateTime dt)
        {
            return workwayEvaluaters.TrueForAll(wde => wde.IsWorkday(dt));
        }

        public void AddWorkdayEvaluator(IWorkdayEvaluater workdayEvaluater)
        {
            workwayEvaluaters.Add(workdayEvaluater);
        }
    }
}