using JDMTuneV2.Data;
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
        
    }
}