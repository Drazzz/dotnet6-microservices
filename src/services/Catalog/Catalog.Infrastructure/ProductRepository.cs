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


        public async Task CreateProduct(Product product, CancellationToken cancellationToken = default)
        {
            ArgumentNullException.ThrowIfNull(product, nameof(product));

            await _context.Products.AddAsync(product, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
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
            => _context.Products.FirstOrDefaultAsync(p => p.Id == id, cancellationToken);

        public async Task<IReadOnlyCollection<Product>> GetProductsByCategory(CategoryType category, CancellationToken cancellationToken = default)
            => (await _context.Products.Where(p => p.Category == category).ToListAsync(cancellationToken)).AsReadOnly();

        public async Task<IReadOnlyCollection<Product>> GetProductByName(string name, CancellationToken cancellationToken = default)
            => (await _context.Products.Where(p => p.Name == name).ToListAsync(cancellationToken)).AsReadOnly();

        public async Task<IReadOnlyCollection<Product>> GetProducts(CancellationToken cancellationToken = default)
            => (await _context.Products.ToListAsync(cancellationToken)).AsReadOnly();

        public async Task<bool> UpdateProduct(Product product, CancellationToken cancellationToken = default)
        {
            ArgumentNullException.ThrowIfNull(product, nameof(product));

            var productToUpdate = await _context.Products.SingleOrDefaultAsync(p => p.Id == product.Id, cancellationToken);
            if (productToUpdate is null)
                throw new ArgumentException($"Product with Id = {product.Id} doesn't exist", nameof(product));

            var updated = Product.From(productToUpdate);
            _context.Products.Update(updated);

            return (await _context.SaveChangesAsync(cancellationToken)) > 0;
        }
    }
}
