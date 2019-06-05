using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace StoreProject.Models
{
    public class Product
    {
        public long Id { get; set; }

        public long? OwnerId { get; set; }
        public long? UserId { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string ShortDescription { get; set; }

        [Required]
        public string LongDescription { get; set; }

        [Required]
        public DateTime Date { get; set; }

        [Required]
        [DataType(DataType.Currency)]
        public decimal Price { get; set; }

        public byte[] Img1 { get; set; }

        public byte[] Img2 { get; set; }

        public byte[] Img3 { get; set; }

        [Required]
        public State State { get; set; }


        public virtual User Owner { get; set; }

        public virtual User User { get; set; }
        public static double GetDiscount(double price)
        {
            return price * (double)0.9;
        }
    }
}
