namespace MarketplaceBLL.DTOs.Items.Requests
{
    public class UpdateItemDTO
    {
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public string Metadata { get; set; } = null!;
    }
}
