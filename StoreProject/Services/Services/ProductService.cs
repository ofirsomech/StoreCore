using StoreProject.DataContext;
using StoreProject.Models;
using StoreProject.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StoreProject.Services
{
    public class ProductService : IProductService
    {
        StoreDBContext _context;
        public ProductService(StoreDBContext context)
        {
            _context = context;
        }

        public Product GetProduct(int id)
        {
           return _context.Products.FirstOrDefault(a => a.Id == id);
        }


        List<Product> IProductService.GetProducts()
        {
            return _context.Products.ToList();
        }
    }
}
