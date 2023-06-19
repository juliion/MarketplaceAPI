using FluentValidation;
using MarketplaceAPI.ViewModels.Sales;

namespace MarketplaceAPI.Validators.Sales
{
    public class UpdateSaleValidator : AbstractValidator<UpdateSaleVM>
    {
        public UpdateSaleValidator()
        {
            RuleFor(sale => sale.ItemId)
                .NotNull();
            RuleFor(sale => sale.Price)
                .NotNull()
                .GreaterThan(0);
            RuleFor(sale => sale.Status)
                .NotNull()
                .IsInEnum();
            RuleFor(sale => sale.Seller)
                .NotEmpty()
                .NotNull()
                .Length(1, 100);
            RuleFor(sale => sale.Buyer)
                .Length(1, 100)
                .When(sale => !string.IsNullOrEmpty(sale.Buyer));
        }
    }
}
