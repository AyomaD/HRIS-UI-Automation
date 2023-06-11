using AventStack.ExtentReports;
using NUnit.Framework;
using NUnit.Framework.Interfaces;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using System.Reflection;
using TestContext = NUnit.Framework.TestContext;

namespace UI_Automation.Base
{
    [TestFixture]
    public class TestBase : Base
    {
        protected IWebDriver GetDriver;

        [SetUp]
        public void StartTest()
        {
            Test = Extent.CreateTest(TestContext.CurrentContext.Test.Name);
            StartUp();
            NavigateToURL();
        }

        [TearDown]
        public void EndTest()
        {
            var status = TestContext.CurrentContext.Result.Outcome.Status;
            var stacktrace = string.IsNullOrEmpty(TestContext.CurrentContext.Result.Message)
            ? ""
            : string.Format("{0}", TestContext.CurrentContext.Result.StackTrace);
            Status logstatus;

            switch (status)
            {
                case TestStatus.Failed:
                    logstatus = Status.Fail;
                    break;
                case TestStatus.Inconclusive:
                    logstatus = Status.Warning;
                    break;
                case TestStatus.Skipped:
                    logstatus = Status.Skip;
                    break;
                default:
                    logstatus = Status.Pass;
                    break;
            }

            Test.Log(logstatus, "Test ended with " + logstatus + stacktrace);
            try
            {
                if (TestContext.CurrentContext.Result.Outcome.Status == TestStatus.Failed)
                {
                    logger.Info(TestContext.CurrentContext.Test.MethodName + ": Failed");
                    Test.Log(logstatus, Test.AddScreenCaptureFromPath(CaptureScreenShots())
                        + " test failed");
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
                Extent.Flush();
                QuitWebdriver();
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

        private string CaptureScreenShots()
        {
            try
            {
                string FileLocation = ScreenShotFilePath 
                                        + "\\" 
                                        + DateTime.Now.ToString("MMMMdd-HHmm")
                                        + TestContext.CurrentContext.Test.MethodName + ".jpg";
                Screenshot TakeScreenShot = ((ITakesScreenshot)GetDriver).GetScreenshot();
                TakeScreenShot.SaveAsFile(FileLocation);
                return FileLocation;
            }
            catch (Exception ex)
            {
                throw new Exception("Exception occure while capturing a screen shot", ex);
            }
        }
    }
}
