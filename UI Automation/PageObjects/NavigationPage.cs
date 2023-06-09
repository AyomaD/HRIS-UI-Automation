using OpenQA.Selenium;
using SeleniumExtras.PageObjects;
using SeleniumExtras.WaitHelpers;
using UI_Automation.Base;

namespace UI_Automation.PageObjects
{
    public class NavigationPage : BasePage
    {
        [FindsBy(How = How.XPath,Using = "//a/div[2]/img")]
        private IWebElement imgLogo;
        public NavigationPage(IWebDriver webDriver) : base(webDriver)
        {
            PageFactory.InitElements(webDriver, this);
        }

        public bool IsLogoIsVisible() 
        {
            Wait.Until(ExpectedConditions.ElementToBeClickable(imgLogo));
            return imgLogo.Displayed;
        }
    }
}
