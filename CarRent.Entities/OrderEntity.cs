using System;
using System.Collections.Generic;
using System.Text;

namespace CarRent.Entities
{
    public class OrderEntity
    {
        public int? Id { get; set; }
        public int UserId { get; set; }
        public int CarId { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public decimal TotalCost { get; set; }
    }
}
