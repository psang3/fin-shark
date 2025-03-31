namespace api.Helpers
{
    public class QueryObject
    {
        public string? Symbol { get; set; }
        public string? CompanyName { get; set; }
        public string? SortBy { get; set; }
        public bool IsDecsending { get; set; }
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 20;
    }

    public class CommentQueryObject : QueryObject
    {
        public string? StockId { get; set; }
    }
} 