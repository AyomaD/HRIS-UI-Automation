using NLog;
using NUnit.Framework;
using NUnit.Framework.Interfaces;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using System.Reflection;
using UI_Automation.Util;
using TestContext = NUnit.Framework.TestContext;

namespace UI_Automation.Base
{
    [TestFixture]
    public class TestBase
    {
        public static Dictionary<string, string> configData = DataReader.getAutomationConfigData();
        public Dictionary<string, string> dataSet = DataReader.getData(configData["Env"]);
        protected static Logger logger = LogManager.GetCurrentClassLogger();
        public IWebDriver GetDriver;

        [SetUp]
        public void StartTest()
        {
            StartUp();
            NavigateToURL();
        }

        [TearDown]
        public void EndTest()
        {
            try
            {
                if (TestContext.CurrentContext.Result.Outcome.Status == TestStatus.Failed)
                {
                    logger.Info(TestContext.CurrentContext.Test.MethodName + ": Failed");
                    CaptureScreenShots();
                }
                else
                {
                    logger.Info(TestContext.CurrentContext.Test.MethodName + ": Passed");
                }
            }
            catch (Exception ex)
            {
                logger.Info(ex.Message);
            }
            finally
            {
                QuitWebdriver();
                logger.Info(TestContext.CurrentContext.Test.MethodName + ": Completed");
            }

        }
        private void StartUp()
        {
            try
            {
                if (configData["Browser"] == "chrome")
                {
                    GetDriver = new ChromeDriver(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location));
                }
                else if (configData["Browser"] == "firefox")
                {
                    GetDriver = new FirefoxDriver(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location));
                }

                logger.Info("Scuessfully initiate {driver} web driver", configData["Browser"]);
                logger.Info(TestContext.CurrentContext.Test.FullName + ": Started");
            }
            catch (Exception Ex)
            {
                logger.Info("Exception occure while initiating web driver");
                logger.Error(Ex.Message.ToString());
            }
        }

        private void NavigateToURL()
        {
            GetDriver.Manage().Window.Maximize();
            GetDriver.Url = dataSet["URL"];
            logger.Info("Navigated to " + dataSet["URL"]);
        }

        private void QuitWebdriver()
        {
            if (GetDriver != null)
            {
                GetDriver.Quit();
            }
            logger.Info("Scuessfully quit {driver} web driver", configData["Browser"]);
        }

        private void CaptureScreenShots()
        {
            try
            {
                string FileLocation = configData["ScreenShotLocation"] +
                DateTime.Now.ToString("MMMMdd-HHmm") +
                TestContext.CurrentContext.Test.MethodName + ".jpg";
                Screenshot TakeScreenShot = ((ITakesScreenshot)GetDriver).GetScreenshot();
                TakeScreenShot.SaveAsFile(FileLocation);
            }
            catch (Exception ex)
            {
                throw new Exception("Exception occure while capturing a screen shot", ex);
            }
        }
    }
}
