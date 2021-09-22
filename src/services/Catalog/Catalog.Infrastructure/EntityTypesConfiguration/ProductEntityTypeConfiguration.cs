using Catalog.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Catalog.Infrastructure.EntityTypesConfiguration
{
    internal sealed class ProductEntityTypeConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder
                .UseETagConcurrency()
                .ToContainer("Products")
                ;

            builder
                .HasKey(p => p.Id)
                ;

            builder
                .Property(p => p.Summary)
                .IsRequired()
                ;

            builder
                .Property(p => p.Category)
                .HasConversion<string>()
                .HasMaxLength(10)
                .IsRequired()               
            ;

            builder
                .OwnsOne(
                    p => p.Price,
                    pr =>
                    {
                        pr.ToJsonProperty("Price");
                        pr.WithOwner()
                            .HasForeignKey("ProductId");

                        pr.Property(x => x.Currency)
                            .HasMaxLength(1)
                            .IsRequired();
                        pr.Property(x => x.Amount)
                            .IsRequired();
                    })
                ;
        }
        
        public static ProductEntityTypeConfiguration Create() => new();
    }
}
