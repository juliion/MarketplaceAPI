namespace MarketplaceBLL.DTOs
{
    public class PagedResponse<T>
    {
        public int Limit { get; set; }
        public int Skip { get; set; }
        public long Total { get; set; }
        public List<T> Data { get; set; } = new();
    }
}
