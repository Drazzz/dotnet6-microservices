using Catalog.API.Models;
using Catalog.Domain;
using NodaTime;

namespace Catalog.API.ApplicationServices
{
    public sealed class ProductService
    {
        private readonly IClock _clock;
        private readonly ILogger<ProductService> _logger;
        private readonly IProductRepository _productRepository;


        public ProductService(IProductRepository productRepository, ILogger<ProductService> logger, IClock clock)
        {
            ArgumentNullException.ThrowIfNull(productRepository, nameof(productRepository));        

            _productRepository = productRepository;
            _logger = logger;
            _clock = clock;
        }


        internal async Task<bool> UpdateProduct(UpdateProductCommand productUpdateCommand, CancellationToken token)
        {
            ArgumentNullException.ThrowIfNull(productUpdateCommand, nameof(productUpdateCommand));
            _logger.LogInformation($"Updatating product with id: {productUpdateCommand.Id} with using {productUpdateCommand}");

            var product = await _productRepository.GetProduct(productUpdateCommand.Id);
            if (product is null)
                return false;

            product.UpdateProduct(product.Name, productUpdateCommand.Description, productUpdateCommand.Summary, product.Price, (CategoryType)productUpdateCommand.CategoryId, _clock);

            return await _productRepository.UpdateProduct(product, token)
                .ConfigureAwait(false);
        }

        internal Task<Guid> AddNewProduct(AddNewProductCommand addNewProductCommand, CancellationToken token)
        {
            ArgumentNullException.ThrowIfNull(addNewProductCommand, nameof(addNewProductCommand));
            _logger.LogInformation($"Adding the new product {addNewProductCommand}");

            var product = Product.From(addNewProductCommand.Name, addNewProductCommand.Description, addNewProductCommand.Summary, addNewProductCommand.Currency, addNewProductCommand.Amount, addNewProductCommand.CategoryId);
            return _productRepository.CreateProduct(product, token);
        }
    }
}
