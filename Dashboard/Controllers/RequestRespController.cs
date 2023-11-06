using Dashboard.DbService;
using Dashboard.Models;
using Dashboard.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Dashboard.Controllers
{
    public class RequestRespController : Controller
    {
        private readonly NpgsqlDbService _dbService;
        public RequestRepositoriesImp _requestRepositories;
        RequestModel requestModel = new RequestModel();
        public FilterRepositories _filterRepositories;

        public RequestRespController(NpgsqlDbService dbService,RequestRepositoriesImp requestRepositories, FilterRepositories filterRepositories)
        {
            _dbService = dbService;
            _requestRepositories = requestRepositories;
            _filterRepositories = filterRepositories;
        }
        
        public IActionResult Index(string query)
        {
            requestModel.RequestDataModel = _requestRepositories.requestImp(_dbService);
            requestModel.RequestListModel = _requestRepositories.requestListImp(_dbService);
            return View(requestModel);
        }

        public IActionResult TopRequest(string query)
        {

            requestModel.RequestDataModel = _requestRepositories.requestImpFilter(_dbService).OrderByDescending(x => x.requestedon).Take(5).ToList();
            return View(requestModel);
        }

        #region API CALLS
        [HttpGet]
        public IActionResult GetAll(string status)
        {
            requestModel.RequestDataModel = _requestRepositories.requestImp(_dbService);
            switch (status)
            {
                case "topRequest":
                    requestModel.RequestDataModel = _requestRepositories.requestImpFilter(_dbService).OrderByDescending(x => x.requestedon).Take(5).ToList();
                    break;
                default:
                    break;
            }
            var data = requestModel.RequestDataModel;
            return Json(new { data });
        }
        #endregion
    }
}
