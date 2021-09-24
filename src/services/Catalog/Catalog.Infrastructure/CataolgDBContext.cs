using Catalog.Domain;
using Catalog.Infrastructure.EntityTypesConfiguration;
using Microsoft.Azure.Cosmos;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace Catalog.Infrastructure
{
    public class CataolgDBContext : DbContext
    {
        public const string Name = "CatalogDb";

        private readonly CosmosDBOptions _options;

        public virtual DbSet<Product>? Products { get; set; }


        public CataolgDBContext(IOptions<CosmosDBOptions> options)
        {
            ArgumentNullException.ThrowIfNull(options, nameof(options));

            _options = options.Value;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            => optionsBuilder.UseCosmos(
                connectionString: _options.ConnectionString,
                databaseName: Name,
                options =>
                {
                    options.Region(Regions.WestEurope);
                    options.RequestTimeout(TimeSpan.FromMinutes(1));
                })
            ;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(ProductEntityTypeConfiguration.Create());
#if DEBUG
            modelBuilder.Seed();
#endif
        }
    }
}
