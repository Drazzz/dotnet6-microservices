namespace Catalog.Infrastructure
{
    public sealed class CosmosDBOptions
    {
        public const string SectionName = "CosmosDB";

        public string? Url { get; set; }
        public string? PrimaryKey { get; set; }
        public string? ConnectionString { get; set; }
    }
}
