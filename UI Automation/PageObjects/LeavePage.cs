using OpenQA.Selenium;
using SeleniumExtras.PageObjects;
using SeleniumExtras.WaitHelpers;
using System.Xml.Linq;
using UI_Automation.Base;

namespace UI_Automation.PageObjects
{
    public class LeavePage : BasePage
    {

        [FindsBy(How = How.XPath, Using = "//span[text()='Configure ']")]
        private IWebElement configureOption;
        [FindsBy(How = How.XPath, Using = "//a[text()='Holidays']")]
        private IWebElement addHolyDaysOption;
        [FindsBy(How = How.XPath, Using = "//*[@id=\"app\"]/div[1]/div[2]/div[2]/div/div[2]/div[3]/div/div[2]//div/div[2]/div   ")]
        private IList<IWebElement> holyDayList;
        [FindsBy(How = How.XPath, Using = "//*[@id=\"app\"]/div[1]/div[2]/div[2]/div/div[2]/div[2]/div/span")]
        private IWebElement holyDayCountLbl;
        public LeavePage(IWebDriver webDriver) : base(webDriver)
        {
            PageFactory.InitElements(webDriver, this);
        }

        public void ClickOnConfigureOption() 
        {
            Wait.Until(webDriver => 
            {
                return configureOption.Displayed && configureOption.Enabled;
            });
            configureOption.Click();
        }

        public void ClickOnAddHolyDaysOption()
        {
            Wait.Until(webDriver =>
            {
                return addHolyDaysOption.Displayed && addHolyDaysOption.Enabled;
            });
            addHolyDaysOption.Click();
        }

        public string GetHolydayCount() 
        {
            Wait.Until(webDriver =>
            {
                return holyDayCountLbl.Displayed;
            });
            return holyDayCountLbl.Text.Substring(1, 2);
        }
    }
}
