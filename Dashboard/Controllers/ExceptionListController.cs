using Microsoft.AspNetCore.Mvc;

namespace Dashboard.Controllers
{
    public class ExceptionListController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
