using MarketplaceBLL.DTOs;
using MarketplaceBLL.DTOs.Sales.Requests;
using MarketplaceBLL.DTOs.Sales.Responses;

namespace MarketplaceBLL.Interfaces
{
    public interface ISaleService
    {
        public Task<int> CreateSale(CreateSaleDTO saleDto);
        public Task<SaleDTO> GetSale(int id);
        public Task<PagedResponse<SaleDTO>> GetSales(SalesQueryParams queryParams);
        public Task<SaleDTO> UpdateSale(int id, UpdateSaleDTO saleDto);
        public Task DeleteSale(int id);
    }
}
