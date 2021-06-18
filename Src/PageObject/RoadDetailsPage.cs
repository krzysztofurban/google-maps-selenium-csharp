using System;
using System.Text.RegularExpressions;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.PageObjects;
using SeleniumGoogleMapsExample.Test.model;

namespace SeleniumGoogleMapsExample.PageObject
{
    public class RoadDetailsPage
    {
        private readonly IWebDriver _driver;
        private readonly WebDriverWait _wait;
        private readonly int _timeout;

        [FindsBy(How = How.Id, Using = "section-directions-trip-details-msg-0")]
        [CacheLookup]
        private IWebElement _detailsButton;

        [FindsBy(How = How.ClassName, Using = "section-trip-summary-title")]
        [CacheLookup]
        private IWebElement _tripDetailsSummaryTitle;

        public RoadDetailsPage(IWebDriver driver, int timeout)
        {
            this._driver = driver;
            this._wait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeout));
            this._timeout = timeout;
            PageFactory.InitElements(driver, this);
        }

        private TripParameters GetRoadParameters()
        {
            String summaryTitle = _tripDetailsSummaryTitle.Text;
        }

        private RoadDetailsPage GetFastestRoadDetails()
        {
            IWebElement detailButton =
                _wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(_detailsButton));
            detailButton.Click();
        }
    }


    public class TripParameters
    {
        public int Minutes { get; }
        public double DistanceInKm { get; }

        public TripParameters(int minutes, double distanceInKm)
        {
            this.Minutes = minutes;
            this.DistanceInKm = distanceInKm;
        }
        
        //26 min (2,1 km)
        public static TripParameters FromDetailsSummaryTitle(string title)
        {
            
        }

        //26 min (2,1 km)
        int GetDistanceFromTitle(string title)
        {
            Regex rx = new Regex(@"",
                RegexOptions.Compiled | RegexOptions.IgnoreCase);

            // Define a test string.
            string text = "The the quick brown fox  fox jumps over the lazy dog dog.";

            // Find matches.
            MatchCollection matches = rx.Matches(text);
        }
    }
}
