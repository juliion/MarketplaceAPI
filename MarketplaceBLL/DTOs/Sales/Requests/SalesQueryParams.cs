using MarketplaceDAL.Enums;

namespace MarketplaceBLL.DTOs.Sales.Requests
{
    public class SalesQueryParams : QueryParamsRequest
    {
        public string SortColumn { get; set; } = "CreatedDt";
        public string Order { get; set; } = "desc";
        public MarketStatus? Status { get; set; }
        public string? Seller { get; set; }
        public string? ItemName { get; set; }
    }
}
