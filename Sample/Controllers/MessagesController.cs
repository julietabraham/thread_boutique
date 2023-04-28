using Microsoft.AspNetCore.Mvc;
using Sample.Models;
using System.Diagnostics;

namespace Sample.Controllers
{
    public class MessagesController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
      
        public IActionResult Privacy()
        {
            return View();
        }
    }
}