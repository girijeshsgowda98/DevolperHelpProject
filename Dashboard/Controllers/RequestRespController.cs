using Microsoft.AspNetCore.Mvc;

namespace Dashboard.Controllers
{
    public class RequestRespController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

    }
}
