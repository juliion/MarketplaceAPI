using Microsoft.AspNetCore.Mvc;
using MarketplaceBLL.Interfaces;
using FluentValidation;
using MarketplaceAPI.ViewModels.Items;
using AutoMapper;
using MarketplaceBLL.DTOs.Items.Requests;
using MarketplaceBLL.DTOs.Items.Responses;
using MarketplaceDAL.Entities;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using System.Data;
using MarketplaceBLL.Exceptions;

namespace MarketplaceAPI.Controllers
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("0.1")]
    public class ItemsController : ControllerBase
    {
        private readonly IItemService _itemService;
        private IValidator<CreateItemVM> _createItemValidator;
        private IValidator<UpdateItemVM> _updateItemValidator;
        private readonly IMapper _mapper;

        public ItemsController(IItemService itemService, IValidator<CreateItemVM> createItemValidator, IValidator<UpdateItemVM> updateItemValidator, IMapper mapper)
        {
            _itemService = itemService;
            _createItemValidator = createItemValidator;
            _updateItemValidator = updateItemValidator;
            _mapper = mapper;
        }

        [HttpPost("CreateItem")]
        public async Task<IActionResult> CreateItem(CreateItemVM itemVM)
        {
            var validationRes = _createItemValidator.Validate(itemVM);
            if (!validationRes.IsValid)
                return BadRequest(validationRes);

            var itemDto = _mapper.Map<CreateItemVM, CreateItemDTO>(itemVM);

            var itemId = await _itemService.CreateItem(itemDto);

            return Ok(itemId);
        }

        [HttpPut("UpdateItem/{id}")]
        public async Task<IActionResult> UpdateItem(int id, UpdateItemVM itemVM)
        {
            var validationRes = _updateItemValidator.Validate(itemVM);
            if (!validationRes.IsValid)
                return BadRequest(validationRes);
            try
            {
                var itemDto = _mapper.Map<UpdateItemVM, UpdateItemDTO>(itemVM);

                var updatedItem = await _itemService.UpdateItem(id, itemDto);

                return Ok(updatedItem);
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpDelete("DeleteItem/{id}")]
        public async Task<IActionResult> DeleteItem(int id)
        {
            try
            {
                await _itemService.DeleteItem(id);

                return NoContent();
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpGet("GetItems")]
        public async Task<IActionResult> GetItems()
        {
            var items = await _itemService.GetItems();

            var itemsVM = _mapper.Map<IEnumerable<ItemDTO>, IEnumerable<ItemVM>>(items);

            return Ok(itemsVM);
        }

        [HttpGet("ItemDetails/{id}")]
        public async Task<IActionResult> ItemDetails(int id)
        {
            try
            {
                var item = await _itemService.GetItem(id);

                var itemVM = _mapper.Map<ItemDTO, ItemVM>(item);

                return Ok(itemVM);
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

    }
}
