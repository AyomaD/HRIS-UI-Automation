using Microsoft.VisualStudio.TestPlatform.ObjectModel;
using NUnit.Framework;
using UI_Automation.Base;
using UI_Automation.PageObjects;

namespace UI_Automation.Tests
{
    [TestFixture]
    [Parallelizable(scope: ParallelScope.Self)]
    public class LoginTest : TestActivityBase
    {
         [TestCase (TestName = "VerifyLoginForValidTest")]
        public void VerifyLoginForValidDataTest()
        {
            NavigationPage = Login(); 
            Assert.IsTrue(NavigationPage.IsLogoIsVisible(),"Logo should be displayed once sucessufully logged into system ");
        }

        [Test]
        [TestCase("Admin", "invalidPassword"
            , TestName = "VerifyFailedToLoginForInvalidPasswordTest" )]
        [TestCase("InvaldUser", "admin123"
            ,TestName = "VerifyFailedToLoginForInvalidUserNameTest")]
        
        public void VerifyLoginForInvalidDataTest(String userName, String password)
        {
            LoginPage = new LoginPage(GetDriver);
            LoginPage.EnterUserName(userName);
            LoginPage.EnterPassword(password);
            NavigationPage = LoginPage.ClickOnLoginButton(dataSet["LoggedInURL"]);
            Assert.IsNull(NavigationPage, "User should not be able to logged into system ");
        }
    }
}
