using System;
using JDMTuneV2.Data;
using JDMTuneV2.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace JDMTuneV2.Controllers
{
    public class ShopController : Controller
    {
        private readonly DataRepository _data;
        

        public ShopController(DataRepository data)
        {
            _data = data;
        }

        [Authorize]
        public IActionResult ShoppingCart()
        {
            return View();
        }

        [Authorize]
        public IActionResult CheckOut()
        {
            return View();
        }

        public IActionResult MarketPlace()
        {
            return View();
        }
        
        

        public IActionResult ProductPage()
        {
            return View();
        }

        public IActionResult DeleteProduct()
        {
            Cart.isCartNull = true;

            return View("ShoppingCart");
        }
        
        public IActionResult AddProduct()
        {
            if (Cart.isCartNull)
            {
                Cart.isCartNull = false;
            }

            return RedirectToAction("ShoppingCart", "Shop");
        }

        [Authorize]
        [HttpPost]
        public IActionResult CheckOut([FromForm] OrderRequest order)
        {
            var user = _data.GetAccountByEmail(User.Identity.Name);
            Order _order = new Order
            {
                OrderId = Guid.NewGuid(),
                UserId = user.Id,
                Name = order.Name,
                Surname = order.Surname,
                Country = order.Country,
                City = order.City,
                StreetAddress = order.StreetAddress,
                PostCode = order.PostCode,
                PhoneNumber = order.PhoneNumber,
            };
            
            _data.AddOrder(_order);
            
            return View();
        }
        
        
        
    }
}