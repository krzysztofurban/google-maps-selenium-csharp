using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.PageObjects;
using SeleniumGoogleMapsExample.Test.model;

namespace SeleniumGoogleMapsExample.PageObject
{
    public class GoogleMapsPage
    {
        private readonly IWebDriver _driver;
        private readonly string pageUrl = "https://www.google.pl/maps/";
        private readonly WebDriverWait _wait;
        private readonly int _timeout;

        [FindsBy(How = How.Id, Using = "searchboxinput")]
        [CacheLookup]
        private IWebElement _searchBar;

        [FindsBy(How = How.ClassName, Using = "searchbox-directions")]
        [CacheLookup]
        private IWebElement _directionWidgetButton;

        [FindsBy(How = How.CssSelector, Using = "#directions-searchbox-0 input")]
        [CacheLookup]
        private IWebElement _startingPointInput;

        [FindsBy(How = How.CssSelector, Using = "#directions-searchbox-1 input")]
        [CacheLookup]
        private IWebElement _destinationPointInput;

        public GoogleMapsPage(IWebDriver driver, int timeout)
        {
            _driver = driver;
            _wait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeout));
            _timeout = timeout;
            PageFactory.InitElements(driver, this);
        }

        public GoogleMapsPage GoToPage()
        {
            _driver.Navigate().GoToUrl(pageUrl);
            LoadComplete();
            if (_driver.Url.Contains("consent"))
            {
                ConsentPage consentPage = new ConsentPage(_driver, _timeout);
                consentPage.Submit();
            }

            LoadComplete();
            return this;
        }

        public GoogleMapsPage FillAndSubmitDirectionWidgetForm(string startAddress, string destinationAddress,
            TransportType transportType)
        {
            return OpenDirectionWidget()
                .SetTransportType(transportType)
                .FillStartingPointInput(startAddress)
                .FillDestinationPointInput(destinationAddress)
                .SubmitDirectionForm();
        }

        public Boolean IsSearchBarVisible()
        {
            return _searchBar.Displayed;
        }

        private GoogleMapsPage FillDestinationPointInput(string destinationAddress)
        {
            _destinationPointInput.SendKeys(destinationAddress);
            return this;
        }

        private GoogleMapsPage FillStartingPointInput(string startAddress)
        {
            _startingPointInput.SendKeys(startAddress);
            return this;
        }

        private GoogleMapsPage SubmitDirectionForm()
        {
            _destinationPointInput.SendKeys(Keys.Enter);
            return this;
        }

        private GoogleMapsPage SetTransportType(TransportType transportType)
        {
            string expectedXpath = $"//*[@data-tooltip='{TransportTypeConverter.ToPolish(transportType)}']";
            IWebElement transportTypeElement = _wait.Until(
                SeleniumExtras.WaitHelpers.ExpectedConditions.ElementExists(By.XPath(expectedXpath)));
            transportTypeElement.Click();
            LoadComplete();
            return this;
        }

        private GoogleMapsPage OpenDirectionWidget()
        {
            _directionWidgetButton.Click();
            return this;
        }

        private void LoadComplete()
        {
            _wait.Until(d => ((IJavaScriptExecutor)d).ExecuteScript("return document.readyState").Equals("complete"));
        }

    }
}
