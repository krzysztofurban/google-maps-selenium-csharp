using OpenQA.Selenium;
using SeleniumExtras.PageObjects;

namespace SeleniumGoogleMapsExample.PageObject
{
    public class ConsentPage
    {
        private IWebDriver _driver;

        [FindsBy(How = How.CssSelector, Using = "button.VfPpkd-LgbsSe.VfPpkd-LgbsSe-OWXEXe-k8QpJ.VfPpkd-LgbsSe-OWXEXe-dgl2Hf.nCP5yc.AjY5Oe.DuMIQc")]
        [CacheLookup]
        private IWebElement _agreeButton;

        public ConsentPage(IWebDriver driver, int timeout)
        {
            this._driver = driver;
            PageFactory.InitElements(driver, this);
        }

        public ConsentPage Submit()
        {
            _agreeButton.Click();
            return this;
        }
    }
}
