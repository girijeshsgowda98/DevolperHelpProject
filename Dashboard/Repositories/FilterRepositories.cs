using Dashboard.DbService;
using Dashboard.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Office.Interop.Excel;

namespace Dashboard.Repositories
{
    public class FilterRepositories
    {
        public RequestRepositoriesImp _requestRepositories;
        public ExceptionRepositories _exceptionRepositories;
        List<RequestDataModel> requestData = new List<RequestDataModel>();
        List<ExceptionsDataModel> exceptionData = new List<ExceptionsDataModel>();
        FilterListModel filterModel = new FilterListModel();

        public NpgsqlDbService _dbService;
        public FilterRepositories(NpgsqlDbService dbService,RequestRepositoriesImp requestRepositoriesImp, ExceptionRepositories exceptionRepositories)
        { 
            _dbService = dbService; 
            _requestRepositories = requestRepositoriesImp;
            _exceptionRepositories= exceptionRepositories;
        }
        public FilterListModel FilterImp(string type, string moduleName, string input)
        {
            if (type == "Apis")
            {
                var result = _requestRepositories.requestImpFilter(_dbService,moduleName,input).Take(100).ToList();
                
                if (moduleName != null)
                {
                    if (input != null)
                    {
                        result = _requestRepositories.requestImpFilter(_dbService,moduleName,input).Where(r =>  (r.cliendcode == input || r.uccid == input || r.uniqueid == input || r.usermasterid == input) &  r.moduleName == moduleName).Take(100).ToList();
                        
                    }
                    if (input == null)
                    {
                        result = _requestRepositories.requestImpFilter(_dbService,moduleName,input).Where(r => r.moduleName == moduleName).Take(100).ToList();
                    }
                   
                }
                filterModel.RequestDataModel = result;
            }
            if (type == "Exceptions")
            {
                var result2 = _exceptionRepositories.ExceptionImpFilter(_dbService, moduleName, input).OrderByDescending(r=>r.createdon).Take(10).ToList();
                if (moduleName != null)
                {
                    if (input != null)
                    {
                        result2 = _exceptionRepositories.ExceptionImpFilter(_dbService, moduleName, input).Where(r => (r.cliendcode == input || r.uccid == input || r.uniqueid == input ) & r.moduleName == moduleName).OrderByDescending(r => r.createdon).Take(10).ToList();

                    }
                    if (input == null)
                    {
                        result2 = _exceptionRepositories.ExceptionImpFilter(_dbService, moduleName, input).Where(r => r.moduleName == moduleName).OrderByDescending(r => r.createdon).Take(10).ToList();
                    }

                }
                filterModel.ExceptionsDataModel = result2;
            }

            return filterModel;
        }
    }
}
