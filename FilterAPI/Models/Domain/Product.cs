namespace FilterAPI.Models.Domain
{
    public class Product
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public int StockQuantity { get; set; }
        public DateOnly MfgDate { get; set; }
    }
}
