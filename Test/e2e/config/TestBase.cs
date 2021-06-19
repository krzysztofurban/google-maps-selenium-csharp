using System;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;

namespace SeleniumGoogleMapsExample.Test.e2e.config
{
    [TestFixture]
    public class TestBase
    {
        protected IWebDriver driver; 
        protected int TIMEOUT = 500;
        protected BrowserType browserType;


        public TestBase(BrowserType browserType)
        {
            this.browserType = browserType;
        }

        [SetUp]
        public void SetUp()
        {
            switch (browserType)
            {
                case BrowserType.Chrome:
                    driver = new ChromeDriver();
                    break;
                case BrowserType.Firefox:
                    driver = new FirefoxDriver();
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        [TearDown]
        public void CloseBrowser()
        {
            driver.Close();
        }
    }
}
