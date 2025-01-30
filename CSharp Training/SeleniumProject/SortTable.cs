using System;
using System.Collections;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using WebDriverManager.DriverConfigs.Impl;

namespace SeleniumProject
{
    public class SortTable
    {
        IWebDriver driver;

        [SetUp]

        public void SetUp()
        {
            new WebDriverManager.DriverManager().SetUpDriver(new ChromeConfig());
            driver = new ChromeDriver();

            driver.Manage().Window.Maximize();
            //implicit wait
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
        }


        [Test]
        public void Test()
        {
            ArrayList ar = new ArrayList();
            ArrayList newAr = new ArrayList();
            driver.Url = "https://rahulshettyacademy.com/seleniumPractise/#/offers";
            IWebElement page = driver.FindElement(By.Id("page-menu"));
            SelectElement se = new SelectElement(page);
            se.SelectByValue("20");

            IList<IWebElement> items = driver.FindElements(By.XPath("//td[1]"));

            foreach(IWebElement item in items)
            {
                ar.Add(item.Text);
            }
            ar.Sort();

            driver.FindElement(By.CssSelector("[aria-label*='fruit name']")).Click();
            IList<IWebElement> newItems = driver.FindElements(By.XPath("//td[1]"));

            foreach (IWebElement item in newItems)
            {
                newAr.Add(item.Text);
            }
            Assert.AreEqual(ar, newAr);
        }
    }
}

