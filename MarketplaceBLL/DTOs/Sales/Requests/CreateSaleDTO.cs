using MarketplaceDAL.Enums;

namespace MarketplaceBLL.DTOs.Sales.Requests
{
    public class CreateSaleDTO
    {
        public int ItemId { get; set; }
        public decimal Price { get; set; }
        public string Seller { get; set; } = null!;
    }
}
