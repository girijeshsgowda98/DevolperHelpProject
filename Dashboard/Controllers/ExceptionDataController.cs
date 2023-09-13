using Dashboard.DbService;
using Dashboard.Models;
using Dashboard.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Dashboard.Controllers
{
    public class ExceptionDataController : Controller
    {
        private readonly NpgsqlDbService _dbService;
        public ExceptionRepositories _exceptionRepositories;
        ExceptionModel exceptionModel = new ExceptionModel();
        public ExceptionDataController(NpgsqlDbService dbService, ExceptionRepositories exceptionRepositories)
        {
            _dbService = dbService;
            _exceptionRepositories = exceptionRepositories;
        }
        public IActionResult Index(string query)
        {
            exceptionModel.ExceptionsDataModel = _exceptionRepositories.ExceptionImp(_dbService);
            exceptionModel.ExceptionsListModel = _exceptionRepositories.ExceptionListImp(_dbService);
            return View(exceptionModel);
        }
    }
}
