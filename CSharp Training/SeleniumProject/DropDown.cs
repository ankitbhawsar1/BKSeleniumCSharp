using System;
using System.Collections;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using WebDriverManager.DriverConfigs.Impl;

namespace SeleniumProject
{
    public class DropDown
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
        public void DropDownTest()
        {
            driver.Url = "https://rahulshettyacademy.com/loginpagePractise/";

            IWebElement dropwDown = driver.FindElement(By.CssSelector("select.form-control"));
            SelectElement se = new SelectElement(dropwDown);
            se.SelectByText("Teacher");
            se.SelectByValue("teach");
            se.SelectByIndex(1);

            IList<IWebElement> radios = driver.FindElements(By.Id("usertype"));

            foreach(IWebElement rad in radios)
            {
                if (rad.GetAttribute("value").Equals("user")) {
                    rad.Click();
                }
            }
            driver.FindElement(By.Id("okayBtn")).Click();

            //foreach (IWebElement rad in radios)
            //{
            //    if (rad.GetAttribute("value").Equals("user")) {
            //        bool isSelected = rad.Selected;
            //        TestContext.Progress.WriteLine("selected {0}", isSelected);

            //        Assert.AreEqual(isSelected, true);
            //    }
            //}
            bool isSelected = driver.FindElement(By.CssSelector("[value='user']")).Selected;
            TestContext.Progress.WriteLine("selected {0}", isSelected);
            Assert.That(isSelected, Is.True);

        }


        [TearDown]
        public void Teardown()
        {
            //if (driver != null)
            //{
            driver.Quit();
            driver.Dispose();
            //}
        }

    }
}

