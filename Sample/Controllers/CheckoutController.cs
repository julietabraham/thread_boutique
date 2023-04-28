using Microsoft.AspNetCore.Mvc;

namespace Sample.Controllers
{
    public class CheckoutController : Controller
    {
        public IActionResult Index()
        {

            return View();
        }
    }
}
