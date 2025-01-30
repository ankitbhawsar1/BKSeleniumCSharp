using System;
using System.Configuration;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using WebDriverManager.DriverConfigs.Impl;

namespace SalesforceProject.Utilities
{
	public class BaseClass
	{
		private ThreadLocal<IWebDriver> driver = new ThreadLocal<IWebDriver>();

		[OneTimeSetUp]
		public void SetUp()
		{
			string browser = TestContext.Parameters["browser"];
            InitBrowser(browser);
			driver.Value.Manage().Window.Maximize();
			driver.Value.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
        }

		private void InitBrowser(string browser)
		{
			switch (browser)
			{
				case "Chrome":
					new WebDriverManager.DriverManager().SetUpDriver(new ChromeConfig());
					driver.Value = new ChromeDriver();

                    break;

				case "Firefox":
					new WebDriverManager.DriverManager().SetUpDriver(new FirefoxConfig());
					driver.Value = new FirefoxDriver();
					break;

				default:
                    TestContext.Progress.WriteLine("Browser not available");
                    break;
			}
		}

		[TearDown]
		public void TearDown()
		{
			driver.Value.Quit();
		}

		public IWebDriver getDriver()
		{
			return driver.Value;
		}

		public string getURl()
		{
			return ConfigurationManager.AppSettings[ "url"];
		}
	}

}

