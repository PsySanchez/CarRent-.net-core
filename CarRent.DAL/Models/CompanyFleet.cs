using System;
using System.Collections.Generic;

namespace CarRent.DAL.Models
{
    public partial class CompanyFleet
    {
        public CompanyFleet()
        {
            Orders = new HashSet<Orders>();
        }

        public int Id { get; set; }
        public int ModelId { get; set; }
        public string CarNumber { get; set; }

        public virtual ModelsCar Model { get; set; }
        public virtual ICollection<Orders> Orders { get; set; }
    }
}
