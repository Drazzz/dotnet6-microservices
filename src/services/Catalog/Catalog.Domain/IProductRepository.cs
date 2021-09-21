namespace Catalog.Domain
{
    public interface IProductRepository
    {
        Task<IEnumerable<Product>> GetProducts(CancellationToken cancellationToken = default);
        Task<Product> GetProduct(Guid id, CancellationToken cancellationToken = default);
        Task<IEnumerable<Product>> GetProductByName(string name, CancellationToken cancellationToken = default);
        Task<IEnumerable<Product>> GetProductByCategory(string categoryName, CancellationToken cancellationToken = default);

        Task CreateProduct(Product product, CancellationToken cancellationToken = default);
        Task<bool> UpdateProduct(Product product, CancellationToken cancellationToken = default);
        Task<bool> DeleteProduct(Guid id, CancellationToken cancellationToken = default);
    }
}
