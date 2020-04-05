using System;
using System.Collections.Generic;

namespace CarRent.DAL.Models
{
    public partial class Users
    {
        public Users()
        {
            Orders = new HashSet<Orders>();
            Tokens = new HashSet<Tokens>();
        }

        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Password { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Role { get; set; }

        public virtual ICollection<Orders> Orders { get; set; }
        public virtual ICollection<Tokens> Tokens { get; set; }
    }
}
