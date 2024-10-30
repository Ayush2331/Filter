namespace FilterAPI.Models.Requests
{
    public class FilterRequest
    {
        public List<string>? Name { get; set; }
        public List<string>? Type { get; set; }
        public List<int>? StockQuantity { get; set; }
        public DateOnly? MfgStartDate { get; set; }
        public DateOnly? MfgEndDate { get; set; }
        public SortRequest? SortRequest { get; set; }
        public PaginationRequest? PaginationRequest { get; set; }
    }

    public class SortRequest
    {
        public bool? IsRelevantAscending { get; set; }
        public bool? IsDateAscending { get; set; }
        public bool? IsPopularAscending { get; set; }
    }

    public class PaginationRequest
    {
        public int Page { get; set; }
        public int PageSize { get; set; }
    }
}
