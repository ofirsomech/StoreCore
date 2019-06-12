using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using StoreProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace StoreProject.DataContext
{
    public class StoreDBContext : DbContext
    {
        public StoreDBContext(DbContextOptions option) : base(option)
        {

        }

        public virtual DbSet<User> Users{ get; set; }
        public virtual DbSet<Product> Products{ get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            var webClient = new WebClient();


            //modelBuilder.Entity<User>().HasData(
            //    new User { Id = 1, FirstName = "Ofir", LastName = "Somech", Email = "user@gmail.com", UserName = "user@gmail.com", BirthDate = DateTime.Now, Password = "123456" },
            //    new User { Id = 2, FirstName = "Avi", LastName = "Levi", Email = "avi@gmail.com", UserName = "avi@gmail.com", BirthDate = DateTime.Now, Password = "123456" },
            //    new User { Id = 3, FirstName = "Dani", LastName = "Cohen", Email = "dani@gmail.com", UserName = "dani@gmail.com", BirthDate = DateTime.Now, Password = "123456" }
            //    );

            //modelBuilder.Entity<Product>().HasData(
            //    new Product { Id = 1, OwnerId = 3, UserId = 2, Date = DateTime.Now, LongDescription = "test", ShortDescription = "TEST1",Price = 33, Title = "title", State = State.InStore}
            //    );

            
        }




    }
}
