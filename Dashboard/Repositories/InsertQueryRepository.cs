using OfficeOpenXml;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;

namespace Dashboard.Repositories
{
    public class InsertQueryRepository
    {
        public List<string> GenerateInsertQueries(string filePath)
        {
            var insertProcessor = new InsertQueryRepository();
            var dataTable = insertProcessor.ReadExcelData(filePath);
            string tableName = "YourTableName";
            List<string> insertQueries = new List<string>();
            insertQueries = insertProcessor.GenerateAndExecuteInsertQueries(dataTable, tableName);

            
            
            return insertQueries;
            //return null;
        }

        public DataTable ReadExcelData(string filePath)
        {
            DataTable dataTable = new DataTable();
            // LicenseContext of the ExcelPackage class:
            //ExcelPackage.LicenseContext = LicenseContext.Commercial;

            // If you use EPPlus in a noncommercial context
            // according to the Polyform Noncommercial license:
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;


            using (var package = new ExcelPackage(new FileInfo(filePath)))
            {
                var worksheet = package.Workbook.Worksheets[0];
                int rowCount = worksheet.Dimension.Rows;
                int colCount = worksheet.Dimension.Columns;

                // Assuming the first row contains column headers
                for (int col = 1; col <= colCount; col++)
                {
                    string columnName = worksheet.Cells[1, col].Value?.ToString();
                    if (!string.IsNullOrEmpty(columnName))
                    {
                        dataTable.Columns.Add(columnName);
                    }
                }

                // Loop through rows and cells to read data
                for (int row = 2; row <= rowCount; row++)
                {
                    DataRow dataRow = dataTable.NewRow();

                    for (int col = 1; col <= colCount; col++)
                    {
                        string cellValue = worksheet.Cells[row, col].Value?.ToString();
                        dataRow[col - 1] = cellValue;
                    }

                    dataTable.Rows.Add(dataRow);
                }
            }

            return dataTable;
        }
        public List<string> GenerateAndExecuteInsertQueries(DataTable dataTable, string tableName)
        {
            List<string> queries = new List<string>();
            List<string> commonColumns = commoncolums(dataTable);
            //List<string> desiredColums = desiredcolumns(dataTable);
            //string[] commonstring = commonColumns.ToArray();
            string[] commonstring = commonColumns.Select(x => x.ToString()).ToArray();
            foreach (DataRow row in dataTable.Rows)
                {
                int k = 4;
                for (int i = 4 ;i <=dataTable.Columns.Count-1;i++)
                {
                    string columnNames = "";
                    string columnValues = "";
                    foreach (DataColumn columns in dataTable.Columns)
                    {
                        if (columns.ColumnName == commonstring.ElementAt(1) || columns.ColumnName == commonstring.ElementAt(3))
                        {
                            //columnNames += columns.ColumnName + ",";
                            columnValues += "'" + row[columns.ColumnName] + "',";
                        }
                        if (columns.ColumnName == commonstring.ElementAt(k))
                        {
                            //columnNames += columns.ColumnName + ",";
                            columnValues += "'" + columns.ColumnName + "," + "'" + row[columns.ColumnName] + "',";
                            break;
                        }
                    }
                    k++;
                    columnNames = ("contextType,kyctype,access,type,condition,updatedon,updatedoff");
                    //columnNames = columnNames.TrimEnd(',');
                    columnValues = columnValues.TrimEnd(',');

                    string insertQuery = $"INSERT INTO {tableName} ({columnNames}) VALUES ({columnValues});";
                    queries.Add(insertQuery);
                    insertQuery = "";
                }
                }
                
            return queries;
        }

        private List<string> commoncolums(DataTable dataTable)
        {
            var commonColumns = dataTable.Columns
                   .Cast<DataColumn>()
                   .Select(x => x.ColumnName)
                   .ToList();
            return commonColumns;
        }
    }
}  
