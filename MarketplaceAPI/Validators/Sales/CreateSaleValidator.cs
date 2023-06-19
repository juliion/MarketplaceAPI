using FluentValidation;
using MarketplaceAPI.ViewModels.Sales;

namespace MarketplaceAPI.Validators.Sales
{
    public class CreateSaleValidator : AbstractValidator<CreateSaleVM>
    {
        public CreateSaleValidator()
        {
            RuleFor(sale => sale.ItemId)
                .NotNull();
            RuleFor(sale => sale.Price)
                .NotNull()
                .GreaterThan(0);
            RuleFor(sale => sale.Seller)
                .NotEmpty()
                .NotNull()
                .Length(1, 100);
        }
    }
}
