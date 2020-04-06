using System;
using System.Collections.Generic;

namespace CarRent.DAL.Models
{
    public partial class Cars
    {
        public int Id { get; set; }
        public string CarNumber { get; set; }
        public string Model { get; set; }
        public decimal PricePerDay { get; set; }
        public string Image { get; set; }
        public string ManufacturerName { get; set; }
    }
}
