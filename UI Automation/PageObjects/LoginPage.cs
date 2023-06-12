using OpenQA.Selenium;
using SeleniumExtras.PageObjects;
using SeleniumExtras.WaitHelpers;
using UI_Automation.Base;

namespace UI_Automation.PageObjects
{
    public class LoginPage : BasePage
    {
        [FindsBy(How = How.Name, Using = "username")]
        private IWebElement txtUserName;
        [FindsBy(How = How.Name, Using = "password")]
        private IWebElement txtPassword;
        [FindsBy(How = How.XPath, Using = "//button[text()=' Login ']")]
        private IWebElement btnLogin;

        public LoginPage(IWebDriver webDriver) : base(webDriver)
        {
            PageFactory.InitElements(webDriver, this);
        }

        public void EnterUserName(string userName) 
        {
            Wait.Until(ExpectedConditions.ElementToBeClickable(txtUserName));
            txtUserName.Clear();
            txtUserName.SendKeys(userName);
            logger.Info("Enter User Name " +userName);
        }

        public void EnterPassword(string password)
        {
            txtPassword.SendKeys(password);
            logger.Info("Enter User Name " + password);
        }

        public NavigationPage ClickOnLoginButton(string url)
        {
            try
            {
                btnLogin.Click();
                Wait.Until(ExpectedConditions.UrlToBe(url));
                return new NavigationPage(WebDriver);
            }
            catch (WebDriverTimeoutException ex)
            {
                return null;
            }            
            
        }


    }
}
