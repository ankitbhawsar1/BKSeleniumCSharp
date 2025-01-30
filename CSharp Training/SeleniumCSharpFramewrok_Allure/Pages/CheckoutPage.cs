using System;
using OpenQA.Selenium;
using SeleniumExtras.PageObjects;

namespace SeleniumCSharpFramework.Pages
{
	public class CheckoutPage
	{
        private IWebDriver driver;
        public CheckoutPage(IWebDriver driver)
        {
            this.driver = driver;
            PageFactory.InitElements(driver, this);
        }

     
        [FindsBy(How = How.CssSelector, Using = "h4 a")]
        private IList<IWebElement> actulaElementList;

        [FindsBy(How = How.CssSelector, Using = ".btn-success")]
        private IWebElement checkoutBtn;

        public IList<IWebElement> getActulaElementList()
        {
            return actulaElementList;
        }

        public void clickCheckoutBtn()
        {
            checkoutBtn.Click();
        }
    }
}

