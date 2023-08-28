using Microsoft.AspNetCore.Mvc;

namespace Dashboard.Controllers
{
    public class ExceptionDataController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
