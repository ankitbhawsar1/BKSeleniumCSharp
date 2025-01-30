using System.Data;
using System.Diagnostics;
using OpenQA.Selenium;

using OpenQA.Selenium.Support.UI;
using SeleniumCSharpFramework.Pages;
using SeleniumCSharpFramework.Utilities;
using static SeleniumCSharpFramework.Utilities.CSVReader;

namespace SeleniumCSharpFramework.Tests;
[Parallelizable(ParallelScope.Self)]
public class E2EExcel : BaseClass
{
    UtilityClass utility = new UtilityClass();

    private static IEnumerable<TestCaseData> getTestData = UtilityClass.getTestDataExcel();
    //private static DataTable testData = ExcelReader.ExcelToDataTable("data/TestData.xlsx");
    private static IEnumerable<TestCaseData> getTestDataByRow = UtilityClass.getTestDataExcelByRow();
    
    [SetUp]
    public void setupE2E()
    {
        getDriver().Url = getURL();
    }

    [Test]
    //[TestCase("rahulshettyacademy", "learning")]// data driven
    //[TestCase("ankit", "1234")]// test will run 2 times with both credentials/
    [TestCaseSource(nameof(getTestData))]
    public void Test(string username, string password)
    {
        
        LoginPage login = new LoginPage(getDriver());
        //ProductPage productPage = new ProductPage(getDriver());
        CheckoutPage checkoutPage = new CheckoutPage(getDriver());
        OrderConfirmPage orderPage = new OrderConfirmPage(getDriver());

        
        ProductPage productPage = login.ValidLogin(username, password);

        string[] mobiles = { "iphone X", "Blackberry" };

        //TestContext.Progress.WriteLine("expectedProduct --{0}", getJsonReader().getArrayData("expectedProduct"));

        //List<string> mobiles = getJsonReader().getArrayData("expectedProduct");
        //string[] mobiles = getJsonReader().getArrayData("expectedProduct");
        //foreach (var name in getJsonReader().getArrayData("expectedProduct"))
        //{
        //    TestContext.Progress.WriteLine("expectedProduct --{0}",name);
        //}

        utility.waitForVisibility(getDriver(), productPage.checkout);

        IList<IWebElement> productEles = productPage.getProducts();

        foreach (IWebElement product in productEles)
        {
            string text = product.FindElement(productPage.cardTitle).Text;
            if (mobiles.Contains(text))
            {
                product.FindElement(productPage.cardFooterBtn).Click();
            }
        }
   
            productPage.getCheckout();



        string[] productList = { "", "" };
    
        IList<IWebElement> productEles2 = checkoutPage.getActulaElementList();
        for (int i = 0; i < productEles2.Count; i++)
        {
            string text = productEles2[i].Text;
            productList[i] = text;
        }

        Assert.That(mobiles, Is.EqualTo(productList));


        checkoutPage.clickCheckoutBtn();


        

        orderPage.selectCountry("ind", "//a[text()='India']");
         

        orderPage.getCheckboxClick();




        orderPage.getPurchaseBtnClick();
        string successText = orderPage.getSuccessMsg();
        StringAssert.Contains("Success", successText);


        
    }

    //[Test]
    ////[TestCase("rahulshettyacademy", "learning")]// data driven
    ////[TestCase("ankit", "1234")]// test will run 2 times with both credentials/
    //[TestCaseSource(nameof(getTestDataByRow))]
    //public void TestByRow(DataRow row)
    //{

    //    LoginPage login = new LoginPage(getDriver());
    //    //ProductPage productPage = new ProductPage(getDriver());
    //    CheckoutPage checkoutPage = new CheckoutPage(getDriver());
    //    OrderConfirmPage orderPage = new OrderConfirmPage(getDriver());

    //    string username = row["UserName"].ToString();
    //    string password = row["Password"].ToString();
    //    ProductPage productPage = login.ValidLogin(username, password);

    //    string[] mobiles = { "iphone X", "Blackberry" };

    //    //TestContext.Progress.WriteLine("expectedProduct --{0}", getJsonReader().getArrayData("expectedProduct"));

    //    //List<string> mobiles = getJsonReader().getArrayData("expectedProduct");
    //    //string[] mobiles = getJsonReader().getArrayData("expectedProduct");
    //    //foreach (var name in getJsonReader().getArrayData("expectedProduct"))
    //    //{
    //    //    TestContext.Progress.WriteLine("expectedProduct --{0}",name);
    //    //}

    //    utility.waitForVisibility(getDriver(), productPage.checkout);

    //    IList<IWebElement> productEles = productPage.getProducts();

    //    foreach (IWebElement product in productEles)
    //    {
    //        string text = product.FindElement(productPage.cardTitle).Text;
    //        if (mobiles.Contains(text))
    //        {
    //            product.FindElement(productPage.cardFooterBtn).Click();
    //        }
    //    }

    //    productPage.getCheckout();



    //    string[] productList = { "", "" };

    //    IList<IWebElement> productEles2 = checkoutPage.getActulaElementList();
    //    for (int i = 0; i < productEles2.Count; i++)
    //    {
    //        string text = productEles2[i].Text;
    //        productList[i] = text;
    //    }

    //    Assert.That(mobiles, Is.EqualTo(productList));


    //    checkoutPage.clickCheckoutBtn();




    //    orderPage.selectCountry("ind", "//a[text()='India']");


    //    orderPage.getCheckboxClick();




    //    orderPage.getPurchaseBtnClick();
    //    string successText = orderPage.getSuccessMsg();
    //    StringAssert.Contains("Success", successText);



    //}


    

    //[Test]
    //public void TestUpdateExcel()
    //{
    //    string projectPath = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName;
    //    TestContext.Progress.WriteLine($"Current Directory: {Directory.GetCurrentDirectory()}");

    //    string projectPath2 = Path.Combine("data", "TestData.csv");
    //    Console.WriteLine($"Looking for file at: {Path.GetFullPath(projectPath2)}");

    //    string testDataPath = projectPath + "/Data/" + "TestData.xlsx";
    //    DataCollection dt = new DataCollection();
    //    dt.updateExcel(testDataPath, "Sheet1", 1, "UserName", "a");
    //}


    
}
