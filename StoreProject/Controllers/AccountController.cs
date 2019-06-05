using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;
using StoreProject.Models;
using StoreProject.Services.Interfaces;

namespace StoreProject.Controllers
{
    public class AccountController : Controller
    {
        IAccountService _acc;
        private readonly IHttpContextAccessor _httpContext;

        public AccountController(IAccountService acc, IHttpContextAccessor httpContext)
        {
            _acc = acc;
            _httpContext = httpContext;
            ViewBag.activeUser = httpContext.HttpContext.Request.Cookies["login_user"];
        }
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(User user)
        {
            if (_acc.GetUser(user.Email, user.Password, out User theUser))
            {
                var json = JsonConvert.SerializeObject(theUser);
                Set("login_user", json);
                return RedirectToAction("Index", "Home");
            }
            return RedirectToAction("Indexx", "Home");
        }

        public IActionResult Logout()
        {
            Response.Cookies.Delete("login_user");
            return RedirectToAction("index", "home");
        }


        public IActionResult Register()
        {

            return View();
        }

        [HttpPost]
        public IActionResult Register(User user)
        {

            _acc.CreateUser(user);

            return RedirectToAction("Index", "Home");
        }


        private void Set(string key, string value)
        {
            CookieOptions option = new CookieOptions
            {
                Expires = DateTime.Now.AddMinutes(60)
            };
            Response.Cookies.Append(key, value, option);
        }

        public override void OnActionExecuted(ActionExecutedContext context)
        {
            try
            {
                base.OnActionExecuted(context);
                var _activeuser = JsonConvert.DeserializeObject<User>(_httpContext.HttpContext.Request.Cookies["login_user"]);
                ViewBag.activeUser = _activeuser;

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}


