using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using SeleniumGoogleMapsExample.PageObject;

namespace SeleniumGoogleMapsExample.Test
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

        public void FromDetailsSummaryTitle_ValidTime()
        {
            TripParameters expected = new TripParameters(26, 2.1);
            TripParameters actual = TripParameters.FromDetailsSummaryTitle(_tripSummaryTitle);

            Assert.AreEqual(expected.Minutes, actual.Minutes);
        }
    }
}
