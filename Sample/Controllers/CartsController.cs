using Microsoft.AspNetCore.Mvc;
using Sample.Models;

namespace Sample.Controllers
{
    public class CartsController : Controller
    {
        public IActionResult Index()
        {
            
            return View();
        }
    }
}
