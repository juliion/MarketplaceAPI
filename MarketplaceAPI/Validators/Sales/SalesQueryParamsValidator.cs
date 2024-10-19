using FluentValidation;
using MarketplaceBLL.DTOs.Sales.Requests;
using MarketplaceDAL.Entities;

namespace MarketplaceAPI.Validators.Sales
{
    public class SalesQueryParamsValidator : AbstractValidator<SalesQueryParams>
    {
        public SalesQueryParamsValidator()
        {
            RuleFor(x => x.Limit)
                .GreaterThan(0)
                .LessThanOrEqualTo(100);

            RuleFor(x => x.Skip)
                .GreaterThanOrEqualTo(0);

            RuleFor(param => param.SortColumn)
                .Must(BeValidSortColumn);

            RuleFor(param => param.Order)
                .Must(order => order.ToLower() == "asc" || order.ToLower() == "desc");

            RuleFor(x => x.Status)
                .IsInEnum()
                .When(x => x.Status.HasValue);
        }

        private bool BeValidSortColumn(string sortColumn)
        {
            var normalizedSaleProps = typeof(Sale).GetProperties()
                                .Select(p => p.Name.ToLower())
                                .ToList();
            return normalizedSaleProps.Contains(sortColumn.ToLower());
        }
    }
}
