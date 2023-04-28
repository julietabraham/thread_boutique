using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Sample.Controllers
{
    [Authorize(Roles ="Admin")]
    public class RoleController : Controller
    {
        //1-Create private properties
        private readonly RoleManager<IdentityRole> _roleManager;
        
        //2-Create constroctor to inject the objects
        public RoleController(RoleManager<IdentityRole> roleManager)
        {
           _roleManager = roleManager;
        }

        //Display all the roles 
        public IActionResult Index()
        {
            var roles = _roleManager.Roles;
            return View(roles); //will return IdentityRole
        }

        [HttpGet]
        public IActionResult Create() {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(IdentityRole model)
        {
            if (!_roleManager.RoleExistsAsync(model.Name).GetAwaiter().GetResult())
            {
                _roleManager.CreateAsync(new IdentityRole (model.Name)).GetAwaiter().GetResult();
            }
            return RedirectToAction("Index");
        }
    }
}
