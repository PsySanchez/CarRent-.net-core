using System;
using System.Collections.Generic;

namespace CarRent.DAL.Models
{
    public partial class ContactUsMessage
    {
        public int ContactUsId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Message { get; set; }
        public string Subject { get; set; }
    }
}
