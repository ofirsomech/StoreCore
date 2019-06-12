using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;
using StoreProject.DataContext;
using StoreProject.Models;
using StoreProject.Services.Interfaces;
using StoreProject.Services.Services;
using StoreProject.Tools;

namespace StoreProject.Controllers
{
    public class AccountController : Controller
    {
        IAccountService _acc;
        IEmailSender _email;
        StoreDBContext _context;
        private readonly IHttpContextAccessor _httpContext;

        public AccountController(IAccountService acc, IHttpContextAccessor httpContext, IEmailSender email, StoreDBContext context)
        {
            _acc = acc;
            _httpContext = httpContext;
            ViewBag.activeUser = httpContext.HttpContext.Request.Cookies["login_user"];
            _email = email;
            _context = context;
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
            if (!_acc.CheckIfEmailExist(user.Email))
            {
                //Eror if need
                if (!_acc.CheckIfUserNameExist(user.UserName))
                {
                    _acc.CreateUser(user);
                    var json = JsonConvert.SerializeObject(user);
                    Set("login_user", json);
                    _email.SendEmail(user);
                    //Eror if need
                }
            }
            //TODO EROOR!

            return RedirectToAction("Index", "Home");
        }

        public IActionResult SendEmailVerify()
        {
            var _activeuser = JsonConvert.DeserializeObject<User>(_httpContext.HttpContext.Request.Cookies["login_user"]);
            _email.SendEmail(_activeuser);
            return RedirectToAction("VerifyUser", "Account", new { _activeuser.Guid });
        }



        [HttpGet("Account/VerifyUser/{guid}", Name = "guid")]
        public IActionResult VerifyUser(string guid)
        {
            var user = _acc.GetUserByGuid(guid);


            return View(user);
        }

        [HttpPost]
        public IActionResult VerifyUser(User user)
        {
            var _activeuser = JsonConvert.DeserializeObject<User>(_httpContext.HttpContext.Request.Cookies["login_user"]);
            user = _activeuser;
            user = _acc.UpdateRoleUser(user.Guid.ToString());
            var json = JsonConvert.SerializeObject(user);
            Set("login_user", json);

            if (user.Role == Role.NotVerify)
                return RedirectToAction("VerifyUser", "Account");
            else
                return RedirectToAction("Index", "Home");
        }



        public IActionResult Update(int id)
        {
            var user = _acc.GetUser(id);


            return View(user);
        }

        [HttpPost]
        public IActionResult Update(User user)
        {
            _acc.UpdateUser(user);
            var json = JsonConvert.SerializeObject(user);
            Set("login_user", json);

            return RedirectToAction("Index", "Home");
        }


        public void Set(string key, string value)
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
                try
                {
                    var _activeuser = JsonConvert.DeserializeObject<User>(_httpContext.HttpContext.Request.Cookies["login_user"]);
                    ViewBag.activeUser = _activeuser;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
                try
                {
                    var cart = SessionHelper.GetObjectFromJson<List<Product>>(HttpContext.Session, CartController.getKey());
                    ViewBag.Cart = cart;

                }
                catch
                {
                    Console.WriteLine("error");
                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}


