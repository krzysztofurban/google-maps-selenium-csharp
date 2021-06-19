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
        private GoogleMapsPage onGoogleMapsPage;
        private TripDetailsPage onTripDetailsPage;

        [SetUp]
        public void StartBrowser()
        {
            driver = new ChromeDriver();
            onGoogleMapsPage = new GoogleMapsPage(driver, TIMEOUT);
            onTripDetailsPage = new TripDetailsPage(driver, TIMEOUT);
        }

        [Test]
        public void ShouldVisitGoogleMaps_ShouldDisplaySearchBar()
        {
            onGoogleMapsPage.GoToPage();

            Assert.True(onGoogleMapsPage.IsSearchBarVisible());
        }

        [TestCase("plac Defilad 1,Warszawa", "Chłodna 51, Warszawa", 3.0, 40, TransportType.Walking)]
        [TestCase("plac Defilad 1,Warszawa", "Chłodna 51, Warszawa", 3.0, 15, TransportType.Cycling)]
        public void ShouldAssertDistanceAndTimeBetweenObjectsIsLesserThanGiven(string startAddress, string destinationAddress, double kmLimit, int timeLimitMin, TransportType transportType)
        {
            onGoogleMapsPage.GoToPage()
                .FillAndSubmitDirectionWidgetForm(startAddress, destinationAddress, transportType);

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
