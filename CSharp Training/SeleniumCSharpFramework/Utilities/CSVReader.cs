using System.Globalization;
using CsvHelper;
using CsvHelper.Configuration;

namespace SeleniumCSharpFramework.Utilities
{
	public class CSVReader
	{

		private List<TestData> testDataList;

		public List<TestData> geTestDatatCSV()
		{
			using var reader = new StreamReader("/Users/ankit/BeyondKey/Local/QAAutomation/CSharp Training/SeleniumCSharpFramework/Data/TestDataC.csv");
			using (var csv = new CsvReader(reader, new CsvConfiguration(CultureInfo.InvariantCulture)))
			{
				testDataList = csv.GetRecords<TestData>().ToList();
			}
			return testDataList;
		}

		public class TestData
		{
			public string userName { get; set; }
            public string password { get; set; }
            public string product1 { get; set; }
            public string product2 { get; set; }
        }

        public class CSVData
        {
            public List<TestData> Data { get; set; }
            public List<string> ColumnNames { get; set; }
           
        }

		public CSVData ReadCSV(string filePath)
		{
            using var reader = new StreamReader(filePath);
            using (var csv = new CsvReader(reader, new CsvConfiguration(CultureInfo.InvariantCulture)))
            {
                var records = csv.GetRecords<TestData>().ToList();
                var columns = csv.HeaderRecord.ToList();

                return new CSVData
                {
                    Data = records,
                    ColumnNames = columns
                };
            }
           
        }

        public void updateCell(CSVData csvData, int rowNumber, string columnName, string newValue)
        {
            if (rowNumber < 0 || rowNumber >= csvData.Data.Count || !csvData.ColumnNames.Contains(columnName))
            {
                throw new ArgumentException("Invalid row or column name");
            }
            //update the specify cell
            var row = csvData.Data[rowNumber];
            var property = typeof(TestData).GetProperty(columnName);
            if (property != null)
            {
                property.SetValue(row, newValue);
            }
        }

        public void WriteCSV(string filePath, CSVData csvData)
        {
            using (var writer = new StreamWriter(filePath))
            using (var csv = new CsvWriter(writer, new CsvConfiguration(CultureInfo.InvariantCulture)))
            {
                csv.WriteRecords(csvData.Data);
            }
        }
    }
}

