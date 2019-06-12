using StoreProject.DataContext;
using StoreProject.Models;
using StoreProject.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EASendMail; //add EASendMail namespace
using Newtonsoft.Json;
using Microsoft.AspNetCore.Http;

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

        public User GetUser(int id)
        {

            try
            {
                var userlog = Store.Users.FirstOrDefault(a => a.Id == id);
                return userlog;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw;
            }
        }
        public bool CheckIfEmailExist(string email)
        {

            try
            {
                var userlog = Store.Users.FirstOrDefault(a => a.Email == email);
                if (userlog == null)
                    return false;
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw;
            }
        }
        public bool CheckIfUserNameExist(string userName)
        {

            try
            {
                var userlog = Store.Users.FirstOrDefault(a => a.UserName == userName);
                if (userlog == null)
                    return false;
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw;
            }
        }



        public bool CreateUser(User newUser)
        {

            newUser.Password = Encryptor.MD5Hash(newUser.Password);
            newUser.Guid = Guid.NewGuid();


            Store.Users.Add(newUser);
            Store.SaveChanges();
            return true;
        }

        public User UpdateRoleUser(string guid)
        {
            var user = GetUserByGuid(guid);
            if (user != null)
            {
                user.Role = Role.RegularUser;
            }
            Store.SaveChanges();

            return user;

        }

        public bool UpdateUser(User updateUser)
        {
            var user = GetUser((int)updateUser.Id);
            if (user != null)
            {
                user.BirthDate = updateUser.BirthDate;
                user.FirstName = updateUser.FirstName;
                user.LastName = updateUser.LastName;
                user.Email = updateUser.Email;
                user.Password = Encryptor.MD5Hash(updateUser.Password);
            }
            Store.SaveChanges();
            return true;

        }

        public User GetUserByGuid(string guid)
        {
            var user = Store.Users.FirstOrDefault(u => u.Guid.ToString() == guid);

            return user;
        }

    }
}
