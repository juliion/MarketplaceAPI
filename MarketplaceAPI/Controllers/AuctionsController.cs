using AutoMapper;
using FluentValidation;
using MarketplaceAPI.Validators.Items;
using MarketplaceAPI.ViewModels.Items;
using MarketplaceAPI.ViewModels.Sales;
using MarketplaceBLL.DTOs;
using MarketplaceBLL.DTOs.Items.Requests;
using MarketplaceBLL.DTOs.Items.Responses;
using MarketplaceBLL.DTOs.Sales.Requests;
using MarketplaceBLL.DTOs.Sales.Responses;
using MarketplaceBLL.Exceptions;
using MarketplaceBLL.Interfaces;
using MarketplaceBLL.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace MarketplaceAPI.Controllers
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("0.1")]
    public class AuctionsController : ControllerBase
    {
        private readonly ISaleService _saleService;
        private IValidator<CreateSaleVM> _createSaleValidator;
        private IValidator<UpdateSaleVM> _updateSaleValidator;
        private readonly IMapper _mapper;

        public AuctionsController(ISaleService saleService, IValidator<CreateSaleVM> createSaleValidator, IValidator<UpdateSaleVM> updateSaleValidator, IMapper mapper)
        {
            _saleService = saleService;
            _createSaleValidator = createSaleValidator;
            _updateSaleValidator = updateSaleValidator;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetSales([FromQuery]SalesQueryParams queryParams)
        {
            var pagedSales = await _saleService.GetSales(queryParams);

            var pagedSalesVM = _mapper.Map<PagedResponse<SaleDTO>, PagedResponse<SaleVM>>(pagedSales);

            return Ok(pagedSalesVM);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> SaleDetails(int id)
        {
            try
            {
                var sale = await _saleService.GetSale(id);

                var saleVM = _mapper.Map<SaleDTO, SaleVM>(sale);

                return Ok(saleVM);
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPost("CreateSale")]
        public async Task<IActionResult> CreateSale(CreateSaleVM saleVM)
        {
            var validationRes = _createSaleValidator.Validate(saleVM);
            if (!validationRes.IsValid)
                return BadRequest(validationRes);

            var saleDto = _mapper.Map<CreateSaleVM, CreateSaleDTO>(saleVM);

            var saleId = await _saleService.CreateSale(saleDto);
            return Ok(saleId);
        }

        [HttpPut("UpdateSale/{id}")]
        public async Task<IActionResult> UpdateSale(int id, UpdateSaleVM saleVM)
        {
            var validationRes = _updateSaleValidator.Validate(saleVM);
            if (!validationRes.IsValid)
                return BadRequest(validationRes);
            try
            {
                var saleDto = _mapper.Map<UpdateSaleVM, UpdateSaleDTO>(saleVM);

                var updatedSale = await _saleService.UpdateSale(id, saleDto);

                return Ok(updatedSale);
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (ForbiddenException ex)
            {
                return StatusCode(StatusCodes.Status403Forbidden, ex.Message);
            }
        }

        [HttpDelete("DeleteSale/{id}")]
        public async Task<IActionResult> DeleteSale(int id)
        {
            try
            {
                await _saleService.DeleteSale(id);

                return NoContent();
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
