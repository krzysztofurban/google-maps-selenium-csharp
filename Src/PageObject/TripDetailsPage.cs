using System;
using System.Text.RegularExpressions;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.PageObjects;
using SeleniumGoogleMapsExample.Test.model;

namespace SeleniumGoogleMapsExample.PageObject
{
    public class TripDetailsPage
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

        public TripDetailsPage(IWebDriver driver, int timeout)
        {
            this._driver = driver;
            this._wait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeout));
            this._timeout = timeout;
            PageFactory.InitElements(driver, this);
        }

        public TripParameters GetTripParameters()
        {
            IWebElement summaryTitle =
                _wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(_tripDetailsSummaryTitle));
            return TripParameters.FromDetailsSummaryTitle(summaryTitle.Text);
        }

        public TripDetailsPage ShowFirstTripDetailsSection()
        {
            IWebElement detailButton =
                _wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(_detailsButton));
            detailButton.Click();
            return this;
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
        
        
        public static TripParameters FromDetailsSummaryTitle(string title)
        {
            int minutes = TripParametersUtils.GetMinutesFromTitle(title);
            double distance = TripParametersUtils.GetDistanceFromTitle(title);
            return new TripParameters(minutes, distance);
        }
    }

    //Naive implementation, works only for "26 min (2,1 km)" #noTime
    public class TripParametersUtils
    {
        internal static double GetDistanceFromTitle(string title)
        {
            Regex rx = new Regex(@"[0-9]{1,}", RegexOptions.Compiled);
            MatchCollection matches = rx.Matches(title);
            
            string km = matches[1].Value;
            string meters = matches[2].Value;
            string distanceString = $"{km},{meters}";

            Console.WriteLine(distanceString);
            return Double.Parse(distanceString);
        }

        internal static int GetMinutesFromTitle(string title)
        {
            Regex rx = new Regex(@"^[0-9]{1,}", RegexOptions.Compiled );
            MatchCollection matches = rx.Matches(title);
            string value = matches[0].Value;
            
            Console.WriteLine(value);
            return Int32.Parse(value);
        }
    }
}
