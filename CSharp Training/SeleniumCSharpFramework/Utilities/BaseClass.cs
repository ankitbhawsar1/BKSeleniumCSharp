using System.Configuration;
using System.Threading;
using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;
using NUnit.Framework.Interfaces;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using WebDriverManager.DriverConfigs.Impl;
using AventStack.ExtentReports.Model;

[assembly: LevelOfParallelism(3)]//count mean how many paraller test will execute
namespace SeleniumCSharpFramework.Utilities
{
	public class BaseClass
	{
        //private IWebDriver driver;

        private ThreadLocal<IWebDriver> driver = new ThreadLocal<IWebDriver>();
        public static ExtentReports extent;
        public static ThreadLocal<ExtentTest> extentTest = new ThreadLocal<ExtentTest>();

        [OneTimeSetUp]
        public void oneTimeSetup()
        {
            string projectPath = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName;
            string reportPath = projectPath + "/index.html";
            var htmlReporter = new ExtentSparkReporter(reportPath);
            extent = new ExtentReports();
            extent.AttachReporter(htmlReporter);
            extent.AddSystemInfo("Browser", "Chrome");
            extent.AddSystemInfo("Enviornment", "QA");
        }
        [SetUp]
        public void SetUp()
        {
            extentTest.Value = extent.CreateTest(TestContext.CurrentContext.Test.Name);
            string browser = TestContext.Parameters["browser"];// if we pass browser from cammand line.
            if (browser == null)
            {
                browser = ConfigurationManager.AppSettings["browser"];
            }
            
            //string url = ConfigurationManager.AppSettings["url"];

            InitBrowser(browser);

            driver.Value.Manage().Window.Maximize();
            //implicit wait
            driver.Value.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
            //driver.Url = "https://rahulshettyacademy.com/loginpagePractise/";
            //driver.Url = url;
        }

        private void InitBrowser(string browserName)
        {
            switch (browserName)
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

        //[OneTimeTearDown]
        [TearDown]
        public void Teardown()
        {
            var status = TestContext.CurrentContext.Result.Outcome.Status;
            string stackTrace = TestContext.CurrentContext.Result.StackTrace;
            DateTime dateTime = DateTime.Now;
            string fileName = "Screenshot_" + dateTime.ToString("h_mm_ss") + ".png";
            if (status == TestStatus.Failed)
            {
                getExtentTest().Fail("Test Failed", CaptureScreenShot(driver.Value, fileName));
                getExtentTest().Log(Status.Fail, "Test failed with logtrace" + stackTrace);
            }
            else if (status == TestStatus.Skipped)
            {

            }
            //if (driver != null)
            //{
            
            driver.Value.Quit();
            //driver.Dispose();
            //}
        }
        [OneTimeTearDown]
        public void OneTimeTearDown()
        {
            extent.Flush();
        }


        public IWebDriver getDriver()
        {
            return driver.Value;
        }

        public string getURL()
        {
            return ConfigurationManager.AppSettings["url"];
        }

        public static JsonReader getJsonReader()
        {
            return new JsonReader();
        }

        public Media CaptureScreenShot(IWebDriver driver, string fileName)
        {
            ITakesScreenshot ts = (ITakesScreenshot)driver;
            var screebshot = ts.GetScreenshot().AsBase64EncodedString;
            return MediaEntityBuilder.CreateScreenCaptureFromBase64String(screebshot, fileName).Build();
            
        }

        public ExtentTest getExtentTest()
        {
            return extentTest.Value;
        }
    }

    
}

