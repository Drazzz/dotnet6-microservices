using BuildingBlocks.Domain;
using NodaTime;

namespace Catalog.Domain
{
    public sealed class Product : AuditedEntity
    {
        public string Name { get; private set; }
        public string Description { get; private set; }
        public string Summary { get; private set; }
        public Money Price { get; private set; }
        public CategoryType Category { get; private set; }


        //for EF Core
        private Product() { }
        private Product(string name, string description, string summary, string priceCurrency, decimal amount, short categoryId)
            : this(Guid.NewGuid(), name, description, summary, new Money(priceCurrency, amount), (CategoryType) categoryId) { }
        private Product(Guid id, string name, string description, string summary, Money price, CategoryType category)
        {
            ArgumentNullException.ThrowIfNull(price, nameof(price));

            if (id == Guid.Empty)
                throw new ArgumentException($"{nameof(id).ToUpperInvariant} cannot be null or empty");
            if(string.IsNullOrEmpty(name))
                throw new ArgumentException($"{nameof(name).ToUpperInvariant} cannot be null or empty");
            if (string.IsNullOrEmpty(description))
                throw new ArgumentException($"{nameof(description).ToUpperInvariant} cannot be null or empty");
            if (string.IsNullOrEmpty(summary))
                throw new ArgumentException($"{nameof(summary).ToUpperInvariant} cannot be null or empty");

            Id = id;
            Name = name;
            Description = description;
            Summary = summary;
            Price = price;
            Category = category;
        }

        public static Product From(string name, string description, string summary, string priceCurrency, decimal amount, short categoryId)
            => new Product(name, description, summary, priceCurrency, amount, categoryId);
        public static Product From(Guid id, string name, string description, string summary, Money price, CategoryType category)
            => new Product(id, name, description, summary, price, category);
        public static Product From(Product product)
            => new Product(product.Id, product.Name, product.Description, product.Summary, product.Price, product.Category);


        public void UpdateProduct(string name, string description, string summary, Money price, CategoryType category, IClock clock)
        {
            ArgumentNullException.ThrowIfNull(price, nameof(price));
            ArgumentNullException.ThrowIfNull(clock, nameof(clock));

            if (string.IsNullOrEmpty(name))
                throw new ArgumentException($"{nameof(name).ToUpperInvariant} cannot be null or empty");
            if (string.IsNullOrEmpty(description))
                throw new ArgumentException($"{nameof(description).ToUpperInvariant} cannot be null or empty");
            if (string.IsNullOrEmpty(summary))
                throw new ArgumentException($"{nameof(summary).ToUpperInvariant} cannot be null or empty");
            
            Name = name;
            Description = description;
            Summary = summary;
            Price = price;
            Category = category;
            LastModifiedDate = clock.GetCurrentInstant().ToDateTimeUtc();
        }
    }
}
