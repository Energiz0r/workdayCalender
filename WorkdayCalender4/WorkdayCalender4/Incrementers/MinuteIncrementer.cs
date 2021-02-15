using System;

namespace WorkdayCalender4
{
    public class MinuteIncrementer : IIncrementer
    {
        private readonly IWorkdayController workdayController;
        private readonly IWorkhourController workhorController;

        public MinuteIncrementer(IWorkdayController workdayController, IWorkhourController workhorController)
        {
            this.workdayController = workdayController;
            this.workhorController = workhorController;
        }

        public DateTime Increment(DateTime cdt, double workdaysToAdd)
        {
            var isIncrementing = workdaysToAdd >= 0;
            var workdayMinutes = workhorController.GetWorkdayMinutes();
            var workMinutsToAdd = workdaysToAdd % 1;
            var minutesToAdd = (int)Math.Abs(workMinutsToAdd * workdayMinutes);

            //Reset to nearest work hour
            while (!workhorController.IsInsideWorkHours(cdt, isIncrementing))
            {
                cdt = cdt.AddMinutes(isIncrementing ? 1 : -1);
            }

            //Tick minutes until all minutes is added and currentDateTime is correct
            while (minutesToAdd > 0)
            {
                cdt = cdt.AddMinutes(isIncrementing ? 1 : -1);
                if (workhorController.IsInsideWorkHours(cdt, isIncrementing) && workdayController.IsWorkDay(cdt))
                {
                    minutesToAdd--;
                }
            }
            return cdt;
        }
    }
}
