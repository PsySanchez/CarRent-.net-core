using System;
using System.Collections.Generic;

namespace CarRent.DAL.Models
{
    public partial class Orders
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int CarId { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public decimal TotalCost { get; set; }

        public virtual CompanyFleet Car { get; set; }
        public virtual Users User { get; set; }
    }
}
