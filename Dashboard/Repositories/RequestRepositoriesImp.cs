using Dashboard.DbService;
using Dashboard.Models;

namespace Dashboard.Repositories
{
    public class RequestRepositoriesImp
    {
        List<RequestDataModel> requestData = new List<RequestDataModel>();
        List<RequestListModel> requestList = new List<RequestListModel>();
        public List<RequestDataModel> requestImp(NpgsqlDbService dbService)
        {
            var sql = "SELECT modulename, controlname, actionname, requestedon, responseon FROM usermaster.tbl_api_reqresp_logs;";
            var reader = dbService.ExecuteQuery(sql);

            while (reader.Read())
            {
                requestData.Add(new RequestDataModel
                {
                    moduleName = reader.IsDBNull(0) ? "Unknown" : reader.GetString(0),
                     //controlName = string.IsNullOrEmpty(reader.GetString(1))?string.Empty: reader.GetString(1),
                     //actionName = string.IsNullOrEmpty(reader.GetString(2)) ? "Unknown" : reader.GetString(2),
                    controlName = reader.IsDBNull(1)? "Unknown":reader.GetString(1),
                    actionName = reader.IsDBNull(2) ? "Unknown" : reader.GetString(2),
                    requestedon = reader.GetDateTime(3),
                    responseon = reader.GetDateTime(4)
                });
            }
            var result = requestData
               .GroupBy(r => r.moduleName)
               .Select(group => new RequestDataModel
               {
                   moduleName = group.Key,
                   controlName = group.Select(r => r.controlName).First(),
                   actionName = group.Select(r => r.actionName).First(),
                   requestedon = group.Select(r => r.requestedon).First(),
                   responseon = group.Select(r => r.responseon).Last(),
                   TotalRequest = group.Count(),
                   AverageTime = Math.Round(group.Average(r => r.requestedon.Millisecond), 2)
               }).Where(r => !r.moduleName.Contains("Unknown")).OrderByDescending(r => r.TotalRequest)
               .ToList();
            /*if(!string.IsNullOrEmpty(query))
            {
                result = result.Where(r => r.moduleName == query).ToList();
            }*/
            return result;
        }

        public List<RequestListModel> requestListImp(NpgsqlDbService dbService)
        {
            var sql = "SELECT modulename, controlname, actionname, requestedon, responseon FROM usermaster.tbl_api_reqresp_logs;";
            var reader = dbService.ExecuteQuery(sql);
            while (reader.Read())
            {
                requestList.Add(new RequestListModel
                {
                    moduleName = reader.IsDBNull(0) ? "Unknown" : reader.GetString(0),
                    //controlName = string.IsNullOrEmpty(reader.GetString(1))?string.Empty: reader.GetString(1),
                    //actionName = string.IsNullOrEmpty(reader.GetString(2)) ? "Unknown" : reader.GetString(2),
                    controlName = reader.IsDBNull(1) ? "Unknown" : reader.GetString(1),
                    actionName = reader.IsDBNull(2) ? "Unknown" : reader.GetString(2),
                    requestedon = reader.GetDateTime(3),
                    responseon = reader.GetDateTime(4)
                });
            }
            var result = requestData
               .GroupBy(r => r.moduleName)
               .Select(group => new RequestListModel
               {
                   moduleName = group.Key,
                   controlName = group.Select(r => r.controlName).First(),
                   actionName = group.Select(r => r.actionName).First(),
                   requestedon = group.Select(r => r.requestedon).First(),
                   responseon = group.Select(r => r.responseon).First()
               }).Where(r => !r.moduleName.Contains("Unknown")).Take(5)
               .ToList();
            /*if (!string.IsNullOrEmpty(query))
            {
                result = result.Where(r => r.moduleName == query).ToList();
            }*/
            return result;
        }
        public List<RequestDataModel> requestImpFilter(NpgsqlDbService dbService,string modulename,string input)
        {
            var sql = "SELECT modulename, controlname, actionname, requestedon, responseon,uniqueid, usermasterid, cliendcode, uccid FROM usermaster.tbl_api_reqresp_logs where modulename is not null and controlname is not null and actionname is not null and uniqueid is not null and usermasterid is not null and cliendcode is not null and uccid is not null order by requestedon desc;";
            var reader = dbService.ExecuteQuery(sql);

            while (reader.Read())
            {
                requestData.Add(new RequestDataModel
                {
                    moduleName = reader.IsDBNull(0) ? "Unknown" : reader.GetString(0),
                    controlName = reader.IsDBNull(1) ? "Unknown" : reader.GetString(1),
                    actionName = reader.IsDBNull(2) ? "Unknown" : reader.GetString(2),
                    requestedon = reader.GetDateTime(3),
                    responseon = reader.GetDateTime(4),
                    uniqueid = reader.IsDBNull(5) ? "Unknown" : reader.GetString(5),
                    usermasterid = reader.IsDBNull(6) ? "Unknown" : reader.GetString(6),
                    cliendcode = reader.IsDBNull(7) ? "Unknown" : reader.GetString(7),
                    uccid = reader.IsDBNull(8) ? "Unknown" : reader.GetString(8)
                });
            }
            var result = requestData.ToList();
            /*if(!string.IsNullOrEmpty(query))
            {
                result = result.Where(r => r.moduleName == query).ToList();
            }*/
            return result;
        }
    }
}
