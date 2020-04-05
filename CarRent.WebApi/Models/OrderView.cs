using System;

namespace CarRent.WebApi.Models
{
    public class OrderView
    {
        public int? Id { get; set; }
        public int UserId { get; set; }
        public int CarId { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public decimal TotalCost { get; set; }
    }
}
