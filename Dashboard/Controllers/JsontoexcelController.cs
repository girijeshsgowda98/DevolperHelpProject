
using ClosedXML.Excel;
using Dashboard.Models;
using Grpc.Core;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using OfficeOpenXml;

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
        public IActionResult ConvertJsonTextToExcel(string jsonData)
        {
            
            //var data = Newtonsoft.Json.JsonConvert.DeserializeObject<List<JsonToExcelModel>>(jsonData);

            // Deserialize the JSON data into your model
            var data = JsonConvert.DeserializeObject<List<JsonToExcelModel>>(jsonData);
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            // Create a new Excel package
            using (var package = new ExcelPackage())
            {
                var worksheet = package.Workbook.Worksheets.Add("Data");

                // Add the column headers
                worksheet.Cells["A1"].LoadFromCollection(data, true);

                // Set content type and return the Excel file
                //Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                Response.Headers["Content-Type"] = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                Response.Headers["Content-Disposition"] = "attachment; filename=ExcelDoc.xlsx";

                Response.Body.Write(package.GetAsByteArray());
                //Response.

                //Response.BinaryWrite(package.GetAsByteArray());
            }

            return new EmptyResult();
        }
    }
}

