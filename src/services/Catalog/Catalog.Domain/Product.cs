
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


        private Product(string name, string description, string summary, string priceCurrency, decimal amount, short categoryId, IClock clock)
            : this(Guid.NewGuid(), name, description, summary, new Money(priceCurrency, amount), (CategoryType) categoryId, clock) { }
        private Product(Guid id, string name, string description, string summary, Money price, CategoryType category, IClock clock)
            : base(clock)
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

        public static Product From(string name, string description, string summary, string priceCurrency, decimal amount, short categoryId, IClock clock)
            => new Product(name, description, summary, priceCurrency, amount, categoryId, clock);
        public static Product From(Guid id, string name, string description, string summary, Money price, CategoryType category, IClock clock)
            => new Product(id, name, description, summary, price, category, clock);


        public void ChangeProductTextDescriptionTo(string name, string description, string summary)
        {
            if (string.IsNullOrEmpty(name))
                throw new ArgumentException($"{nameof(name).ToUpperInvariant} cannot be null or empty");
            if (string.IsNullOrEmpty(description))
                throw new ArgumentException($"{nameof(description).ToUpperInvariant} cannot be null or empty");
            if (string.IsNullOrEmpty(summary))
                throw new ArgumentException($"{nameof(summary).ToUpperInvariant} cannot be null or empty");

            Name = name;
            Description = description;
            Summary = summary;
        }

        public void ChangeProductPriceTo(Money newPrice)
        {
            ArgumentNullException.ThrowIfNull(newPrice, nameof(newPrice));
            
            Price = newPrice;
        }

        public void AssignToThe(CategoryType newCategory) => Category = newCategory;
    }
}
