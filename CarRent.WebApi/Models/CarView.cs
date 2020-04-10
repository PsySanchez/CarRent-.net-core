using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarRent.WebApi.Models
{
    public class CarView
    {
        public int? Id { get; set; }
        public string Image { get; set; }
        public string Model { get; set; }
        public decimal PricePerDay { get; set; }
        public string CarNumber { get; set; }
        public string Manufacturer { get; set; }
    }
}
