using System;
using System.Collections.Generic;

namespace CarRent.DAL.Models
{
    public partial class UserOrders
    {
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public decimal TotalCost { get; set; }
        public string Photo { get; set; }
        public string Model { get; set; }
        public string ManufacturerName { get; set; }
        public int UserId { get; set; }
    }
}
