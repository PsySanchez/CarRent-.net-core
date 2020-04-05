using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarRent.WebApi.Models
{
    public class ModelCarView
    {
        public int? Id { get; set; }
        public string Model { get; set; }
        public int ManufacturerId { get; set; }
        public decimal PricePerDay { get; set; }
        public string Photo { get; set; }
    }
}
