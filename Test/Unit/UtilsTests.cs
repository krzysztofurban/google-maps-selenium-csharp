using NUnit.Framework;
using SeleniumGoogleMapsExample.PageObject;
using SeleniumGoogleMapsExample.Test.E2E.Config;

namespace SeleniumGoogleMapsExample.Test.Unit
{
    class UtilsTests
    {
        private string _tripSummaryTitle = "26 min (2,1 km)";

        [Test]
        public void FromDetailsSummaryTitle_ValidDistance()
        {
            TripParameters expected = new TripParameters(26, 2.1);
            TripParameters actual = TripParameters.FromDetailsSummaryTitle(_tripSummaryTitle);

            Assert.AreEqual(expected.DistanceInKm, actual.DistanceInKm);
        }

        [Test]
        public void FromDetailsSummaryTitle_ValidTime()
        {
            TripParameters expected = new TripParameters(26, 2.1);
            TripParameters actual = TripParameters.FromDetailsSummaryTitle(_tripSummaryTitle);

            Assert.AreEqual(expected.Minutes, actual.Minutes);
        }

        [Test]
        public void ShouldEscapeIllegalCharactersFromPath()
        {
            string expectedTestCase = "(plac Defilad 1,Warszawa, Chłodna 51, Warszawa', 3.0, 40, Tra";
            string invalidTestCaseName = "(\"plac Defilad 1,Warszawa\", \"Chłodna 51, Warszawa', 3.0, 40, Tra";
            
            string validTestCaseName = SecurePathUtils.SecureWindowsOsPathInvalidChars(invalidTestCaseName);

            Assert.AreEqual(expectedTestCase, validTestCaseName);
        }
    }
}
