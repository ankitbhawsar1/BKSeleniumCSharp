using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.PageObjects;

namespace SeleniumCSharpFramework.Pages
{
	public class ProductPage
	{
        private IWebDriver driver;
        public ProductPage(IWebDriver driver)
        {
            this.driver = driver;
            PageFactory.InitElements(driver, this);
        }

        //username
        [FindsBy(How = How.TagName, Using = "app-card")]
        private IList<IWebElement> cards;
        public By checkout = By.XPath("//a[contains(text(),'Checkout')]");

        public By cardTitle = By.CssSelector(".card-title a");

        public By cardFooterBtn = By.CssSelector(".card-footer button");

        public IList<IWebElement> getProducts()
        {
            return cards;
        }

        public void getCheckout()
        {
            driver.FindElement(checkout).Click();
        }
        //public void waitForVisibility()
        //{
        //    WebDriverWait wait2 = new(driver, TimeSpan.FromSeconds(6));
        //    //wait2.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.PartialLinkText("Checkout")));
        //    wait2.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(checkout));
        //}
    }
}

