using System;

namespace WorkdayCalender4
{
    public class WorkdayCalender
    {
        private IIncrementer _minuteIncrementer;
        private IIncrementer _dayIncrementer;
        private IWorkdayController _workdayController;
        private IWorkhourController _workhourController;

        public WorkdayCalender(
            IIncrementer _minuteIncrementer,
            IIncrementer _dayIncrementer, 
            IWorkdayController _workdayController,
            IWorkhourController _workhourController)
        {
            this._minuteIncrementer = _minuteIncrementer;
            this._dayIncrementer = _dayIncrementer;
            this._workdayController = _workdayController;
            this._workhourController = _workhourController;
            _workdayController.AddWorkdayEvaluator(new WeekendEvaulator());
        }

        public void SetSingleHoliday(DateTime dateTime)
        {
            _workdayController.AddWorkdayEvaluator(new SingleHolidayEvaulator(dateTime));
        }

        public void SetRecurringHoliday(DateTime dateTime)
        {
            _workdayController.AddWorkdayEvaluator(new RecurringHolidayEvaulator(dateTime));
        }

        public void SetWorkdayStartEnd(TimeSpan start, TimeSpan end)
        {
            _workhourController.SetWorkdayStartEnd(start, end);
        }

        public DateTime GetNextWorkday(DateTime cdt, double workdaysToAdd = 0)
        {
            cdt = _minuteIncrementer.Increment(cdt, workdaysToAdd);
            return _dayIncrementer.Increment(cdt, workdaysToAdd);
        }
    }
}
