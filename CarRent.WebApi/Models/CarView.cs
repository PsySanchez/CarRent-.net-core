using System.ComponentModel.DataAnnotations;


namespace CarRent.WebApi.Models
{
    public class CarView
    {
        public int? Id { get; set; }
        [Required]
        public string Image { get; set; }
        [Required]
        public string Model { get; set; }
        [Required]
        public decimal PricePerDay { get; set; }
        [Required]
        [StringLength(7, MinimumLength = 7)]
        public string CarNumber { get; set; }
        [Required]
        public string Manufacturer { get; set; }
    }
}
