using FluentValidation;
using MarketplaceAPI.ViewModels.Items;

namespace MarketplaceAPI.Validators.Items
{
    public class UpdateItemValidator : AbstractValidator<UpdateItemVM>
    {
        public UpdateItemValidator()
        {
            RuleFor(ui => ui.Name)
                .NotNull()
                .NotEmpty()
                .Length(1, 100);
            RuleFor(ui => ui.Description)
                .NotNull()
                .NotEmpty();
            RuleFor(ui => ui.Metadata)
                .NotNull()
                .NotEmpty();
        }
    }
}
