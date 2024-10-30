namespace FilterAPI.Models.Domain
{
    public class SearchHistory
    {
        public Guid Id { get; set; }
        public string Query { get; set; }
        public DateTime Timestamp { get; set; }
    }
}
