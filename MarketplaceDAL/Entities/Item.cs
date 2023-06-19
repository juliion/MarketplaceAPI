namespace MarketplaceDAL.Entities
{
    public class Item
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public string Metadata { get; set; } = null!;

        public List<Sale> Sales { get; set; } = new();
    }
}
