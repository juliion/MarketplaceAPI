namespace MarketplaceBLL.DTOs
{
    public class QueryParamsRequest
    {
        public int Limit { get; set; } = 10;
        public int Skip { get; set; } = 0;
    }
}
