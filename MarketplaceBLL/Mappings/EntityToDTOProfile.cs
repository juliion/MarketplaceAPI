using AutoMapper;
using MarketplaceBLL.DTOs.Items.Requests;
using MarketplaceBLL.DTOs.Items.Responses;
using MarketplaceBLL.DTOs.Sales.Requests;
using MarketplaceBLL.DTOs.Sales.Responses;
using MarketplaceDAL.Entities;

namespace MarketplaceBLL.Mappings
{
    public class EntityToDTOProfile : Profile
    {
        public EntityToDTOProfile()
        {
            CreateMap<CreateItemDTO, Item>();
            CreateMap<Item, ItemDTO>();

            CreateMap<CreateSaleDTO, Sale>();
            CreateMap<Sale, SaleDTO>()
                .ForMember(dest => dest.ItemName, opt => opt.MapFrom(src => src.Item.Name));
        }
    }
}
