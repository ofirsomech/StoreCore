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

        bool GetUser(string email, string password, out User user);
        User GetUser(int id);
        bool CheckIfEmailExist(string email);
        bool CheckIfUserNameExist(string userName);
        bool CreateUser(User newUser);
        bool UpdateUser(User updateUser);
        User UpdateRoleUser(string guid);
        User GetUserByGuid(string guid);
    }
}
