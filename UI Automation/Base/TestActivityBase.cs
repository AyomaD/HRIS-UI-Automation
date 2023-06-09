using UI_Automation.PageObjects;

namespace UI_Automation.Base
{
    public class TestActivityBase : TestBase
    {
        protected LoginPage LoginPage;
        protected NavigationPage NavigationPage;

        protected NavigationPage Login()
        {
            try 
            {
                LoginPage = new LoginPage(GetDriver);
                LoginPage.EnterUserName(dataSet["UserName"]);
                LoginPage.EnterPassword(dataSet["PassWord"]);
                return LoginPage.ClickOnLoginButton(dataSet["LoggedInURL"]);
            }
            catch(Exception ex) 
            {
                logger.Info("Failed to login to the system");
                logger.Info(ex.StackTrace);
                return null;
            }
        }
    }
}
