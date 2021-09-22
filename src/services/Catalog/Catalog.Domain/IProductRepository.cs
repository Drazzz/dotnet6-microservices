namespace Catalog.Domain
{
    public interface IProductRepository
    {
        Task<IReadOnlyCollection<Product>> GetProducts(CancellationToken cancellationToken = default);
        Task<Product> GetProduct(Guid id, CancellationToken cancellationToken = default);
        Task<IReadOnlyCollection<Product>> GetProductByName(string name, CancellationToken cancellationToken = default);
        Task<IReadOnlyCollection<Product>> GetProductByCategory(CategoryType category, CancellationToken cancellationToken = default);

        Task CreateProduct(Product product, CancellationToken cancellationToken = default);
        Task<bool> UpdateProduct(Product product, CancellationToken cancellationToken = default);
        Task<bool> DeleteProduct(Guid id, CancellationToken cancellationToken = default);
    }
}
