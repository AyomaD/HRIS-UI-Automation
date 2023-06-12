using NLog;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace UI_Automation.Base
{
    public class BasePage
    {
        protected IWebDriver WebDriver;
        protected WebDriverWait Wait;
        protected WebDriverWait FluentWait;
        protected static Logger logger = LogManager.GetCurrentClassLogger();

        public BasePage(IWebDriver webDriver)
        {
            this.WebDriver = webDriver;
            this.Wait = new WebDriverWait(webDriver, TimeSpan.FromMilliseconds(10000));
            this.FluentWait = new WebDriverWait(webDriver, TimeSpan.FromMilliseconds(5000));
            FluentWait.PollingInterval = TimeSpan.FromMilliseconds(50);
        }

        protected void ClickByJavaScript(IWebElement element)
        {
            ((IJavaScriptExecutor)WebDriver).ExecuteScript("arguments[0].click();", element);
        }

        protected void ScrollIntoView(IWebElement element)
        {
            ((IJavaScriptExecutor)WebDriver).ExecuteScript("arguments[0].scrollIntoView(true);", element);
        }

    }
}
