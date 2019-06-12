using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace StoreProject.Models
{
    public class User
    {
        public long Id { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime BirthDate { get; set; } = DateTime.Now;

        public DateTime SignupDate { get; set; } = DateTime.Now;

        [Required]
        public string Email { get; set; }

        [Required]
        public string UserName { get; set; }

        public Role Role { get; set; } = Role.NotVerify;

        public Guid Guid { get; set; } = Guid.NewGuid();

        [Required]
        [StringLength(255, MinimumLength = 6)]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [NotMapped]
        [Compare("Password")]
        [Required]
        public string RePassword { get; set; }

        public string GetFullName()
        {
            return FirstName + " " + LastName;
        }
    }
}
