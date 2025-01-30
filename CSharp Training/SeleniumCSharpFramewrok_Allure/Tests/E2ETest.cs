using System.Diagnostics;
using AventStack.ExtentReports;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using SeleniumCSharpFramework.Pages;
using SeleniumCSharpFramework.Utilities;
using NUnit.Allure.Core;
using NUnit.Allure.Attributes;

namespace SeleniumCSharpFramework.Tests;

[AllureNUnit]

public class E2E : BaseClass
{
    UtilityClass utility = new UtilityClass();

    private static IEnumerable<TestCaseData> getTestData = UtilityClass.getTestData();

    [SetUp]
    public void setupE2E()
    {
        getDriver().Url = getURL();
    }

    [Test]
    //[TestCase("rahulshettyacademy", "learning")]// data driven
    //[TestCase("ankit", "1234")]// test will run 2 times with both credentials/
    [TestCaseSource(nameof(getTestData))]
    [AllureDescription("E2E Test to buy products")]
    [AllureStory("Buy Order")]
    [Parallelizable(ParallelScope.All), Category("Regression")]
    public void Test(string username, string password, string[] mobiles)
    {
       
        LoginPage login = new LoginPage(getDriver());
        //ProductPage productPage = new ProductPage(getDriver());
        CheckoutPage checkoutPage = new CheckoutPage(getDriver());
        OrderConfirmPage orderPage = new OrderConfirmPage(getDriver());

        
        ProductPage productPage = login.ValidLogin(username, password);

        
        utility.waitForVisibility(getDriver(), productPage.checkout);
        logSteps("successfull login with username" + username);
        IList<IWebElement> productEles = productPage.getProducts();

        TestContext.Progress.WriteLine("countttt ---" + productEles.Count);

        foreach (IWebElement product in productEles)
        {
            string text = product.FindElement(productPage.cardTitle).Text;
            if (mobiles.Contains(text))
            {
                product.FindElement(productPage.cardFooterBtn).Click();
            }
        }
        logSteps("Products added successfully");

        productPage.getCheckout();



        string[] productList = { "", "" };
    
        IList<IWebElement> productEles2 = checkoutPage.getActulaElementList();
        for (int i = 0; i < productEles2.Count; i++)
        {
            string text = productEles2[i].Text;
            productList[i] = text;
        }

        Assert.That(mobiles, Is.EqualTo(productList));

        logSteps("Expected products for added" + productList);


        checkoutPage.clickCheckoutBtn();


        

        orderPage.selectCountry("ind", "//a[text()='India']");
         

        orderPage.getCheckboxClick();




        orderPage.getPurchaseBtnClick();
        string successText = orderPage.getSuccessMsg();
        StringAssert.Contains("Success", successText);
        logSteps("Products purchased successfully");


    }
    [Test, Category("Smoke")]
    //[Test]
    public void ActionTest()
    {
        //driver.Url = "https://rahulshettyacademy.com/";
        getDriver().Url = "https://demoqa.com/droppable";
        Actions action = new Actions(getDriver());
        //action.MoveToElement(driver.FindElement(By.XPath("//div[@class='nav-outer clearfix']//a[@class='dropdown-toggle']"))).Perform();

        //action.Click(driver.FindElement(By.XPath("(//*[@class='dropdown-menu']//a)[1]"))).Perform();

        //driver.FindElement(By.XPath("//div[@class='nav-outer clearfix']//a[text()='About us']")).Click();
        //TestContext.Progress.WriteLine("eteete {0}", ete);

        //div[@class='nav-outer clearfix']//ul[@class='dropdown-menu']//a[text()='About us']      
        //div[@class='nav-outer clearfix']//a[text()='About us']

        //  (//*[@class='dropdown-menu']//a)[1]


        action.DragAndDrop(getDriver().FindElement(By.Id("draggable")), getDriver().FindElement(By.Id("droppable"))).Perform();
    }



}
