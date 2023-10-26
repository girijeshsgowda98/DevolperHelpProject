
using ClosedXML.Excel;
using Dashboard.Models;
using DocumentFormat.OpenXml.Features;
using DocumentFormat.OpenXml.Spreadsheet;
using Grpc.Core;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System;
using System.Data;
using System.Reflection;

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
            var data = JsonConvert.DeserializeObject<List<JsonToExcelTextModel>>(jsonData);
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            using (var package = new ExcelPackage())
            {
                var worksheet = package.Workbook.Worksheets.Add("Data");

                worksheet.Cells["A1"].LoadFromCollection(data, true);

                Response.Headers["Content-Type"] = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                Response.Headers["Content-Disposition"] = "attachment; filename=ExcelDoc3.xlsx";

                Response.Body.Write(package.GetAsByteArray());
            }

            return new EmptyResult();
        }
        [HttpPost]
        public IActionResult ConvertJsonToExcel(string jsondata)
        {
             DateTime currentDateTime = DateTime.Now;
            string formattedDateTime = currentDateTime.ToString("yyyyMMddHHmmss");
            JsonToExcelTextModel model = new JsonToExcelTextModel();
            model.data = new Data();
            byte[] excelFile;
            bool temp = false;
            bool loginCountStatus = false;
            bool userLoginStatus = false;
            bool entry = false;
            // Counter to track the number of rows
            int rowCount = 0;

            model = JsonConvert.DeserializeObject<JsonToExcelTextModel>(jsondata);

            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            DataTable dt = new DataTable();
            dt.Columns.Add("Message", typeof(string));
            dt.Columns.Add("Status", typeof(string));
            dt.Columns.Add("TotalUser", typeof(string));
            dt.Columns.Add("IOSUser", typeof(string));
            dt.Columns.Add("Androidusers", typeof(string));
            dt.Columns.Add("Otherusers", typeof(string));
            dt.Columns.Add("Name", typeof(string));
            dt.Columns.Add("Mobile", typeof(string));
            dt.Columns.Add("UCC", typeof(string));
            dt.Columns.Add("Client Type", typeof(string));
            dt.Columns.Add("Login Count", typeof(string));
            dt.Columns.Add("Event Time", typeof(string));
            dt.Columns.Add("User IP", typeof(string));
            dt.Columns.Add("Device Info", typeof(string));
            dt.Columns.Add("Model", typeof(string));
            dt.Columns.Add("UUID", typeof(string));

            
            for (int i = 0; i < 3; i++)
            {
                if (rowCount < 3)
                {
                    // Modify header values as needed
                    dt.Rows.Add(); // Add an empty row for header modification
                    rowCount++; // Reset the counter for data rows
                }
            }

            foreach (var userDetail in model.data.userdetails)
            {
                string message = model.message;
                string status = model.status.ToString();
                string totaluser = model.data.totaluser.ToString();
                string iosusers = model.data.iosusers.ToString();
                string androidusers = model.data.androidusers.ToString();
                string otherusers = model.data.otherusers.ToString();
                string name = userDetail?._name;
                string mobile = userDetail?._mobile;
                string ucc = userDetail?.ucc;
                string clientType = userDetail?.clienttype;
                string loginCount = userDetail?.userlogincount.ToString();
                loginCountStatus = false;

                foreach (var deviceDetail in userDetail.userloggeddevicedetails)
                {
                    string eventTime = deviceDetail.eventtime?.ToString();
                    string userIp = deviceDetail?.userip;
                    string deviceinfo = deviceDetail?.deviceinfo;
                    string modelinfo = deviceDetail?.model;
                    string uuid = deviceDetail?.uuid;
                    entry = false;
                    userLoginStatus = false;
                    if (model.message != null && temp == false)
                    {

                        dt.Rows.Add(message, status, totaluser, iosusers, androidusers, otherusers, name, mobile, ucc, clientType, loginCount, eventTime, userIp, deviceinfo, modelinfo, uuid);
                        temp = true;
                        loginCountStatus = true;
                        userLoginStatus = true;
                        entry = true;
                        
                    }
                    if(temp == true && loginCountStatus == false && userLoginStatus == false &&entry == false)
                    {
                        dt.Rows.Add("", "", "", "", "", "", name, mobile, ucc, clientType, loginCount, eventTime, userIp, deviceinfo, modelinfo, uuid);
                        loginCountStatus = true;
                        userLoginStatus = true;
                        //entry = false;
                        

                    }
                    if (temp == true && loginCountStatus == true && userLoginStatus == false && entry == false)
                    {
                        dt.Rows.Add("", "", "", "", "", "", "", "", "", "", "", eventTime, userIp, deviceinfo, modelinfo, uuid);
                        //entry = false;
                        //loginCountStatus = true;
                        
                    }
                }
            }

            using (var package = new ExcelPackage())
            {
                var worksheet = package.Workbook.Worksheets.Add("Data");
                worksheet.Cells["A1"].LoadFromDataTable(dt, true);
                // Define parent and child headers

                // Get the entire range of cells (including both header and data cells)
                var entireRange = worksheet.Cells[1, 1, 3, dt.Columns.Count + 1];

                // Apply horizontal and vertical center alignment to the entire range
                entireRange.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                entireRange.Style.VerticalAlignment = ExcelVerticalAlignment.Center;

                //data field make tje center
                var dataRange = worksheet.Cells[4, 1, dt.Rows.Count + 1, 6];
                dataRange.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                //Get the entire range of cells (including both header and data cells)
                var specifiedRange = worksheet.Cells[4, dt.Columns.Count - 4, 4, dt.Columns.Count + 1];

                // Apply horizontal and vertical center alignment to the entire range
                specifiedRange.Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;

                worksheet.Cells["A1:A4"].Merge = true;
                worksheet.Cells["A1"].Value = "Message";
                worksheet.Cells["B1:B4"].Merge = true;
                worksheet.Cells["B1"].Value = "Status";
                worksheet.Cells["C1:P1"].Merge = true; // Merge cells for parent header
                worksheet.Cells["C1"].Value = "data";
                // Set child column headers
                worksheet.Cells["C2:C4"].Merge = true;
                worksheet.Cells["C2"].Value = "TotalUser";
                worksheet.Cells["D2:D4"].Merge = true;
                worksheet.Cells["D2"].Value = "IOSUser";
                worksheet.Cells["E2:E4"].Merge = true;
                worksheet.Cells["E2"].Value = "Androidusers";
                worksheet.Cells["F2:F4"].Merge = true;
                worksheet.Cells["F2"].Value = "Otherusers";

                worksheet.Cells["G2:P2"].Merge = true; // Merge cells for parent header
                worksheet.Cells["G2"].Value = "userdetails";
                worksheet.Cells["G3:G4"].Merge = true;
                worksheet.Cells["G3"].Value = "Name";
                worksheet.Cells["H3:H4"].Merge = true;
                worksheet.Cells["H3"].Value = "Mobile";
                worksheet.Cells["I3:I4"].Merge = true;
                worksheet.Cells["I3"].Value = "UCC";
                worksheet.Cells["J3:J4"].Merge = true;
                worksheet.Cells["J3"].Value = "Client Type";
                worksheet.Cells["K3:K4"].Merge = true;
                worksheet.Cells["K3"].Value = "Login Count";

                worksheet.Cells["L3:P3"].Merge = true; // Merge cells for parent header
                worksheet.Cells["L3"].Value = "userloggeddevicedetails";
                worksheet.Cells["L4"].Value = "Event Time";
                worksheet.Cells["M4"].Value = "User IP";
                worksheet.Cells["N4"].Value = "Device Info";
                worksheet.Cells["O4"].Value = "Model";
                worksheet.Cells["P4"].Value = "UUID";

                Response.Clear();
                excelFile = package.GetAsByteArray();
            }
           
            return File(excelFile, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",  formattedDateTime+
            "UserDetails.xlsx");
        }
    }
}

