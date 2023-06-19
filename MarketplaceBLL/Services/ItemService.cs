using AutoMapper;
using MarketplaceBLL.DTOs.Items.Requests;
using MarketplaceBLL.DTOs.Items.Responses;
using MarketplaceBLL.Exceptions;
using MarketplaceBLL.Interfaces;
using MarketplaceDAL;
using MarketplaceDAL.Entities;
using Microsoft.EntityFrameworkCore;
using System.Net.Sockets;

namespace MarketplaceBLL.Services
{
    public class ItemService : IItemService
    {
        private readonly MarketDbContext _context;
        private readonly IMapper _mapper;

        public ItemService(MarketDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<int> CreateItem(CreateItemDTO itemDto)
        {
            var newItem = _mapper.Map<CreateItemDTO, Item>(itemDto);

            _context.Items.Add(newItem);
            await _context.SaveChangesAsync();

            return newItem.Id;
        }

        public async Task DeleteItem(int id)
        {
            var item = await _context.Items.FindAsync(id);
            if (item == null)
            {
                throw new NotFoundException("Item with the provided id does not exist");
            }

            _context.Items.Remove(item);
            await _context.SaveChangesAsync();
        }

        public async Task<ItemDTO> GetItem(int id)
        {
            var item = await _context.Items.FindAsync(id);
            if (item == null)
            {
                throw new NotFoundException("Item with the provided id does not exist");
            }

            var itemDto = _mapper.Map<Item, ItemDTO>(item);

            return itemDto;
        }

        public async Task<IEnumerable<ItemDTO>> GetItems()
        {
            var items = await _context.Items.ToListAsync();
            var itemsDTOs = _mapper.Map<List<Item>, List<ItemDTO>>(items);

            return itemsDTOs;
        }

        public async Task<ItemDTO> UpdateItem(int id, UpdateItemDTO itemDto)
        {
            var item = await _context.Items.FindAsync(id);
            if (item == null)
            {
                throw new NotFoundException("Item with the provided id does not exist");
            }

            item.Name = itemDto.Name;
            item.Description = itemDto.Description;
            item.Metadata = itemDto.Metadata;

            await _context.SaveChangesAsync();

            var itemRes = _mapper.Map<Item, ItemDTO>(item);

            return itemRes;
        }
    }
}
