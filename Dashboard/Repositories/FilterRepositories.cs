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
        //FilterListModel filterModel = new FilterListModel();
        FilterListModel result = new FilterListModel();

        public NpgsqlDbService _dbService;
        public FilterRepositories(NpgsqlDbService dbService,RequestRepositoriesImp requestRepositoriesImp, ExceptionRepositories exceptionRepositories)
        { 
            _dbService = dbService; 
            _requestRepositories = requestRepositoriesImp;
            _exceptionRepositories= exceptionRepositories;
        }
        public FilterListModel FilterImp(string type, string moduleName, string control, string input)
        {
            
            if (type == "Apis")
            {
                if (moduleName == null & control == null & input == null)
                {
                     result.RequestDataModel = _requestRepositories.requestImpFilter(_dbService).Take(100).ToList();
                }
                
                if (moduleName != null)
                {
                    if (control != null)
                    {
                        if (input != null)
                        {
                            result.RequestDataModel = _requestRepositories.requestImpFilter(_dbService).Where(r => (r.cliendcode == input || r.uccid == input || r.uniqueid == input || r.usermasterid == input) & r.moduleName == moduleName & r.controlName == control).Take(100).ToList();

                        }
                        if (input == null)
                        {
                            result.RequestDataModel = _requestRepositories.requestImpFilter(_dbService).Where(r => r.moduleName == moduleName & r.controlName == control).Take(100).ToList();
                        }
                    }
                    if(control == null & input == null)
                    {
                        result.RequestDataModel = _requestRepositories.requestImpFilter(_dbService).Where(r => r.moduleName == moduleName).Take(10).ToList();
                    }
                   
                }
            }
            if (type == "Exceptions")
            {
                if (moduleName == null & control == null & input == null)
                {
                    result.ExceptionsDataModel = _exceptionRepositories.ExceptionImpFilter(_dbService).OrderByDescending(r => r.createdon).Take(10).ToList();
                }
                
                if (moduleName != null)
                {

                    if (control != null)
                    {
                        if (input != null)
                        {
                            result.ExceptionsDataModel = _exceptionRepositories.ExceptionImpFilter(_dbService).Where(r => (r.cliendcode == input || r.uccid == input || r.uniqueid == input) & r.moduleName == moduleName & r.controlName == control).OrderByDescending(r => r.createdon).Take(10).ToList();

                        }
                        if (input == null)
                        {
                            result.ExceptionsDataModel = _exceptionRepositories.ExceptionImpFilter(_dbService).Where(r => r.moduleName == moduleName & r.controlName == control).OrderByDescending(r => r.createdon).Take(10).ToList();
                        }
                    }
                    if (control == null & input == null)
                    {
                        result.ExceptionsDataModel = _exceptionRepositories.ExceptionImpFilter(_dbService).Where(r => r.moduleName == moduleName).Take(100).ToList();
                    }
                }
            }

            return result;
        }
    }
}
