using Dashboard.DbService;
using Dashboard.Models;

namespace Dashboard.Repositories
{
    public class ExceptionRepositories
    {
        List<ExceptionsDataModel> exceptionData = new List<ExceptionsDataModel>();
        List<ExceptionsListModel> exceptionList = new List<ExceptionsListModel>();
        public List<ExceptionsDataModel> ExceptionImp(NpgsqlDbService _dbService)
        {
            var sql = "SELECT modulename, controlname, exception, actionname, createdon FROM usermaster.tbl_api_exception_logs;";
            var reader = _dbService.ExecuteQuery(sql);

            while (reader.Read())
            {
                exceptionData.Add(new ExceptionsDataModel
                {
                    moduleName = reader.IsDBNull(0) ? "Unknown" : reader.GetString(0),
                    controlName = reader.IsDBNull(1) ? "Unknown" : reader.GetString(1),
                    actionName = reader.IsDBNull(3) ? "Unknown" : reader.GetString(3),
                    createdon = reader.GetDateTime(4)
                });
            }
            var result = exceptionData
               .GroupBy(r => r.moduleName)
               .Select(group => new ExceptionsDataModel
               {
                   moduleName = group.Key,
                   controlName = group.Select(r => r.controlName).First(),
                   actionName = group.Select(r => r.actionName).First(),
                   createdon = group.Select(r => r.createdon).First(),
                   TotalExceptions = group.Count()
               }).OrderByDescending(r => r.TotalExceptions)
               .ToList();
            return result;
        }

        public List<ExceptionsListModel> ExceptionListImp(NpgsqlDbService _dbService)
        {
            var sql = "SELECT modulename, controlname, exception, actionname, createdon FROM usermaster.tbl_api_exception_logs;";
            var reader = _dbService.ExecuteQuery(sql);

            while (reader.Read())
            {
                exceptionList.Add(new ExceptionsListModel
                {
                    moduleName = reader.IsDBNull(0) ? "Unknown" : reader.GetString(0),
                    controlName = reader.IsDBNull(1) ? "Unknown" : reader.GetString(1),
                    actionName = reader.IsDBNull(3) ? "Unknown" : reader.GetString(3),
                    createdon = reader.GetDateTime(4)
                });
            }
            var result = exceptionList
               .GroupBy(r => r.moduleName)
               .Select(group => new ExceptionsListModel
               {
                   moduleName = group.Key,
                   controlName = group.Select(r => r.controlName).First(),
                   actionName = group.Select(r => r.actionName).First(),
                   createdon = group.Select(r => r.createdon).First()
               }).Take(3).ToList();
            return result;
        }
    }
    }

