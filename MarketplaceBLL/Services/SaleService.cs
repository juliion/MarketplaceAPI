using AutoMapper;
using MarketplaceBLL.DTOs;
using MarketplaceBLL.DTOs.Items.Requests;
using MarketplaceBLL.DTOs.Items.Responses;
using MarketplaceBLL.DTOs.Sales.Requests;
using MarketplaceBLL.DTOs.Sales.Responses;
using MarketplaceBLL.Exceptions;
using MarketplaceBLL.Interfaces;
using MarketplaceDAL;
using MarketplaceDAL.Entities;
using MarketplaceDAL.Enums;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using System.Linq.Expressions;
using System.Net.Sockets;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace MarketplaceBLL.Services
{
    public class SaleService : ISaleService
    {
        private readonly MarketDbContext _context;
        private readonly IMapper _mapper;
        private readonly ISortingHelper<Sale> _sortingHelper;
        private readonly ILogger<SaleService> _logger;

        public SaleService(MarketDbContext context, IMapper mapper, ISortingHelper<Sale> sortingHelper, ILogger<SaleService> logger)
        {
            _context = context;
            _mapper = mapper;
            _sortingHelper = sortingHelper;
            _logger = logger;
        }
        public async Task<SaleDTO> GetSale(int id)
        {
            _logger.LogInformation($"Getting sale with id: {id}");

            var sale = await _context.Sales
                .Include(s => s.Item)
                .SingleOrDefaultAsync(s => s.Id == id);
            if (sale == null)
            {
                _logger.LogWarning($"No sale found with id: {id}");
                throw new NotFoundException("Sale with the provided id does not exist");
            }

            var saleDto = _mapper.Map<Sale, SaleDTO>(sale);

            _logger.LogInformation($"Successfully fetched sale with id: {id}");

            return saleDto;
        }
        public async Task<PagedResponse<SaleDTO>> GetSales(SalesQueryParams queryParams)
        {
            _logger.LogInformation(
                "Getting sales with queryParams: " +
                $"Limit: {queryParams.Limit}, " +
                $"Skip: {queryParams.Skip}, " +
                $"SortColumn: {queryParams.SortColumn}, " +
                $"Order: {queryParams.Order}, " +
                $"Status: {queryParams.Status?.ToString() ?? "null"}, " +
                $"Seller: {queryParams.Seller ?? "null"}, " +
                $"ItemName: {queryParams.ItemName ?? "null"}"
            );

            var sales = _context.Sales
                .Include(s => s.Item)
                .AsQueryable();

            var pagedResponse = await ApplyParams(sales, queryParams);

            _logger.LogInformation("Successfully getting sales");
            
            return pagedResponse;
        }

        private async Task<PagedResponse<SaleDTO>> ApplyParams(IQueryable<Sale> query, SalesQueryParams queryParams)
        {
            if (!string.IsNullOrWhiteSpace(queryParams.SortColumn) && !string.IsNullOrWhiteSpace(queryParams.Order))
            {
                query = _sortingHelper.ApplySorting(query, queryParams.SortColumn, queryParams.Order);
            }

            if (queryParams.Status != null)
            {
                query = query.Where(s => s.Status == queryParams.Status);
            }

            if (!string.IsNullOrWhiteSpace(queryParams.Seller))
            {
                query = query.Where(s => EF.Functions.Like(s.Seller, $"%{queryParams.Seller}%"));
            }

            if (!string.IsNullOrWhiteSpace(queryParams.ItemName))
            {
                query = query.Where(s => EF.Functions.Like(s.Item.Name, $"%{queryParams.ItemName}%"));
            }

            var total = await query.CountAsync();
            var limit = queryParams.Limit;
            var skip = queryParams.Skip;

            var sales = await query
                .Skip(skip)
                .Take(limit)
                .ToListAsync();

            var pagedResponse = new PagedResponse<SaleDTO>
            {
                Limit = limit,
                Skip = skip,
                Total = total,
                Data = _mapper.Map<List<Sale>, List<SaleDTO>>(sales)
            };

            return pagedResponse;
        }

        public async Task<int> CreateSale(CreateSaleDTO saleDto)
        {
            _logger.LogInformation($"Creating sale with item id: {saleDto.ItemId}");

            var newSale = _mapper.Map<CreateSaleDTO, Sale>(saleDto);
            newSale.CreatedDt = DateTime.UtcNow;
            newSale.Status = MarketStatus.Active;

            _context.Sales.Add(newSale);
            await _context.SaveChangesAsync();

            _logger.LogInformation($"Successfully created sale with id: {newSale.Id}");

            return newSale.Id;
        }
        public async Task DeleteSale(int id)
        {
            _logger.LogInformation($"Deleting sale with id: {id}");

            var sale = await _context.Sales.FindAsync(id);
            if (sale == null)
            {
                throw new NotFoundException("Sale with the provided id does not exist");
            }

            _context.Sales.Remove(sale);
            await _context.SaveChangesAsync();

            _logger.LogInformation($"Successfully deleted sale with id: {id}");
        }
        public async Task<SaleDTO> UpdateSale(int id, UpdateSaleDTO saleDto)
        {
            _logger.LogInformation($"Updating sale with id: {id}");

            var sale = await _context.Sales.FindAsync(id);
            if (sale == null)
            {
                throw new NotFoundException("Sale with the provided id does not exist");
            }

            if (sale.Status == MarketStatus.Finished)
            {
                throw new ForbiddenException("This sale is finished, it is forbidden to change it");
            }

            sale.ItemId = saleDto.ItemId;
            sale.Price = saleDto.Price;
            sale.Seller = saleDto.Seller;
            sale.Status = saleDto.Status;
            sale.Buyer = saleDto.Buyer;

            if (saleDto.Status == MarketStatus.Finished)
            {
                sale.FinishedDt = DateTime.UtcNow;
            }

            await _context.SaveChangesAsync();

            var saleRes = _mapper.Map<Sale, SaleDTO>(sale);

            _logger.LogInformation($"Successfully updated sale with id: {id}");

            return saleRes;
        }
    }
}
