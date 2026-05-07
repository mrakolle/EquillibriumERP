using FluentValidation;

namespace EquillibriumERP.Application.Services.Products;

public class CreateProductRequestValidator : AbstractValidator<CreateProductRequest>
{
    public CreateProductRequestValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .MaximumLength(100);

        RuleFor(x => x.Sku)
            .NotEmpty()
            .MaximumLength(50);

        RuleFor(x => x.Type)
            .NotEmpty()
            .Must(t => t == "Raw" || t == "Finished")
            .WithMessage("Type must be either 'Raw' or 'Finished'");
    }
}