using Dashboard.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Dashboard.Controllers
{
    public class InsertQueryController : Controller
    {
        public InsertQueryRepository _repository { get; set; }
        public readonly IConfiguration _config;

        public InsertQueryController(InsertQueryRepository repository, IConfiguration config)
        {
            _repository = repository;
            _config = config;
        }
        public IActionResult Index()
        {
            return View();
        }

        //[HttpPost]
        public ActionResult Upload()
        {
            string filePath = "D:\\SuperAppDoc\\UserData_Matrix.xlsx";
            //string filePath = _config.GetConnectionString("filePath");
            List<string> insertQueries = new List<string>();
            if (filePath != null)
            {
                insertQueries = _repository.GenerateInsertQueries(filePath);
                 return View("InsertQuery", insertQueries);
            }
            
            return RedirectToAction("Index", insertQueries);
        }
    }
}
