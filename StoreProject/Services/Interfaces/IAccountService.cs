using StoreProject.DataContext;
using StoreProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StoreProject.Services.Interfaces
{
    public interface IAccountService
    {
        StoreDBContext Store { get; set; }

        bool GetUser(string email , string password , out User user);
        bool CreateUser(User newUser);

    }
}
