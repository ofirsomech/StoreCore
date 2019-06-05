using StoreProject.Models;
using System.Collections.Generic;

namespace StoreProject.Services.Interfaces
{
    public interface IProductService
    {
        List<Product> GetProducts();

        Product GetProduct(int id);


    }
}