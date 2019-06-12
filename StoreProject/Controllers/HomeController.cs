using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using StoreProject.DataContext;
using StoreProject.Models;
using Microsoft.Extensions.FileProviders;
using System.IO;
using StoreProject.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Mvc.Filters;
using StoreProject.Tools;
using Microsoft.EntityFrameworkCore;
using StoreProject.Services.Interfaces;

namespace StoreProject.Controllers
{

    public class HomeController : Controller
    {
        IProductService _products;
        IAccountService _accounts;
        private readonly IHttpContextAccessor _httpContext;
        StoreDBContext _context;


        public HomeController(IProductService products, StoreDBContext context, IHttpContextAccessor httpContext, IAccountService accounts)
        {
            _products = products;
            _context = context;
            _httpContext = httpContext;
            _accounts = accounts;
            ViewBag.activeUser = httpContext.HttpContext.Request.Cookies["login_user"];

        }

        public PartialViewResult ProductList()
        {
            List<Product> products = new List<Product>();

            products = _context.Products.Include("User").ToList();
            return PartialView("_ProductsPartialView", products);

        }

        public async Task<IActionResult> Index(string sortOrder)
        {
            try
            {
                var cart = SessionHelper.GetObjectFromJson<List<Product>>(HttpContext.Session, CartController.getKey());
                if (cart == null)
                {
                    var productsCheck = _products.GetProducts().FindAll(p => p.State == State.InCart);
                    productsCheck.ForEach(p => p.State = State.InStore);
                    _context.SaveChanges();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            ViewData["NameSortParm"] = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewData["DateSortParm"] = sortOrder == "Date" ? "date_desc" : "Date";
            var products = from s in _context.Products
                           select s;
            switch (sortOrder)
            {
                case "name_desc":
                    products = products.OrderByDescending(s => s.Title);
                    break;
                case "Date":
                    products = products.OrderBy(s => s.Date);
                    break;
                case "date_desc":
                    products = products.OrderByDescending(s => s.Date);
                    break;
                default:
                    products = products.OrderBy(s => s.Title);
                    break;
            }
            return View(await products.AsNoTracking().ToListAsync());
        }


        public IActionResult Create()
        {
            Product product = new Product();
            return View(product);
        }


        [HttpPost]
        public async Task<IActionResult> Create(Product product, IFormFile img1, IFormFile img2, IFormFile img3)
        {
            using (var memoryStream = new MemoryStream())
            {
                if (img1 != null)
                {
                    await img1.CopyToAsync(memoryStream);
                    product.Img1 = memoryStream.ToArray();
                }
                else
                {

                    byte[] imageArray = System.IO.File.ReadAllBytes(@"D:\VisualStudioProjects\source\StoreProject\StoreProject\wwwroot\images\480px-No_image_available.svg.png");
                    product.Img1 = imageArray;
                }
            }
            using (var memoryStream = new MemoryStream())
            {
                if (img2 != null)
                {
                    await img2.CopyToAsync(memoryStream);
                    product.Img2 = memoryStream.ToArray();
                }
                else
                {
                    byte[] imageArray = System.IO.File.ReadAllBytes(@"D:\VisualStudioProjects\source\StoreProject\StoreProject\wwwroot\images\480px-No_image_available.svg.png");
                    product.Img2 = imageArray;
                }
            }
            using (var memoryStream = new MemoryStream())
            {
                if (img3 != null)
                {
                    await img3.CopyToAsync(memoryStream);
                    product.Img3 = memoryStream.ToArray();
                }
                else
                {
                    byte[] imageArray = System.IO.File.ReadAllBytes(@"D:\VisualStudioProjects\source\StoreProject\StoreProject\wwwroot\images\480px-No_image_available.svg.png");
                    product.Img3 = imageArray;
                }
            }
            string productJson = Request.Cookies["login_user"];

            User userJson = JsonConvert.DeserializeObject<User>(productJson);


            product.State = State.InStore;
            product.Date = DateTime.Now;
            product.OwnerId = userJson.Id;
            _context.Products.Add(product);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Info(int id)
        {
            var product = _products.GetProduct(id);

            return View(product);
        }

        public IActionResult MyProducts(int id)
        {

            var user = _accounts.GetUser(id);
            var products = _products.GetProducts();
            var myProducts = products.FindAll(p => p.OwnerId == id);

            return View(myProducts);
        }



        public IActionResult Delete(int id)
        {
            var product = _products.GetProduct(id);
            ViewBag.productDel = product;

            product.State = State.Sold;
            _context.SaveChanges();
            return RedirectToAction("Index", "Home");
        }

        public IActionResult About()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
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
