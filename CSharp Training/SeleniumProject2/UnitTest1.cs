using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using WebDriverManager.DriverConfigs.Impl;

namespace SeleniumProject2;

public class Tests
{
    IWebDriver driver;

    [SetUp]

    public void SetUp()
    {
        new WebDriverManager.DriverManager().SetUpDriver(new ChromeConfig());
        driver = new ChromeDriver();
    }

    [Test]
    public void Test()
    {
        //driver.Url = "https://visualstudio.microsoft.com/downloads/";
        driver.Url = "https://rahulshettyacademy.com/loginpagePractise/";
        driver.FindElement(By.Id("username")).Clear();
        driver.FindElement(By.Id("username")).SendKeys("ankit");
        driver.FindElement(By.Name("password")).SendKeys("1234");
        driver.FindElement(By.CssSelector("input[value='Sign In']")).Click();
        Thread.Sleep(3000);
        string errorM = driver.FindElement(By.ClassName("alert-danger")).Text;
        TestContext.Progress.WriteLine("error {0}", errorM);

        IWebElement link = driver.FindElement(By.LinkText("Free Access to InterviewQues/ResumeAssistance/Material"));
        string actualLinkText = link.GetAttribute("href");

        string expectedLink = "https://rahulshettyacademy.com/documents-request";

        Assert.Equals(expectedLink, actualLinkText);
        Assert.AreEqual("Example Domain", driver.Title);
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
