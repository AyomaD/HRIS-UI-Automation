using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;
using NLog;
using NUnit.Framework;
using UI_Automation.Util;

namespace UI_Automation.Base
{
    [SetUpFixture]
    public class Base
    {
        protected static Dictionary<string, string> configData = DataReader.getAutomationConfigData();
        protected Dictionary<string, string> dataSet = DataReader.getData(configData["Env"]);
        protected static Logger logger = LogManager.GetCurrentClassLogger();
        protected ExtentReports Extent;
        protected ExtentTest Test;
        protected string ScreenShotFilePath;

        [OneTimeSetUp]
        protected void Setup()
        {
            var dir = TestContext.CurrentContext.TestDirectory + "\\";
            var fileName = this.GetType().ToString() + ".html";
            var htmlReporter = new ExtentHtmlReporter(dir + fileName);

            Extent = new ExtentReports();
            Extent.AttachReporter(htmlReporter);
            Extent.AddSystemInfo("Environment", configData["Env"]);

            ScreenShotFilePath = TestContext.CurrentContext.TestDirectory + "\\ScreenShots";
            if (!Directory.Exists(ScreenShotFilePath))
            {
                Directory.CreateDirectory(ScreenShotFilePath);
            }
        }

        [OneTimeTearDown]
        protected void TearDown()
        {
            Extent.Flush();
        }
    }
}
