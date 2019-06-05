using StoreProject.DataContext;
using StoreProject.Models;
using StoreProject.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StoreProject.Services.Services
{
    public class AccountService : IAccountService
    {
        public StoreDBContext Store { get; set; }

        public AccountService(StoreDBContext store)
        {
            Store = store;
        }

        public bool GetUser(string userName, string password, out User user)
        {
            var userlog = Store.Users.FirstOrDefault(a => a.UserName == userName);
                user = userlog;
            if (userlog != null)
            {
                if (userlog.Password == Encryptor.MD5Hash(password))
                    return true;
            }
            return false;
        }

        public bool CreateUser(User newUser)
        {
            newUser.Password = Encryptor.MD5Hash(newUser.Password);

            Store.Users.Add(newUser);
            Store.SaveChanges();
            return true;
        }
    }
}
