namespace Catalog.Domain
{
    public sealed record Money
    {
        // Empty constructor in this case is required by EF Core,
        // because has a complex type as a parameter in the default constructor.
        private Money() { }
        public Money(string currency, decimal amount) => (Currency, Amount) = (currency, amount);

        public decimal Amount { get; private init; }
        public string Currency { get; private init; }

        public override string ToString() => $"{Currency} {Amount:0.00}";
    }
}
