namespace MarketplaceAPI.ViewModels.Sales
{
    public class CreateSaleVM
    {
        public int ItemId { get; set; }
        public decimal Price { get; set; }
        public string Seller { get; set; } = null!;
    }
}
