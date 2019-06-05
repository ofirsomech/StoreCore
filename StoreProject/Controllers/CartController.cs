using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;
using StoreProject.DataContext;
using StoreProject.Models;
using StoreProject.Services.Interfaces;
using StoreProject.Tools;

namespace StoreProject.Controllers
{
    public class CartController : Controller
    {
        private readonly IProductService _products;
        private static IHttpContextAccessor _httpContext;
        private readonly StoreDBContext _context;
        double sumPrice = 0;




        public static string getKey()
        {
            return $"cart+{_httpContext.HttpContext.Request.Cookies["login_user"]}";
        }
        public CartController(IProductService products, IHttpContextAccessor httpContext, StoreDBContext context)
        {
            _products = products;
            _httpContext = httpContext;
            _context = context;

        }

        public IActionResult Index()
        {
            try
            {
            var cart = SessionHelper.GetObjectFromJson<List<Product>>(HttpContext.Session, getKey());
            sumPrice = 0;
            foreach (var item in cart)
            {
                sumPrice += (double)item.Price;
            };


            ViewBag.TotalPrice = sumPrice;
            ViewBag.cart = cart;
            return View();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return View();
            }
        }
        public IActionResult AddToCart(int id)
        {
            Product productModel = new Product();
            var changeProduct = _products.GetProduct(id);
            changeProduct.State = State.InCart;

            if (SessionHelper.GetObjectFromJson<List<Product>>(HttpContext.Session, getKey()) == null)
            {
                List<Product> cart = new List<Product>();
                cart.Add(new Product { Id = _products.GetProduct(id).Id, Title = _products.GetProduct(id).Title, Price = _products.GetProduct(id).Price, State = State.InCart });
                SessionHelper.SetObjectAsJson(HttpContext.Session, getKey(), cart);
            }
            else
            {
                List<Product> cart = SessionHelper.GetObjectFromJson<List<Product>>(HttpContext.Session, getKey());
                int index = isExist(id);

                cart.Add(new Product { Id = _products.GetProduct(id).Id, Title = _products.GetProduct(id).Title, Price = _products.GetProduct(id).Price, State = State.InCart });

                SessionHelper.SetObjectAsJson(HttpContext.Session, getKey(), cart);
            }
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult Buy(Product product)
        {
            if (product != null)
            {
                var productBuy = _products.GetProduct((int)product.Id);
                productBuy.State = State.Sold;

                List<Product> cart = SessionHelper.GetObjectFromJson<List<Product>>(HttpContext.Session, getKey());

                product = cart.FirstOrDefault(p => p.Id == product.Id);
                cart.Remove(product);
                product.State = State.Sold;
                var _activeuser = JsonConvert.DeserializeObject<User>(_httpContext.HttpContext.Request.Cookies["login_user"]);
                productBuy.UserId = _activeuser.Id;
                SessionHelper.SetObjectAsJson(HttpContext.Session, getKey(), cart);
                _context.SaveChanges();
            }
            return RedirectToAction("Index", "Home");
        }

        public IActionResult BuyAll(Product product)
        {
            try
            {
                List<Product> cart = SessionHelper.GetObjectFromJson<List<Product>>(HttpContext.Session, getKey());
                foreach (var item in cart)
                {
                    item.State = State.Sold;
                    Product productBuy = _products.GetProduct((int)item.Id);
                    productBuy.State = State.Sold;
                }

                cart.Clear();


                SessionHelper.SetObjectAsJson(HttpContext.Session, getKey(), cart);

                _context.SaveChanges();



                return RedirectToAction("Index", "Home");
            }
            catch (Exception e)
            {

                return BadRequest();

            }
        }

        public IActionResult Remove(int id)
        {
            List<Product> cart = SessionHelper.GetObjectFromJson<List<Product>>(HttpContext.Session, getKey());
            int index = isExist(id);
            cart.RemoveAt(index);
            SessionHelper.SetObjectAsJson(HttpContext.Session, getKey(), cart);
            return RedirectToAction("Index");
        }

        private int isExist(int id)
        {
            List<Product> cart = SessionHelper.GetObjectFromJson<List<Product>>(HttpContext.Session, getKey());
            for (int i = 0; i < cart.Count; i++)
            {
                if (cart[i].Id.Equals(id))
                {
                    return i;
                }
            }
            return -1;
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