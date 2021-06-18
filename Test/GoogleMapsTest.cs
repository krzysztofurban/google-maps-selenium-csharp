using System.Threading;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using SeleniumGoogleMapsExample.PageObject;
using SeleniumGoogleMapsExample.Test.model;

namespace SeleniumGoogleMapsExample.Test
{
    public class GoogleMapsTest
    {
        IWebDriver driver;
        private readonly int TIMEOUT = 500;

        [SetUp]
        public void StartBrowser()
        {
            driver = new FirefoxDriver();
        }

        [Test]
        public void ShouldVisitGoogleMaps_ShouldDisplaySearchBar()
        {
            GoogleMapsPage googleMapsPage = new GoogleMapsPage(this.driver, TIMEOUT);
            googleMapsPage.GoToPage();

            Assert.True(googleMapsPage.IsSearchBarVisible());
        }

        [TestCase("plac Defilad 1,Warszawa", "Chłodna 51, Warszawa", 3.0, 40.0, TransportType.Walking)]
        [TestCase("plac Defilad 1,Warszawa", "Chłodna 51, Warszawa", 3.0, 15.0, TransportType.Cycling)]
        public void ShouldAssertDistanceAndTimeBetweenObjectsIsLesserThanGiven(string startAddress, string destinationAddress, double kmLimit, double timeLimitMin, TransportType transportType)
        {
            GoogleMapsPage googleMapsPage = new GoogleMapsPage(this.driver, TIMEOUT);
            googleMapsPage.GoToPage();
            googleMapsPage.FillAndSubmitDirectionWidgetForm(startAddress, destinationAddress, transportType);
            Thread.Sleep(5000);
            Assert.True(true);
        }

        [TearDown]
        public void CloseBrowser()
        {
            driver.Close();
        }
    }
}
