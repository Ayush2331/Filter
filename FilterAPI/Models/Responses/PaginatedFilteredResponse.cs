namespace FilterAPI.Models.Responses
{
    public class PaginatedFilteredResponse
    {
        public string Name { get; set; }
        public string Type { get; set; }
        public int StockQuantity { get; set; }
        public DateOnly MfgDate { get; set; }
    }
}
