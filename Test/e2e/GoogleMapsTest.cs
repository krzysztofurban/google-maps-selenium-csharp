using System.Threading;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
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
            driver = new ChromeDriver();
        }

        [Test]
        public void ShouldVisitGoogleMaps_ShouldDisplaySearchBar()
        {
            GoogleMapsPage googleMapsPage = new GoogleMapsPage(this.driver, TIMEOUT);
            googleMapsPage.GoToPage();

            Assert.True(googleMapsPage.IsSearchBarVisible());
        }

        [TestCase("plac Defilad 1,Warszawa", "Chłodna 51, Warszawa", 3.0, 40, TransportType.Walking)]
        [TestCase("plac Defilad 1,Warszawa", "Chłodna 51, Warszawa", 3.0, 15, TransportType.Cycling)]
        public void ShouldAssertDistanceAndTimeBetweenObjectsIsLesserThanGiven(string startAddress, string destinationAddress, double kmLimit, int timeLimitMin, TransportType transportType)
        {
            GoogleMapsPage onGoogleMapsPage = new GoogleMapsPage(this.driver, TIMEOUT);
            onGoogleMapsPage.GoToPage()
                .FillAndSubmitDirectionWidgetForm(startAddress, destinationAddress, transportType);

            TripDetailsPage onTripDetailsPage = new TripDetailsPage(driver, TIMEOUT);

            TripParameters tripParameters = onTripDetailsPage
                .ShowFirstTripDetailsSection()
                .GetTripParameters();

            Assert.Less(tripParameters.DistanceInKm, kmLimit);
            Assert.Less(tripParameters.Minutes, timeLimitMin);
        }

        [TearDown]
        public void CloseBrowser()
        {
            driver.Close();
        }
    }
}
