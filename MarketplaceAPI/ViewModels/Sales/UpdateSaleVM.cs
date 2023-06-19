using MarketplaceDAL.Enums;

namespace MarketplaceAPI.ViewModels.Sales
{
    public class UpdateSaleVM
    {
        public int ItemId { get; set; }
        public decimal Price { get; set; }
        public MarketStatus Status { get; set; }
        public string Seller { get; set; } = null!;
        public string? Buyer { get; set; }
    }
}
