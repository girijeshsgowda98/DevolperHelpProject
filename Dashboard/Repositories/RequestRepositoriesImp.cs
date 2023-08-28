using Dashboard.DbService;
using Dashboard.Models;

namespace Dashboard.Repositories
{
    public class RequestRepositoriesImp
    {
        List<RequestDataModel> requestData = new List<RequestDataModel>();
        public List<RequestDataModel> requestImp(NpgsqlDbService _dbService)
        {
            var sql = "SELECT modulename, controlname, actionname, requestedon, responseon FROM public.requestandresp;";
            var reader = _dbService.ExecuteQuery(sql);

            while (reader.Read())
            {
                requestData.Add(new RequestDataModel
                {
                    moduleName = reader.GetString(0),
                    controlName = reader.GetString(1),
                    actionName = reader.GetString(2),
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
                   responseon = group.Select(r => r.responseon).First(),
                   TotalRequest = group.Count(),
                   AverageTime = Math.Round(group.Average(r => r.requestedon.Millisecond) / group.Count(), 2)
               })
               .ToList();
            return result;
        }
    }
}
