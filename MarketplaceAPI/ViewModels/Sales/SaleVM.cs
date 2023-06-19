using MarketplaceDAL.Entities;
using MarketplaceDAL.Enums;

namespace MarketplaceAPI.ViewModels.Sales
{
    public class SaleVM
    {
        public int Id { get; set; }
        public int ItemId { get; set; }
        public string ItemName { get; set; } = null!;
        public DateTime CreatedDt { get; set; }
        public DateTime? FinishedDt { get; set; }
        public decimal Price { get; set; }
        public MarketStatus Status { get; set; }
        public string Seller { get; set; } = null!;
        public string? Buyer { get; set; }
    }
}
