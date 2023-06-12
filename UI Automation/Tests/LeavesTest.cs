using NUnit.Framework;
using UI_Automation.Base;

namespace UI_Automation.Tests
{
    [TestFixture]
    public class LeavesTest : TestActivityBase
    {
        [TestCase(TestName = "VerifyHolyDayCountTest")]
        public void VerifyHolyDayCountTest()
        {
            LeavePage = NavigatedToLeavePage();
            LeavePage.ClickOnConfigureOption();
            LeavePage.ClickOnAddHolyDaysOption();
            Assert.AreEqual(dataSet["Leave_HolyDayCount"], LeavePage.GetHolydayCount(),"Leave count should be equals to"+ dataSet["Leave_HolyDayCount"]);
        }
    }
}
