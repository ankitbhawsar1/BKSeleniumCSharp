using System;
using OpenQA.Selenium;
using SeleniumCSharpFramework.Pages;
using SeleniumCSharpFramework.Utilities;
using static SeleniumCSharpFramework.Utilities.CSVReader;


namespace SeleniumCSharpFramework.Tests
{
    [Parallelizable(ParallelScope.Self)]
    public class E2ETestCSV: BaseClass
    {
        UtilityClass utility = new UtilityClass();
        private static IEnumerable<TestCaseData> getTestDataByCSV = UtilityClass.getTestDataCSV();

        [SetUp]
        public void setupE2E()
        {
            getDriver().Url = getURL();
        }

        [Test]
        //[TestCase("rahulshettyacademy", "learning")]// data driven
        //[TestCase("ankit", "1234")]// test will run 2 times with both credentials/
        [TestCaseSource(nameof(getTestDataByCSV))]
        public void TestByCSV(TestData data)
        {

            LoginPage login = new LoginPage(getDriver());
            //ProductPage productPage = new ProductPage(getDriver());
            CheckoutPage checkoutPage = new CheckoutPage(getDriver());
            OrderConfirmPage orderPage = new OrderConfirmPage(getDriver());

            string username = data.userName;
            string password = data.password;
            ProductPage productPage = login.ValidLogin(username, password);

            string[] mobiles = { data.product1, data.product2 };

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
        //public void TestUpdateCSV()
        //{
        //    string projectPath = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName;
        //    string testDataPath = projectPath + "/Data/" + "TestDataC.csv";
        //    CSVReader csvReader = new CSVReader();
        //    CSVData csvData = csvReader.ReadCSV(testDataPath);
        //    string outputPath = projectPath + "/Data/" + "UpdatedTestDataC.csv";
        //    csvReader.updateCell(csvData, 0, "userName", "ankit ab");
        //    csvReader.WriteCSV(outputPath, csvData);
        //}
    }
}

