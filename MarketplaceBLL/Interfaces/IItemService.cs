using MarketplaceBLL.DTOs.Items.Requests;
using MarketplaceBLL.DTOs.Items.Responses;

namespace MarketplaceBLL.Interfaces
{
    public interface IItemService
    {
        public Task<int> CreateItem(CreateItemDTO itemDto);
        public Task<ItemDTO> GetItem(int id);
        public Task<IEnumerable<ItemDTO>> GetItems();
        public Task<ItemDTO> UpdateItem(int id, UpdateItemDTO itemDto);
        public Task DeleteItem(int id);

    }
}
