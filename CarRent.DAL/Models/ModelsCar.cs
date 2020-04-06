using System;
using System.Collections.Generic;

namespace CarRent.DAL.Models
{
    public partial class ModelsCar
    {
        public ModelsCar()
        {
            CompanyFleet = new HashSet<CompanyFleet>();
        }

        public int Id { get; set; }
        public int ManufacturerId { get; set; }
        public string Model { get; set; }
        public decimal PricePerDay { get; set; }
        public string Image { get; set; }

        public virtual ManufacturersCar Manufacturer { get; set; }
        public virtual ICollection<CompanyFleet> CompanyFleet { get; set; }
    }
}
