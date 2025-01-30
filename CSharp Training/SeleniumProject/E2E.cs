using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using WebDriverManager.DriverConfigs.Impl;
using static OpenQA.Selenium.BiDi.Modules.Script.EvaluateResult;

namespace SeleniumProject
{
	public class E2E
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

            IList<IWebElement> usrAndPass = driver.FindElements(By.XPath("//i"));
            driver.FindElement(By.Id("username")).Clear();
            if (usrAndPass.Count > 1)
            {
                


                driver.FindElement(By.Id("username")).SendKeys(usrAndPass[0].Text);
                driver.FindElement(By.Name("password")).SendKeys(usrAndPass[1].Text);
            }

          
            //driver.FindElement(By.CssSelector("input[value='Sign In']")).Click();
            driver.FindElement(By.CssSelector("#signInBtn")).Click();


            IWebElement ele = driver.FindElement(By.CssSelector("#signInBtn"));
            //Explici wait

            //WebDriverWait wait = new(driver, TimeSpan.FromSeconds(5));
            ////wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.TextToBePresentInElementValue(ele, "Sign In"));
            //wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.ClassName("alert-danger")));

            //Thread.Sleep(3000);

            string[] mobiles = { "iphone X", "Blackberry" };
            //string errorM = driver.FindElement(By.ClassName("alert-danger")).Text;
            //TestContext.Progress.WriteLine("error {0}", errorM);

            //if (errorM == "")
            //{
               
                WebDriverWait wait2 = new(driver, TimeSpan.FromSeconds(6));
                //wait2.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.PartialLinkText("Checkout")));
            wait2.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.XPath("//a[contains(text(),'Checkout')]")));

            IList<IWebElement> productEles = driver.FindElements(By.TagName("app-card"));

            foreach (IWebElement product in productEles)
            {
                string text = product.FindElement(By.CssSelector(".card-title a")).Text;
                if (mobiles.Contains(text))
                {
                    product.FindElement(By.CssSelector(".card-footer button")).Click();
                }
            }
           
            driver.FindElement(By.XPath("//a[contains(text(),'Checkout')]")).Click();
            string[] productList = { "", "" };
            IList<IWebElement> productEles2 = driver.FindElements(By.CssSelector("h4 a"));
            for(int i = 0; i < productEles2.Count;  i++)
            {
                string text = productEles2[i].Text;
                productList[i] = text;
            }

            Assert.That(mobiles, Is.EqualTo(productList));
            driver.FindElement(By.CssSelector(".btn-success")).Click();
            driver.FindElement(By.Id("country")).SendKeys("ind");


            WebDriverWait wait3 = new(driver, TimeSpan.FromSeconds(10));
            //wait2.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.PartialLinkText("Checkout")));
            wait3.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.XPath("//a[text()='India']")));
            driver.FindElement(By.XPath("//a[text()='India']")).Click();
            driver.FindElement(By.XPath("//*[@for='checkbox2']")).Click();
            driver.FindElement(By.CssSelector(".btn-success")).Click();
            string successText = driver.FindElement(By.ClassName("alert-success")).Text;

            StringAssert.Contains("Success", successText);
            

            //}


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

