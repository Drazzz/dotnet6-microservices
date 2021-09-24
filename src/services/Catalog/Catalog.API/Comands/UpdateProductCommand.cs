using FluentValidation;

namespace Catalog.API.Models;

public sealed class UpdateProductCommandValidator : AbstractValidator<UpdateProductCommand>
{
    public UpdateProductCommandValidator()
    {
        RuleFor(p => p.Id).NotEmpty();
        RuleFor(p => p.CategoryId)
            .GreaterThanOrEqualTo(1)
            .LessThanOrEqualTo(5)
            ;
    }
}

public sealed class UpdateProductCommand
{
    public Guid Id { get; set; }
    public string? Description { get; set; }
    public string? Summary { get; set; }
    public int CategoryId { get; set; }
}