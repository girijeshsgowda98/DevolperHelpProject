using Dashboard.DbService;
using Dashboard.Models;
using Dashboard.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Dashboard.Controllers
{
    public class MfpaymentController : Controller
    {
        private readonly NpgsqlDbService _dbService;
        public MfpaymentRepositories _mfpaymentRepositories;
        MfpaymentModel mfpayment = new MfpaymentModel();
        public MfpaymentController(NpgsqlDbService dbService, MfpaymentRepositories mfpaymentRepositories)
        {
            _dbService= dbService;
            _mfpaymentRepositories= mfpaymentRepositories;
        }
        public IActionResult Index()
        {
            mfpayment.MfpaymentDataModel = _mfpaymentRepositories.MfpaymentImp(_dbService);
            return View(mfpayment);
        }
    }
}
