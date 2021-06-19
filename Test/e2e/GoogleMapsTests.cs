using NUnit.Framework;
using SeleniumGoogleMapsExample.PageObject;
using SeleniumGoogleMapsExample.Test.E2E.Config;
using SeleniumGoogleMapsExample.Test.model;

namespace SeleniumGoogleMapsExample.Test.E2E
{
    [TestFixture(BrowserType.Firefox)]
    [TestFixture(BrowserType.Chrome)]
    [Parallelizable(ParallelScope.Fixtures)]
    public class GoogleMapsTests : TestBase
    {
        private GoogleMapsPage onGoogleMapsPage;
        private TripDetailsPage onTripDetailsPage;

        public GoogleMapsTests(BrowserType browserType) : base(browserType)
        {
        }

        [SetUp]
        public void StartBrowser()
        {
            onGoogleMapsPage = new GoogleMapsPage(Driver, Timeout);
            onTripDetailsPage = new TripDetailsPage(Driver, Timeout);
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
