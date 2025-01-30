using System;
using System.Data;
using System.Diagnostics;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using static SeleniumCSharpFramework.Utilities.CSVReader;

namespace SeleniumCSharpFramework.Utilities
{
	public class UtilityClass: BaseClass
    {

        public void waitForVisibility(IWebDriver driver, By by)
        {
            WebDriverWait wait2 = new(driver, TimeSpan.FromSeconds(6));
            wait2.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(by));
        }

        public static IEnumerable<TestCaseData> getTestData()
        {
            
            yield return new TestCaseData(getJsonReader().getData("username"), getJsonReader().getData("password"), getJsonReader().getArrayData("expectedProduct"));
            yield return new TestCaseData("ankit", "1234", getJsonReader().getArrayData("expectedProduct"));

        }


        public static IEnumerable<TestCaseData> getTestDataExcel()
        {
            DataCollection dt = new DataCollection();
            dt.collectToCollection("data/TestData.xlsx");
           

            yield return new TestCaseData(dt.ReadData(1, "UserName"), dt.ReadData(1, "Password"));
            yield return new TestCaseData(dt.ReadData(2, "UserName"), dt.ReadData(2, "Password"));
     
        }

        public static IEnumerable<TestCaseData> getTestDataExcelByRow()
        {
            DataTable dataTable = ExcelReader.ExcelToDataTable("data/TestData.xlsx");
            foreach(DataRow row in dataTable.Rows)
            {
                yield return new TestCaseData(row);
            }

        }

        public static IEnumerable<TestCaseData> getTestDataCSV()
        {
            CSVReader csvReader = new CSVReader();
            List<TestData> testData = csvReader.geTestDatatCSV();
            foreach (TestData data in testData)
            {
                yield return new TestCaseData(data);
            }

        }
    }
}

