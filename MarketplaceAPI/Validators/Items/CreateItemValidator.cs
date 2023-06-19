using FluentValidation;
using MarketplaceAPI.ViewModels.Items;

namespace MarketplaceAPI.Validators.Items
{
    public class CreateItemValidator : AbstractValidator<CreateItemVM>
    {
        public CreateItemValidator()
        {
            RuleFor(ci => ci.Name)
                .NotNull()
                .NotEmpty()
                .Length(1, 100);
            RuleFor(ci => ci.Description)
                .NotNull()
                .NotEmpty();
            RuleFor(ci => ci.Metadata)
                .NotNull()
                .NotEmpty();
        }
    }
}
