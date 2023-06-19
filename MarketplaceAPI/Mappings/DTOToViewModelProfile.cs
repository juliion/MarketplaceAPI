using AutoMapper;
using MarketplaceAPI.ViewModels.Items;
using MarketplaceAPI.ViewModels.Sales;
using MarketplaceBLL.DTOs;
using MarketplaceBLL.DTOs.Items.Requests;
using MarketplaceBLL.DTOs.Items.Responses;
using MarketplaceBLL.DTOs.Sales.Requests;
using MarketplaceBLL.DTOs.Sales.Responses;

namespace MarketplaceAPI.Mappings
{
    public class DTOToViewModelProfile : Profile
    {
        public DTOToViewModelProfile()
        {
            CreateMap<CreateItemVM, CreateItemDTO>();
            CreateMap<UpdateItemVM, UpdateItemDTO>();
            CreateMap<ItemDTO, ItemVM>();

            CreateMap<CreateSaleVM, CreateSaleDTO>();
            CreateMap<UpdateSaleVM, UpdateSaleDTO>();
            CreateMap<SaleDTO, SaleVM>();
                
            
            CreateMap<PagedResponse<SaleDTO>, PagedResponse<SaleVM>>();
        }
    }
}
