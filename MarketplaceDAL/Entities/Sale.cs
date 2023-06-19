using MarketplaceDAL.Enums;

namespace MarketplaceDAL.Entities
{
    public class Sale
    {
        public int Id { get; set; }
        public int ItemId { get; set; }
        public DateTime CreatedDt { get; set; }
        public DateTime? FinishedDt { get; set; }
        public decimal Price { get; set; }
        public MarketStatus Status { get; set; }
        public string Seller { get; set; } = null!;
        public string? Buyer { get; set; }

        public Item Item { get; set; } = null!;
    }
}
