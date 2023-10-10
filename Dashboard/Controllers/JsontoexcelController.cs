using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using OfficeOpenXml;
using System;

namespace Dashboard.Controllers
{
    public class JsontoexcelController : Controller
    {
        public IActionResult Index()
        {
            //return View();
            return View();
        }

        [HttpPost]
        public IActionResult ConvertJsonTextToExcel([FromBody] string jsonText)
        {
            if (string.IsNullOrWhiteSpace(jsonText))
            {
                return BadRequest("JSON data is empty or invalid.");
            }

            try
            {
                var jsonData = JArray.Parse(jsonText); // Parse the JSON text

                using (var package = new ExcelPackage())
                {
                    var worksheet = package.Workbook.Worksheets.Add("Data");

                    // Add headers from the first JSON object (assuming all objects have the same structure)
                    if (jsonData.Count > 0)
                    {
                        var firstObject = jsonData[0];
                        int columnCount = 1;

                        foreach (var property in firstObject)
                        {
                            worksheet.Cells[1, columnCount].Value = property.Path;
                            columnCount++;
                        }

                        // Populate data from JSON
                        int rowCount = 2;
                        foreach (var dataObject in jsonData)
                        {
                            int column = 1;
                            foreach (var property in dataObject)
                            {
                                worksheet.Cells[rowCount, column].Value = property;
                                column++;
                            }
                            rowCount++;
                        }
                    }

                    // Create a memory stream
                    var stream = new MemoryStream(package.GetAsByteArray());

                    // Set the content type and file name for the response
                    Response.Headers.Add("Content-Disposition", "attachment; filename=Data.xlsx");
                    return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
                }
            }
            catch (Exception ex)
            {
                return BadRequest($"Error: {ex.Message}");
            }
        }
    }
}

