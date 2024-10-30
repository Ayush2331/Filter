namespace FilterAPI.Models.Requests
{
    public class FilterRequest
    {
        public List<string>? Name { get; set; } = null;
        public List<string>? Type { get; set; } = null;
        public List<int>? StocksPurchased { get; set; } = null;
        public DateOnly? MfgStartDate { get; set; } = null;
        public DateOnly? MfgEndDate { get; set; } = null;
        public SortRequest? SortRequest { get; set; } = null;
        public PaginationRequest? PaginationRequest { get; set; } = null;
    }

    public class SortRequest
    {
        public bool? IsRelevant { get; set; }
        public bool? IsDateAscending { get; set; }
        public bool? IsPopularAscending { get; set; }
    }

    public class PaginationRequest
    {
        public int Page { get; set; }
        public int PageSize { get; set; }
    }
}
