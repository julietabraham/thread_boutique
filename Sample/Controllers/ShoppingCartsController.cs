using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Sample.Models;
using Sample.ViewModels;

namespace Sample.Controllers
{
    public class ShoppingCartsController : Controller
    {
        OnlineStoreContext ctx;
        public const string CartSessionKey = "CartId";
        public ShoppingCartsController(OnlineStoreContext context) 
        {
            ctx= context;
        }
        public IActionResult Index()
        {
            var cart=ShoppingCart.GetCart(this.HttpContext);
            var viewModel = new CartViewModel 
            { 
             CartItems=cart.GetCartItems(),
             CartTotal=cart.GetTotal()
            };
            return View(viewModel);
        }
        public IActionResult AddToCart(int id)
        {
            var addItems=ctx.Products.Single(product=>product.ProductId==id);
            var cart = ShoppingCart.GetCart(this.HttpContext);
            cart.AddToCart(addItems);
            return RedirectToAction("Index");
        }
      
        public IActionResult RemoveFromCart(int id)
        {
            var cartId = GetCartId(this.HttpContext);

            var cartItems=ctx.Carts.FirstOrDefault(c=>c.CartId==cartId
                           && c.ProductId==id);
            var product = ctx.Products.Find(id);
            ctx.Carts.Remove(cartItems);
            ctx.SaveChangesAsync();
            return RedirectToAction("Index");

           
          


            //var cart = ShoppingCart.GetCart(this.HttpContext);
            //int count = cart.RemoveFromCart(id);
            //var vm = new CartRemoveViewModel
            //{
            //    Message = "The product has been removed.",
            //    CartAmount = cart.GetTotal(),
            //    CartCount = cart.GetCount(),
            //    ItemCount = count,
            //    DeleteId = id,
            //};
            //return Json(vm);
        }

        public IActionResult CartSummary()
        {
            var cart=ShoppingCart.GetCart(this.HttpContext);
            ViewData["count"]=cart.GetCount();
            return PartialView("CartSummary");
        }
        public string GetCartId(HttpContext ctc)
        {
            var session = ctc.Session;

            if (!session.Keys.Contains(CartSessionKey))
            {
                var userName = ctc.User.Identity.Name;

                if (!string.IsNullOrWhiteSpace(userName))
                {
                    session.SetString(CartSessionKey, userName);
                }
                else
                {
                    Guid tempCartId = Guid.NewGuid();
                    session.SetString(CartSessionKey, tempCartId.ToString());
                }
            }
            return session.GetString(CartSessionKey);
        }
    }
}
