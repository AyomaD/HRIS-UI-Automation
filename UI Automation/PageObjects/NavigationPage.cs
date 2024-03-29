﻿using OpenQA.Selenium;
using SeleniumExtras.PageObjects;
using SeleniumExtras.WaitHelpers;
using UI_Automation.Base;

namespace UI_Automation.PageObjects
{
    public class NavigationPage : BasePage
    {
        [FindsBy(How = How.XPath,Using = "//a/div[2]/img")]
        private IWebElement imgLogo;
        [FindsBy(How = How.XPath, Using = "//li[3]/a/span[text()='Leave']")]
        private IWebElement leaveListOption;
        public NavigationPage(IWebDriver webDriver) : base(webDriver)
        {
            PageFactory.InitElements(webDriver, this);
        }

        public bool IsLogoIsVisible() 
        {
            Wait.Until(ExpectedConditions.ElementToBeClickable(imgLogo));
            return imgLogo.Displayed;
        }

        public LeavePage ClickOnLeave() 
        {
            Wait.Until(webDriver =>
            {
                return leaveListOption.Displayed && leaveListOption.Enabled;
            });
            leaveListOption.Click();
            return new LeavePage(WebDriver);
        }
    }
}
