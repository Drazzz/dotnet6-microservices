namespace Catalog.Domain
{
    public sealed record Money(string Currency, decimal Amount)
    {
        public override string ToString() => $"{Currency} {Amount:0.00}";
    }
}
