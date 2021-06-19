using NUnit.Framework;
using SeleniumGoogleMapsExample.PageObject;
using SeleniumGoogleMapsExample.Test.e2e.config;
using SeleniumGoogleMapsExample.Test.model;

namespace SeleniumGoogleMapsExample.Test.e2e
{
    [TestFixture(BrowserType.Firefox)]
    [TestFixture(BrowserType.Chrome)]
    [Parallelizable(ParallelScope.Fixtures)]
    public class GoogleMapsTest : TestBase
    {
        private GoogleMapsPage onGoogleMapsPage;
        private TripDetailsPage onTripDetailsPage;

        public GoogleMapsTest(BrowserType browserType) : base(browserType)
        {
        }

        [SetUp]
        public void StartBrowser()
        {
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
    }
}
