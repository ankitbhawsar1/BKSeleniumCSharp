using System;
using System.Diagnostics;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using WebDriverManager.DriverConfigs.Impl;

namespace SeleniumProject
{
	public class SeleniumFirstScript
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
            //driver.Url = "https://visualstudio.microsoft.com/downloads/";
            driver.Url = "https://rahulshettyacademy.com/loginpagePractise/";


            // match xpath rule - //tagname[@attribute=value]
            // match css rule - tagname[attribute=value]
            driver.FindElement(By.Id("username")).Clear();
            driver.FindElement(By.Id("username")).SendKeys("ankit");
            driver.FindElement(By.Name("password")).SendKeys("1234");
            

            string admin = driver.FindElement(By.CssSelector(".customradio:nth-child(1) span:nth-child(1)")).Text;
            TestContext.Progress.WriteLine("admin {0}", admin);

            Boolean iAdminChecked = driver.FindElement(By.CssSelector(".customradio:nth-child(1) input")).Selected;
            TestContext.Progress.WriteLine("admin isChecked {0}", iAdminChecked);

            Boolean isUserChecked = driver.FindElement(By.CssSelector(".customradio:nth-child(2) input")).Selected;
            TestContext.Progress.WriteLine("user isChecked {0}", isUserChecked);

            //driver.FindElement(By.CssSelector("input[value='Sign In']")).Click();
            driver.FindElement(By.CssSelector("#signInBtn")).Click();


            IWebElement ele = driver.FindElement(By.CssSelector("#signInBtn"));
            //Explici wait

            WebDriverWait wait = new(driver, TimeSpan.FromSeconds(5));
            //wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.TextToBePresentInElementValue(ele, "Sign In"));
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.ClassName("alert-danger")));

            //Thread.Sleep(3000);
            string errorM = driver.FindElement(By.ClassName("alert-danger")).Text;
            TestContext.Progress.WriteLine("error {0}", errorM);
            string usrAndPass = driver.FindElement(By.CssSelector(".text-center.text-white")).Text;
            TestContext.Progress.WriteLine("usrAndPass {0}", usrAndPass);

            IWebElement link = driver.FindElement(By.LinkText("Free Access to InterviewQues/ResumeAssistance/Material"));
            string actualLinkText = link.GetAttribute("href");

            TestContext.Progress.WriteLine("actualLinkText {0}", actualLinkText);

            string expectedLink = "https://rahulshettyacademy.com/documents-request";
            Assert.AreEqual(actualLinkText, expectedLink);
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

