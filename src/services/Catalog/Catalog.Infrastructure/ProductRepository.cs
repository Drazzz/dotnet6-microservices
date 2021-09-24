using BuildingBlocks.Common.Extensions;
using Catalog.Domain;
using Microsoft.EntityFrameworkCore;

namespace Catalog.Infrastructure
{
    public sealed class ProductRepository : IProductRepository
    {
        private readonly CataolgDBContext _context;


        public ProductRepository(CataolgDBContext catalogDbContext)
        {
            ArgumentNullException.ThrowIfNull(catalogDbContext, nameof(catalogDbContext));

            _context = catalogDbContext;
        }

        public async Task<Guid> CreateProduct(Product product, CancellationToken cancellationToken = default)
        {
            ArgumentNullException.ThrowIfNull(product, nameof(product));

            await _context.Products.AddAsync(product, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);

            return product.Id;
        }

        public async Task<bool> DeleteProduct(Guid id, CancellationToken cancellationToken = default)
        {
            var product = await _context.Products.SingleOrDefaultAsync(p => p.Id ==id, cancellationToken);
            if (product is null)
                throw new ArgumentException($"Product with {nameof(id)} = {id} doesn't exist", nameof(id));

            _context.Remove(product);
            return (await _context.SaveChangesAsync(cancellationToken)) > 0;
        }

        public Task<Product> GetProduct(Guid id, CancellationToken cancellationToken = default)
            => _context.Products.FindAsync(cancellationToken, id).AsTask();

        public  Task<IReadOnlyCollection<Product>> GetProductsByCategory(CategoryType category, CancellationToken cancellationToken = default)
            => _context.Products.Where(p => p.Category == category).ToListAsync(cancellationToken).ToReadOnlyCollection();

        public  Task<IReadOnlyCollection<Product>> GetProductByName(string name, CancellationToken cancellationToken = default)
            => _context.Products.Where(p => p.Name == name).ToListAsync(cancellationToken).ToReadOnlyCollection();

        public Task<IReadOnlyCollection<Product>> GetProducts(CancellationToken cancellationToken = default)
            => _context.Products.ToListAsync(cancellationToken).ToReadOnlyCollection();

        public async Task<bool> UpdateProduct(Product product, CancellationToken cancellationToken = default)
        {
            ArgumentNullException.ThrowIfNull(product, nameof(product));

            var productToUpdate = await GetProduct(product.Id, cancellationToken);
            if (productToUpdate is null)
                throw new ArgumentException($"Product with Id = {product.Id} doesn't exist", nameof(product));

            var updated = Product.From(productToUpdate);
            _context.Products.Update(updated);

            return (await _context.SaveChangesAsync(cancellationToken)) > 0;
        }
    }
}
