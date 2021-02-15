using System;

namespace WorkdayCalender4
{
    public class DayIncrementer : IIncrementer
    {
        private readonly IWorkdayController workdayController;

        public DayIncrementer(IWorkdayController workdayController)
        {
            this.workdayController = workdayController;
        }

        public DateTime Increment(DateTime cdt, double workdaysToAdd)
        {
            var isIncrementing = workdaysToAdd >= 0;
            workdaysToAdd = (int)Math.Abs(workdaysToAdd);
            while (workdaysToAdd > 0)
            {
                cdt = cdt.AddDays(isIncrementing ? 1 : -1);
                if (workdayController.IsWorkDay(cdt))
                {
                    workdaysToAdd--;
                }
            }

            return cdt;
        }
    }
}
