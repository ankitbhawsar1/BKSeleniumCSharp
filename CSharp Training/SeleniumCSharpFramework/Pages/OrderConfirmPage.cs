using System;
using OpenQA.Selenium;
using SeleniumCSharpFramework.Utilities;
using SeleniumExtras.PageObjects;

namespace SeleniumCSharpFramework.Pages
{
	public class OrderConfirmPage
	{
        UtilityClass utility = new UtilityClass();
        private IWebDriver driver;
        public OrderConfirmPage(IWebDriver driver)
        {
            this.driver = driver;
            PageFactory.InitElements(driver, this);
        }


        [FindsBy(How = How.Id, Using = "country")]
        private IWebElement countryEle;

        [FindsBy(How = How.XPath, Using = "//*[@for='checkbox2']")]
        private IWebElement checkboxBtn;

        [FindsBy(How = How.CssSelector, Using = ".btn-success")]
        private IWebElement purchaseBtn;

        [FindsBy(How = How.ClassName, Using = "alert-success")]
        private IWebElement successMsgEle;



        public void selectCountry(string key, string locator)
        {
            By country = By.XPath("//a[text()='India']");
            countryEle.SendKeys(key);
            utility.waitForVisibility(driver, country);
            driver.FindElement(country).Click();
        }


        public void getCheckboxClick()
        {
            checkboxBtn.Click();
        }

        public void getPurchaseBtnClick()
        {
            purchaseBtn.Click();
        }

        public string getSuccessMsg()
        {
            return successMsgEle.Text;
        }

    }
}

