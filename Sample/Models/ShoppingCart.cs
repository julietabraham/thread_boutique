using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Sample.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sample.Models
{
    public class ShoppingCart
    {
        OnlineStoreContext context = new OnlineStoreContext();

        public ShoppingCart(OnlineStoreContext ctx) 
        {
            context = ctx;
        }

        public ShoppingCart()
        {
        }

        string ShoppingCartId { get; set; }
        public const string CartSessionKey = "CartId";
        public static ShoppingCart GetCart(HttpContext context)
        {
            var cart = new ShoppingCart();
            cart.ShoppingCartId = cart.GetCartId(context);
            return cart;
        }
        // Helper method to simplify shopping cart calls
        public static ShoppingCart GetCart(Controller controller)
        {
            return GetCart(controller.HttpContext);
        }
        public void AddToCart(Product item)
        {
            
            var cartItem = context.Carts.FirstOrDefault(
                c => c.CartId == ShoppingCartId
                && c.ProductId == item.ProductId);

            if (cartItem == null)
            {
                
                cartItem = new Cart
                {
                    ProductId = item.ProductId,
                    CartId = ShoppingCartId,
                    Count = 1,
                    Created = DateTime.Now
                };
                context.Carts.Add(cartItem);
            }
            else
            {
                
                cartItem.Count++;
            }
            
            context.SaveChanges();
        }
        public int RemoveFromCart(int id)
        {
            var cartItem = context.Carts.FirstOrDefault(
                cart => cart.CartId == ShoppingCartId
                && cart.RecId == id);

            int itemCount = 0;

            if (cartItem != null)
            {
                if (cartItem.Count > 1)
                {
                    cartItem.Count--;
                    itemCount = cartItem.Count;
                }
                else
                {
                    context.Carts.Remove(cartItem);
                }
                
                context.SaveChanges();
            }
            return itemCount;
        }

        public void EmptyCart()
        {
            var cartItems = context.Carts.Where(
                cart => cart.CartId == ShoppingCartId);

            foreach (var cartItem in cartItems)
            {
                context.Carts.Remove(cartItem);
            }
            
            context.SaveChanges();
        }
        public List<Cart> GetCartItems()
        {
            return context.Carts.Where(
                cart => cart.CartId == ShoppingCartId)
                .Include("Product").
                ToList();
           
        }
        public int GetCount()
        {

            int? count = (from cartItems in context.Carts
                          where cartItems.CartId == ShoppingCartId
                          select (int?)cartItems.Count).Sum();

            return count ?? 0;
        }

        public decimal GetTotal()
        {
            
            decimal? total = (from cartItems in context.Carts
                              where cartItems.CartId == ShoppingCartId
                              select (int?)cartItems.Count *
                              cartItems.Product.Price).Sum();

            return total ?? decimal.Zero;
        }

        public int CreateOrder(Order order)
        {
            decimal? orderTotal = 0;

            var cartItems = GetCartItems();
            
            foreach (var item in cartItems)
            {
                var orderDetail = new OrderDetails
                {
                    ProductId = item.ProductId,
                    OrderId = order.OrderID,
                    Price = item.Product.Price,
                    Quantity = item.Count
                };
                
                orderTotal += (item.Count * item.Product.Price);

                context.OrderDetails.Add(orderDetail);

            }
            
            order.Total = orderTotal;


            context.SaveChanges();
            
            EmptyCart();
            
            return order.OrderID;
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
        public void MigrateCart(string Email)
        {
            var shoppingCart = context.Carts.Where(
                c => c.CartId == ShoppingCartId);

            foreach (Cart item in shoppingCart)
            {
                item.CartId = Email;
            }
            context.SaveChanges();
        }


    }
}