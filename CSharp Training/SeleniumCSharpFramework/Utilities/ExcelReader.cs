using System;
using System.Data;
using System.Diagnostics;
using System.Text;
using ExcelDataReader;
using OfficeOpenXml;

namespace SeleniumCSharpFramework.Utilities
{
	public class ExcelReader
	{
		public static DataTable ExcelToDataTable(string fileName)
		{
            // Register the encoding provider
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            using FileStream stream = File.Open(fileName, FileMode.Open, FileAccess.Read);
			IExcelDataReader excelReader = ExcelReaderFactory.CreateOpenXmlReader(stream);
			DataSet resultSet = excelReader.AsDataSet(new ExcelDataSetConfiguration()
			{
                ConfigureDataTable = (_) => new ExcelDataTableConfiguration()
                {

					UseHeaderRow = true
				}
            });;

			DataTableCollection table = resultSet.Tables;
			DataTable resulTable = table["Sheet1"];

			return resulTable;
		}
	}

	public class DataCollection
	{
		public int rowNumber { get; set; }
		public string columnName { get; set; }
		public string columnValue { get; set; }

		List<DataCollection> dataCollection = new List<DataCollection>();

		public void collectToCollection(string fileName)
		{
			DataTable table = ExcelReader.ExcelToDataTable(fileName);
			for (int row = 1; row<=table.Rows.Count; row++)
			{
				for (int col = 0; col<table.Columns.Count; col++)
				{
                    DataCollection dtCollection = new DataCollection()
                    {
                        rowNumber = row,
						columnName = table.Columns[col].ColumnName,
						columnValue = table.Rows[row - 1][col].ToString()
                    };

					dataCollection.Add(dtCollection);
                }
			}
			
		}

		public string ReadData(int row, string column)
		{
			try
			{
				string data = (from columnData in dataCollection
							   where columnData.columnName == column
							   && columnData.rowNumber == row
							   select columnData.columnValue).SingleOrDefault();
				return data.ToString();

			}
			catch(Exception e)
			{
				Debug.WriteLine("erorrrrr  {0}", e);
				return null;
			}
		}

		public void updateExcel(string filePath, string workSheetName, int rowNumber, string columnName, string newValue)
		{
			ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
			using (var package = new ExcelPackage(new FileInfo(filePath)))
			{
				var workSheet = package.Workbook.Worksheets[workSheetName];
				if (workSheet == null)
				{
					throw new ArgumentException($"Worksheet'{workSheetName}' not found");
				}

				var rowIndex = rowNumber;
				if (rowIndex < 1 || rowIndex > workSheet.Dimension.Rows)
				{
					throw new ArgumentException("invalid row number");
				}

				var column = workSheet.Cells[1, 1, 1, workSheet.Dimension.Columns].FirstOrDefault(c => c.Text == columnName);
				if (column == null)
				{
					throw new ArgumentException($"Column '{columnName}' not found");
				}
				workSheet.Cells[rowIndex + 1, column.Start.Column].Value = newValue;
				package.SaveAs(new FileInfo(filePath));
			}
		}
	}
}

