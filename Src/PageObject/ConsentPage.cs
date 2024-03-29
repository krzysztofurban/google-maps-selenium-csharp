﻿using OpenQA.Selenium;
using SeleniumExtras.PageObjects;

namespace SeleniumGoogleMapsExample.PageObject
{
    public class ConsentPage
    {
        private IWebDriver _driver;

        [FindsBy(How = How.CssSelector, Using = "button")]
        [CacheLookup]
        private IWebElement _agreeButton;

        public ConsentPage(IWebDriver driver)
        {
            _driver = driver;
            PageFactory.InitElements(_driver, this);
        }

        public ConsentPage Submit()
        {
            _agreeButton.Click();
            return this;
        }
    }
}
