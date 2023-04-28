using Sample.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

namespace Sample.Controllers
{
    public class OnlineStoreController : Controller
    {
        OnlineStoreContext context;
        public OnlineStoreController(OnlineStoreContext context)
        {
            this.context = context;
        }
        public IActionResult Index()
        {
            var category = context.Categories.ToList();
           
            return View(category);
        }
        public IActionResult Browse(string category)
        {
            var categoryModel=context.Categories.Include("Products")
                .Where(c=>c.Name==category).ToList();

            return View(categoryModel);
        }
        public IActionResult Details(int id)
        {
            var item = context.Products.Where(p => p.ProductId == id)
                .Include("Category")
                .Include("Brand").ToList();
          
            return View(item);
        }
    }
}
