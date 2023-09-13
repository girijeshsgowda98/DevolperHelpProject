using Microsoft.AspNetCore.Mvc;

namespace Dashboard.Controllers
{
    public class PaymentController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
