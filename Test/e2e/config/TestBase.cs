using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;
using NUnit.Framework;
using NUnit.Framework.Interfaces;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using System;
using System.Drawing;
using System.IO;
using System.Threading;

namespace SeleniumGoogleMapsExample.Test.E2E.Config
{
    [TestFixture]
    public class TestBase
    {
        protected IWebDriver Driver;
        protected int Timeout = 500;
        protected BrowserType BrowserType;
        
        private string TC_Name;
        
        //Extent Report
        protected static string path = System.Reflection.Assembly.GetCallingAssembly().CodeBase;
        protected static string actualPath = path.Substring(0, path.LastIndexOf("bin", StringComparison.Ordinal));
        protected static string reportFolderName = $"Reports\\Reports_{DateTime.Now.ToString("yy-MM-ddThh_mm_ss")}";
        protected static string projectPath = new Uri(actualPath).LocalPath;
        protected static string reportRootPath = projectPath + reportFolderName;
        private static ExtentReports _extent = setupExtentReport();
        private static ThreadLocal<ExtentTest> extent_test = new ThreadLocal<ExtentTest>();

        public TestBase(BrowserType browserType)
        {
            BrowserType = browserType;
        }

        private static ExtentReports setupExtentReport()
        {
            ExtentReports _extent = new ExtentReports();
            Directory.CreateDirectory(projectPath + reportFolderName);

            ExtentHtmlReporter htmlReporter = new ExtentHtmlReporter(reportRootPath + "\\Index.html");

            _extent.AttachReporter(htmlReporter);
            _extent.AddSystemInfo("Host Name", "Windows host");
            _extent.AddSystemInfo("Environment", "Test Environment");
            _extent.AddSystemInfo("UserName", "Krzysztof Urban");
            htmlReporter.LoadConfig(projectPath + "report-config.xml");
            return _extent;
        }

        [OneTimeSetUp]
        protected void OneTimeSetup()
        {
            switch (BrowserType)
            {
                case BrowserType.Chrome:
                    Driver = new ChromeDriver();
                    break;
                case BrowserType.Firefox:
                    Driver = new FirefoxDriver();
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            Driver.Manage().Window.Size = new Size(1024, 768);
        }

        [SetUp]
        protected void SetUp()
        {
            string context_name = SecurePathUtils.secureWindowsPath(
                TestContext.CurrentContext.Test.Name + " on " + BrowserType);

            extent_test.Value = _extent.CreateTest(context_name);
            TC_Name = context_name;
        }

        [TearDown]
        protected void TearDown()
        {
            TestStatus exec_status = TestContext.CurrentContext.Result.Outcome.Status;
            string stacktrace = string.IsNullOrEmpty(TestContext.CurrentContext.Result.StackTrace) ? "" : string.Format("{0}", TestContext.CurrentContext.Result.StackTrace);
            Status logstatus = Status.Pass;
            string fileName = "Screenshot_" + Guid.NewGuid() + ".png";

            switch (exec_status)
            {
                case TestStatus.Failed:
                    logstatus = Status.Fail;
                    extent_test.Value.AddScreenCaptureFromPath(Capture(Driver, fileName, reportRootPath));
                    break;
                case TestStatus.Passed:
                    logstatus = Status.Pass;
                    extent_test.Value.AddScreenCaptureFromPath(Capture(Driver, fileName, reportRootPath));
                    break;
                case TestStatus.Inconclusive:
                    logstatus = Status.Warning;
                    break;
                case TestStatus.Skipped:
                    logstatus = Status.Skip;
                    break;
            }
            extent_test.Value.Log(logstatus, "Test: " + TC_Name + " Status:" + logstatus + stacktrace);
        }

        [OneTimeTearDown]
        protected void ExtentClose()
        {
            Driver.Close();
            _extent.Flush();
        }

        public static string Capture(IWebDriver driver, string screenShotName, string reportRootPath)
        {
            ITakesScreenshot ts = (ITakesScreenshot)driver;
            Screenshot screenshot = ts.GetScreenshot();
            string pth = System.Reflection.Assembly.GetCallingAssembly().CodeBase;
            string screenshotDirectory = reportRootPath + "\\Screenshots";
            Directory.CreateDirectory(screenshotDirectory);
            string screenshootPath = screenshotDirectory + "\\" + screenShotName;
            string localpath = new Uri(screenshootPath).LocalPath;
            screenshot.SaveAsFile(localpath, ScreenshotImageFormat.Png);
            return screenshootPath;
        }

        public MediaEntityModelProvider CaptureScreenShot(IWebDriver driver, String screenShotName)
        {
            ITakesScreenshot ts = (ITakesScreenshot)driver;
            string screenshot = ts.GetScreenshot().AsBase64EncodedString;

            return MediaEntityBuilder.CreateScreenCaptureFromBase64String(screenshot, screenShotName).Build();
        }
    }
}
