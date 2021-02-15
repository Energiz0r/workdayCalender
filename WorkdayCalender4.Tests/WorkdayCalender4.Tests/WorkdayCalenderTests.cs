using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace WorkdayCalender4.Tests
{
    [TestClass]
    public class WorkdayCalenderTests
    {
        private WorkdayCalender wc;

        [TestInitialize]
        public void Startup()
        {
            var workdayController = new WorkdayController();
            var workHourController = new WorkhourController();
            wc = new WorkdayCalender(new MinuteIncrementer(workdayController, workHourController), new DayIncrementer(workdayController), workdayController, workHourController);
            wc.SetWorkdayStartEnd(new TimeSpan(8, 0, 0), new TimeSpan(16, 0, 0));
        }

        [TestMethod, TestCategory("4. TDD - SOLIDed")]
        public void GetNextWorkday_should_same_if_it_is_inside_workHours()
        {
            var nextWorkDay = wc.GetNextWorkday(new DateTime(2021, 2, 11, 12, 0, 0));

            Assert.AreEqual(new DateTime(2021, 2, 11, 12, 0, 0), nextWorkDay);
        }

        [TestMethod, TestCategory("4. TDD - SOLIDed")]
        public void GetNextWorkday_should_return_08_plus_one_day()
        {
            var nextWorkDay = wc.GetNextWorkday(new DateTime(2021, 2, 11, 23, 0, 0));

            Assert.AreEqual(new DateTime(2021, 2, 12, 8, 0, 0), nextWorkDay);
        }

        [TestMethod, TestCategory("4. TDD - SOLIDed")]
        public void GetNextWorkday_should_return_08_same_day_if_start_is_before_workhours()
        {
            var nextWorkDay = wc.GetNextWorkday(new DateTime(2021, 2, 11, 05, 0, 0));

            Assert.AreEqual(new DateTime(2021, 2, 11, 8, 0, 0), nextWorkDay);
        }

        [TestMethod, TestCategory("4. TDD - SOLIDed")]
        public void GetNextWorkday_days_plus_one_if_no_holidays_is_present()
        {
            var nextWorkDay = wc.GetNextWorkday(new DateTime(2021, 2, 11, 19, 0, 0), 1);

            Assert.AreEqual(new DateTime(2021, 2, 15, 8, 0, 0), nextWorkDay);
        }

        [TestMethod, TestCategory("4. TDD - SOLIDed")]
        public void GetNextWorkday_days_minus_one_if_no_holidays_is_present()
        {
            var nextWorkDay = wc.GetNextWorkday(new DateTime(2021, 2, 11, 19, 0, 0), -1);

            Assert.AreEqual(new DateTime(2021, 2, 10, 16, 0, 0), nextWorkDay);
        }

        [TestMethod, TestCategory("4. TDD - SOLIDed")]
        public void GetNextWorkday_days_plus_half_workday()
        {
            var nextWorkDay = wc.GetNextWorkday(new DateTime(2021, 2, 10, 19, 0, 0), 0.5);

            Assert.AreEqual(new DateTime(2021, 2, 11, 12, 0, 0), nextWorkDay);
        }

        [TestMethod, TestCategory("4. TDD - SOLIDed")]
        public void GetNextWorkday_days_plus_one_and_half_workday()
        {
            var nextWorkDay = wc.GetNextWorkday(new DateTime(2021, 2, 09, 19, 0, 0), 1.5);

            Assert.AreEqual(new DateTime(2021, 2, 11, 12, 0, 0), nextWorkDay);
        }

        [TestMethod, TestCategory("4. TDD - SOLIDed")]
        public void GetNextWorkday_days_minus_half_workday()
        {
            var nextWorkDay = wc.GetNextWorkday(new DateTime(2021, 2, 10, 19, 0, 0), -0.5);

            Assert.AreEqual(new DateTime(2021, 2, 10, 12, 0, 0), nextWorkDay);
        }

        [TestMethod, TestCategory("4. TDD - SOLIDed")]
        public void GetNextWorkday_days_minus_one_and_half_workday()
        {
            var nextWorkDay = wc.GetNextWorkday(new DateTime(2021, 2, 11, 19, 0, 0), -1.5);

            Assert.AreEqual(new DateTime(2021, 2, 10, 12, 0, 0), nextWorkDay);
        }

        [TestMethod, TestCategory("4. TDD - SOLIDed")]
        public void GetNextWorkday_days_plus_one_day_should_put_over_the_weekend()
        {
            var nextWorkDay = wc.GetNextWorkday(new DateTime(2021, 2, 12, 12, 0, 0), 1); //Fredag

            Assert.AreEqual(new DateTime(2021, 2, 15, 12, 0, 0), nextWorkDay);
        }

        [TestMethod, TestCategory("4. TDD - SOLIDed")]
        public void GetNextWorkday_days_minus_three_days_should_put_over_the_weekend()
        {
            var nextWorkDay = wc.GetNextWorkday(new DateTime(2021, 2, 1, 12, 0, 0), -3); //Monday

            Assert.AreEqual(new DateTime(2021, 1, 27, 12, 0, 0), nextWorkDay);
        }

        [TestMethod, TestCategory("4. TDD - SOLIDed")]
        public void GetNextWorkday_holiday_days_plus_one_day()
        {
            wc.SetSingleHoliday(new DateTime(2021, 2, 11));

            var nextWorkDay = wc.GetNextWorkday(new DateTime(2021, 2, 10, 12, 0, 0), 1);

            Assert.AreEqual(new DateTime(2021, 2, 12, 12, 0, 0), nextWorkDay);
        }

        [TestMethod, TestCategory("4. TDD - SOLIDed")]
        public void GetNextWorkday_holiday_days_plus_one_day_takes_year_into_account()
        {
            wc.SetSingleHoliday(new DateTime(2022, 2, 11));

            var nextWorkDay = wc.GetNextWorkday(new DateTime(2021, 2, 10, 12, 0, 0), 1);

            Assert.AreEqual(new DateTime(2021, 2, 11, 12, 0, 0), nextWorkDay);
        }

        [TestMethod, TestCategory("4. TDD - SOLIDed")]
        public void GetNextWorkday_recurring_holiday_days_plus_one_day()
        {
            wc.SetRecurringHoliday(new DateTime(2021, 2, 11));

            var nextWorkDay = wc.GetNextWorkday(new DateTime(2021, 2, 10, 12, 0, 0), 1);

            Assert.AreEqual(new DateTime(2021, 2, 12, 12, 0, 0), nextWorkDay);
        }

        [TestMethod, TestCategory("4. TDD - SOLIDed")]
        public void GetNextWorkday_recurring_holiday_days_plus_one_day_disreagard_year()
        {
            wc.SetRecurringHoliday(new DateTime(1983, 2, 11));

            var nextWorkDay = wc.GetNextWorkday(new DateTime(2021, 2, 10, 12, 0, 0), 1);

            Assert.AreEqual(new DateTime(2021, 2, 12, 12, 0, 0), nextWorkDay);
        }

        /* ACCEPTANCE TESTS*/

        [TestMethod, TestCategory("4. Acceptance - SOLIDed")]
        public void Adding_100years()
        {
            var nextWorkDay = wc.GetNextWorkday(new DateTime(2021, 2, 10, 12, 0, 0), 36500);

            Assert.AreEqual(new DateTime(2161, 1, 7, 12, 0, 0), nextWorkDay);
        }

        [TestMethod, TestCategory("4. Acceptance - SOLIDed")]
        public void CaseOne()
        {
            wc.SetRecurringHoliday(new DateTime(1983, 5, 17));
            wc.SetSingleHoliday(new DateTime(2004, 5, 27));

            var nextWorkDay = wc.GetNextWorkday(new DateTime(2004, 5, 24, 18, 5, 0), -5.5);

            Assert.AreEqual(new DateTime(2004, 5, 14, 12, 0, 0), nextWorkDay);
        }

        [TestMethod, TestCategory("4. Acceptance - SOLIDed")]
        public void CaseTwo()
        {
            wc.SetRecurringHoliday(new DateTime(1983, 5, 17));
            wc.SetSingleHoliday(new DateTime(2004, 5, 27));

            var nextWorkDay = wc.GetNextWorkday(new DateTime(2004, 5, 24, 19, 5, 0), 44.723656);

            Assert.AreEqual(new DateTime(2004, 7, 27, 13, 47, 0), nextWorkDay);
        }

        [TestMethod, TestCategory("4. Acceptance - SOLIDed")]
        public void CaseThree()
        {
            wc.SetRecurringHoliday(new DateTime(1983, 5, 17));
            wc.SetSingleHoliday(new DateTime(2004, 5, 27));

            var nextWorkDay = wc.GetNextWorkday(new DateTime(2004, 5, 24, 18, 3, 0), -6.7470217);

            Assert.AreEqual(new DateTime(2004, 5, 13, 10, 2, 0), nextWorkDay);
        }

        [TestMethod, TestCategory("4. Acceptance - SOLIDed")]
        public void CaseFour()
        {
            wc.SetRecurringHoliday(new DateTime(1983, 5, 17));
            wc.SetSingleHoliday(new DateTime(2004, 5, 27));

            var nextWorkDay = wc.GetNextWorkday(new DateTime(2004, 5, 24, 8, 3, 0), 12.782709);

            Assert.AreEqual(new DateTime(2004, 6, 10, 14, 18, 0), nextWorkDay);
        }

        [TestMethod, TestCategory("4. Acceptance - SOLIDed")]
        public void CaseFive()
        {
            wc.SetRecurringHoliday(new DateTime(1983, 5, 17));
            wc.SetSingleHoliday(new DateTime(2004, 5, 27));

            var nextWorkDay = wc.GetNextWorkday(new DateTime(2004, 5, 24, 7, 3, 0), 8.276628);

            Assert.AreEqual(new DateTime(2004, 6, 4, 10, 12, 0), nextWorkDay);
        }
    }
}
