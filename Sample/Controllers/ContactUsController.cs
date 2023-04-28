using Microsoft.AspNetCore.Mvc;

namespace Sample.Controllers
{
    public class ContactUsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
