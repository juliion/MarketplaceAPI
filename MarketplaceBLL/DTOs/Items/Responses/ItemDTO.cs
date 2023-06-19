namespace MarketplaceBLL.DTOs.Items.Responses
{
    public class ItemDTO
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public string Metadata { get; set; } = null!;
    }
}
