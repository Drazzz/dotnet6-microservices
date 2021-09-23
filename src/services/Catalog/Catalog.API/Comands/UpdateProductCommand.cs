namespace Catalog.API.Models;

public sealed class UpdateProductCommand
{
    public Guid Id { get; set; }
    public string? Description { get; set; }
    public string? Summary { get; set; }
    public int CategoryId { get; set; }
}