using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using WebDriverManager.DriverConfigs.Impl;

namespace SeleniumProject
{
    public class Alert
    {
        private IWebDriver driver;

        [SetUp]

        public void SetUp()
        {
            new WebDriverManager.DriverManager().SetUpDriver(new ChromeConfig());
            driver = new ChromeDriver();

            driver.Manage().Window.Maximize();
            //implicit wait
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
            //driver.Url = "https://visualstudio.microsoft.com/downloads/";
            //driver.Url = "https://rahulshettyacademy.com/AutomationPractice/";

        }


        [Test]
        public void AlertTest()
        {

            string name = "Ankit";
            driver.FindElement(By.Id("name")).SendKeys(name);
            driver.FindElement(By.Id("confirmbtn")).Click();
            IAlert alert = driver.SwitchTo().Alert();
            string alertText = alert.Text;
            alert.Accept();

            StringAssert.Contains(name, alertText);



        }

        [Test]
        public void AutoTest()
        {
            driver.FindElement(By.Id("autocomplete")).SendKeys("ind");
            IList<IWebElement> options = driver.FindElements(By.CssSelector(".ui-menu-item div"));

            foreach (IWebElement option in options)
            {
                if (option.Text.Equals("India"))
                {
                    option.Click();
                }
            }
        }

        [Test]
        public void ActionTest()
        {
            //driver.Url = "https://rahulshettyacademy.com/";
            driver.Url = "https://demoqa.com/droppable";
            Actions action = new Actions(driver);
            //action.MoveToElement(driver.FindElement(By.XPath("//div[@class='nav-outer clearfix']//a[@class='dropdown-toggle']"))).Perform();

            //action.Click(driver.FindElement(By.XPath("(//*[@class='dropdown-menu']//a)[1]"))).Perform();

            //driver.FindElement(By.XPath("//div[@class='nav-outer clearfix']//a[text()='About us']")).Click();
            //TestContext.Progress.WriteLine("eteete {0}", ete);

            //div[@class='nav-outer clearfix']//ul[@class='dropdown-menu']//a[text()='About us']      
            //div[@class='nav-outer clearfix']//a[text()='About us']

            //  (//*[@class='dropdown-menu']//a)[1]


            action.DragAndDrop(driver.FindElement(By.Id("draggable")), driver.FindElement(By.Id("droppable"))).Perform();
        }

        [Test]
        public void FrameTest()
        {
            IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
            js.ExecuteScript("arguments[0].scrollIntoView(true)", driver.FindElement(By.Id("courses-iframe")));
            driver.SwitchTo().Frame("courses-iframe");//switch to frame inside a site.
            driver.FindElement(By.LinkText("Blog")).Click();
            driver.SwitchTo().DefaultContent();//switch back to the site from frame
            TestContext.Progress.WriteLine(" text :{0}", driver.FindElement(By.XPath("//button[text()='Practice']")).Text);

            js.ExecuteScript("arguments[0].scrollIntoView(true)", driver.FindElement(By.XPath("//button[text()='Practice']")));
        }

        [Test]
        public void WindowTest()
        {
            driver.Url = "https://rahulshettyacademy.com/loginpagePractise/";
            string parentWindow = driver.CurrentWindowHandle;
            driver.FindElement(By.XPath("//a[@class='blinkingText'][1]")).Click();

            TestContext.Progress.WriteLine(" window count :{0}", driver.WindowHandles.Count);

            driver.SwitchTo().Window(driver.WindowHandles[1]);

            string text = driver.FindElement(By.ClassName("red")).Text;

            TestContext.Progress.WriteLine(" text :{0}", text);
            string[] splitedText = text.Split(" ");

            string email = splitedText[4];
            driver.Close();
            driver.SwitchTo().Window(parentWindow);

            driver.FindElement(By.Id("username")).SendKeys(email);

            driver.SwitchTo().NewWindow(WindowType.Tab);
            driver.SwitchTo().NewWindow(WindowType.Window);


        }



        [TearDown]
        public void Teardown()
        {
            //if (driver != null)
            //{
            //driver.Quit();
            //driver.Dispose();
            //}
        }
    }
}

