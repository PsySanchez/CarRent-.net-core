namespace CarRent.Entities
{
    public class CarEntity
    {
        public int? Id { get; set; }
        public string Image { get; set; }
        public string Model { get; set; }
        public decimal PricePerDay { get; set; }
        public string CarNumber { get; set; }
        public string Manufacturer { get; set; }
    }
}
