namespace MarketplaceAPI.ViewModels.Items
{
    public class ItemVM
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public string Metadata { get; set; } = null!;
    }
}
