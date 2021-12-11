using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using JDMTuneV2.Data;
using JDMTuneV2.Models;
using JDMTuneV2.PassHasher;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.DataProtection.Internal;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;


namespace JDMTuneV2.Controllers
{
    public class UserController: Controller
    {
        private readonly DataRepository _data;
        

        /// <summary>
        /// Конструктор для управления репозиторием с данными 
        /// </summary>
        /// <param name="data"></param>
        public UserController(DataRepository data)
        {
            _data = data;
        }
        
        public IActionResult Registration()
        {
            return View();
        }

        public IActionResult Login()
        {
            return View();
        }
        
        /// <summary>
        /// Эта штука регает юзера доставая данные из формочки RegRequest
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Registration([FromForm] RegRequest request)
        {
            if (ModelState.IsValid)
            {
                var user = IsUserExists(request.Email);
                if (user != null)
                {
                    return Conflict("Account already exist");
                }

                user = new User()
                {
                    Id = Guid.NewGuid(),
                    Email = request.Email,
                    Password = PassHasher.PassHashing.Encrypt(request.Password),
                    Role = "User"
                };
            
                _data.AccountCreate(user);

                await Authenticate(request.Email);
                
                return RedirectToAction("Index","Home");
            }
            
            return View(request);
        }

        /// <summary>
        /// Логин пользователя на сайте
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login([FromForm] AuthRequest request)
        {
            if (ModelState.IsValid)
            {
                var pass = PassHasher.PassHashing.Encrypt(request.Password);
                var user = Authentication(request.Email, pass);
            
                if (user == null) return View();

                await Authenticate(request.Email);

                return RedirectToAction("Index","Home");
            }
            return View(request);
        }
        

        /// <summary>
        /// Users Authentication
        /// </summary>
        /// <param name="email"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        private User Authentication(string email,string password)
        {
            return _data.Users.FirstOrDefault(u => u.Email == email && u.Password == password);
        }
        
        /// <summary>
        /// Проверка сущетсвует ли такой чел в бд
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        private User IsUserExists(string email)
        {
            return _data.Users.FirstOrDefault(u => email == u.Email);
        }
        
        /// <summary>
        /// Хуйня которая создает кукисы для авторизации 
        /// </summary>
        /// <param name="userName"></param>
        private async Task Authenticate(string userName)
        {
            
            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, userName)
            };
            
            
            ClaimsIdentity id = new ClaimsIdentity(claims, "ApplicationCookie", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
            
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(id));
        }
 
        
        /// <summary>
        /// Удаление кукисов или просто логаут
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Home");
        }
    }
}