using MarketplaceDAL.Enums;

namespace MarketplaceBLL.DTOs.Sales.Requests
{
    public class UpdateSaleDTO
    {
        public int ItemId { get; set; }
        public decimal Price { get; set; }
        public MarketStatus Status { get; set; }
        public string Seller { get; set; } = null!;
        public string? Buyer { get; set; }
    }
}
