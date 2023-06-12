using NUnit.Framework;
using UI_Automation.PageObjects;

namespace UI_Automation.Base
{
    [TestFixture]
    public class TestActivityBase : TestBase
    {
        protected LoginPage LoginPage;
        protected NavigationPage NavigationPage;
        protected LeavePage LeavePage;

        protected NavigationPage Login()
        {
            try 
            {
                LoginPage = new LoginPage(GetDriver);
                LoginPage.EnterUserName(dataSet["Login_UserName"]);
                LoginPage.EnterPassword(dataSet["Login_PassWord"]);
                return LoginPage.ClickOnLoginButton(dataSet["LoggedInURL"]);
            }
            catch(Exception ex) 
            {
                logger.Info("Failed to login to the system");
                logger.Info(ex.StackTrace);
                return null;
            }
        }

        protected LeavePage NavigatedToLeavePage() 
        {
            try
            {
                NavigationPage = Login();
                return NavigationPage.ClickOnLeave();
            }
            catch (Exception ex)
            {
                logger.Info("Failed to Navigated to Leaves Page");
                logger.Info(ex.StackTrace);
                return null;
            }
        }
    }
}
