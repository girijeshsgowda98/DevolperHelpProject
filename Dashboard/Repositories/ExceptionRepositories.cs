using Dashboard.DbService;
using Dashboard.Models;

namespace Dashboard.Repositories
{
    public class ExceptionRepositories
    {
        List<ExceptionsDataModel> exceptionData = new List<ExceptionsDataModel>();
        public List<ExceptionsDataModel> ExceptionImp(NpgsqlDbService _dbService)
        {
            var sql = "SELECT modulename, controlname, exceptions, actionname, requestedon, responseon FROM public.exceptionhandler;";
            var reader = _dbService.ExecuteQuery(sql);

            while (reader.Read())
            {
                exceptionData.Add(new ExceptionsDataModel
                {
                    moduleName = reader.GetString(0),
                    controlName = reader.GetString(1),
                    actionName = reader.GetString(3),
                    requestedon = reader.GetDateTime(4),
                    responseon = reader.GetDateTime(5)
                });
            }
            var result = exceptionData
               .GroupBy(r => r.moduleName)
               .Select(group => new ExceptionsDataModel
               {
                   moduleName = group.Key,
                   controlName = group.Select(r => r.controlName).First(),
                   actionName = group.Select(r => r.actionName).First(),
                   requestedon = group.Select(r => r.requestedon).First(),
                   responseon = group.Select(r => r.responseon).First(),
                   TotalExceptions = group.Count()
               })
               .ToList();
            return result;
        }
    }
}
