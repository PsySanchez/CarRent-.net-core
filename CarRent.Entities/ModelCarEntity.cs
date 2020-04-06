namespace CarRent.Entities
{
    public class ModelCarEntity
    {
        public int? Id { get; set; }
        public string Model { get; set; }
        public int ManufacturerId { get; set; }
        public decimal PricePerDay { get; set; }
        public string Image { get; set; }
    }
}
