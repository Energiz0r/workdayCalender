using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace WorkdayCalender.Tests
{
    [TestClass]
    public class WorkdayCalenderTests
    {
        private WorkdayCalender wc;

        [TestInitialize]
        public void Startup()
        {
            wc = new WorkdayCalender();
            wc.SetWorkdayStartEnd(new DateTime(2021, 2, 11, 8, 0, 0), new DateTime(2021, 2, 11, 16, 0, 0));
        }


        [TestMethod, TestCategory("1. TDD - Brute")]
        public void GetNextWorkday_should_same_if_it_is_inside_workHours()
        {
            var nextWorkDay = wc.GetNextWorkday(new DateTime(2021, 2, 11, 12, 0, 0));

            Assert.AreEqual(nextWorkDay, new DateTime(2021, 2, 11, 12, 0, 0));
        }

        [TestMethod, TestCategory("1. TDD - Brute")]
        public void GetNextWorkday_should_return_08_plus_one_day()
        {
            var nextWorkDay = wc.GetNextWorkday(new DateTime(2021, 2, 11, 23, 0, 0));

            Assert.AreEqual(nextWorkDay, new DateTime(2021, 2, 12, 8, 0, 0));
        }

        [TestMethod, TestCategory("1. TDD - Brute")]
        public void GetNextWorkday_should_return_08_same_day_if_start_is_before_workhours()
        {
            var nextWorkDay = wc.GetNextWorkday(new DateTime(2021, 2, 11, 05, 0, 0));

            Assert.AreEqual(nextWorkDay, new DateTime(2021, 2, 11, 8, 0, 0));
        }

        [TestMethod, TestCategory("1. TDD - Brute")]
        public void GetNextWorkday_days_plus_half_workday()
        {
            var nextWorkDay = wc.GetNextWorkday(new DateTime(2021, 2, 10, 19, 0, 0), 0.5);

            Assert.AreEqual(new DateTime(2021, 2, 11, 12, 0, 0), nextWorkDay);
        }

        [TestMethod, TestCategory("1. TDD - Brute")]
        public void GetNextWorkday_days_plus_one_and_half_workday()
        {
            var nextWorkDay = wc.GetNextWorkday(new DateTime(2021, 2, 09, 19, 0, 0), 1.5);

            Assert.AreEqual(new DateTime(2021, 2, 11, 12, 0, 0), nextWorkDay);
        }

        [TestMethod, TestCategory("1. TDD - Brute")]
        public void GetNextWorkday_days_minus_half_workday()
        {
            var nextWorkDay = wc.GetNextWorkday(new DateTime(2021, 2, 10, 19, 0, 0), -0.5);

            Assert.AreEqual(new DateTime(2021, 2, 10, 12, 0, 0), nextWorkDay);
        }

        [TestMethod, TestCategory("1. TDD - Brute")]
        public void GetNextWorkday_days_minus_one_and_half_workday()
        {
            var nextWorkDay = wc.GetNextWorkday(new DateTime(2021, 2, 11, 19, 0, 0), -1.5);

            Assert.AreEqual(new DateTime(2021, 2, 10, 12, 0, 0), nextWorkDay);
        }

        [TestMethod, TestCategory("1. TDD - Brute")]
        public void GetNextWorkday_days_plus_one_day_should_put_over_the_weekend()
        {
            var nextWorkDay = wc.GetNextWorkday(new DateTime(2021, 2, 12, 12, 0, 0), 1); //Fredag

            Assert.AreEqual(new DateTime(2021, 2, 15, 12, 0, 0), nextWorkDay);
        }

        [TestMethod, TestCategory("1. TDD - Brute")]
        public void GetNextWorkday_days_minus_three_days_should_put_over_the_weekend()
        {
            var nextWorkDay = wc.GetNextWorkday(new DateTime(2021, 2, 1, 12, 0, 0), -3); //Fredag

            Assert.AreEqual(new DateTime(2021, 1, 27, 12, 0, 0), nextWorkDay);
        }

        //Her begynner problemene
        //[TestMethod]
        //public void GetNextWorkday_days_plus_half_workday_mid_day()
        //{
        //    var nextWorkDay = wc.GetNextWorkday(new DateTime(2021, 2, 10, 12, 0, 0), 0.75m);

        //    Assert.AreEqual(new DateTime(2021, 2, 11, 10, 0, 0), nextWorkDay);
        //}
    }
}
