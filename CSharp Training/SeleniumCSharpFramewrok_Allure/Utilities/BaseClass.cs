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
using NUnit.Allure.Attributes;
using System.Reflection;
using Allure.Commons;

[assembly: LevelOfParallelism(3)]//count mean how many paraller test will execute
namespace SeleniumCSharpFramework.Utilities
{
	public class BaseClass
	{
        //private IWebDriver driver;
        //npm install -g allure-commandline.
        //allure serve allure-results
        private ThreadLocal<IWebDriver> driver = new ThreadLocal<IWebDriver>();
            
        [SetUp]
        public void SetUp()
        {
          
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

            if (TestContext.CurrentContext.Result.Outcome != ResultState.Success)
            {
                CaptureScreenShot(driver.Value);
            }
         
            driver.Value.Quit();
            
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

        public void CaptureScreenShot(IWebDriver driver)
        {
            //string projectPath = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName;
            //string directory = projectPath + "\\Screenshot\\";
            //DateTime dateTime = DateTime.Now;
            //string fileName = "Screenshot_" + dateTime.ToString("h_mm_ss") + ".png";
            ////var directory = Directory.CreateDirectory(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "\\Screenshot\\");
            //var filPath = directory + fileName;
            //ITakesScreenshot ts = (ITakesScreenshot)driver;
            //var file = ts.GetScreenshot();
            //file.SaveAsFile(filPath);
            byte[] screenshot = ((ITakesScreenshot)driver).GetScreenshot().AsByteArray;
            AllureLifecycle.Instance.AddAttachment("Screenshot", "image/png", screenshot);
            //return filPath;


        }



        [AllureStep("{0}")]
        public void logSteps(string message)
        {
            // Intentionally left empty for Allure to handle logging.
        }
    }

    
}

