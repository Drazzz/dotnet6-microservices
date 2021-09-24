using FluentValidation;

namespace Catalog.API.Models;

public sealed class AddNewProductCommandValidator : AbstractValidator<AddNewProductCommand>
{
    public AddNewProductCommandValidator()
    {
        RuleFor(p => p.Name).NotEmpty();
        RuleFor(p => p.Summary).NotEmpty();
        RuleFor(p => p.Amount)
            .NotEmpty()
            .GreaterThan(0);
        RuleFor(p => p.Currency)
            .NotEmpty()
            .Length(3);
    }
}

public sealed class AddNewProductCommand
{
    public string Name { get; set; }
    public string? Description { get; set; }
    public string Summary { get; set; }
    public decimal Amount { get; set; }
    public string Currency { get; set; }
    public short CategoryId { get; set; }
}