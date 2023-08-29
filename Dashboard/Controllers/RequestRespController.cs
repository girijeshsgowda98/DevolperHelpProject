using Dashboard.DbService;
using Dashboard.Models;
using Dashboard.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Dashboard.Controllers
{
    public class RequestRespController : Controller
    {
        private readonly NpgsqlDbService _dbService;
        public RequestRepositoriesImp _requestRepositories;
        RequestModel requestModel = new RequestModel();

        public RequestRespController(NpgsqlDbService dbService,RequestRepositoriesImp requestRepositories)
        {
            _dbService = dbService;
            _requestRepositories = requestRepositories;
        }
        
        public IActionResult Index()
        {
            requestModel.RequestDataModel = _requestRepositories.requestImp(_dbService);
            return View(requestModel);
        }
    }
}
