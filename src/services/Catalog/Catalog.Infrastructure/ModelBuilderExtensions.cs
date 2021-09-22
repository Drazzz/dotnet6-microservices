using Catalog.Domain;
using Microsoft.EntityFrameworkCore;

namespace Catalog.Infrastructure
{
    internal static class ModelBuilderExtensions
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>(p =>
            {
                var p1Id = Guid.NewGuid();
                var p2Id = Guid.NewGuid();                   

                p.HasData(new
                {
                    Id = p1Id,
                    Name = "Product1",
                    Description = "This is product1 description",
                    Summary = "This is product1 summary",
                    Category = CategoryType.Smartphone,
                    CreatedDate = DateTime.UtcNow
                }, new
                {
                    Id = p2Id,
                    Name = "Product2",
                    Description = "This is product2 description",
                    Summary = "This is product2 summary",
                    Category = CategoryType.SmartWatch,
                    CreatedDate = DateTime.UtcNow
                });
                p.OwnsOne(p => p.Price)
                    .HasData(new
                    {
                        ProductId = p1Id,
                        Currency = "PLN",
                        Amount = 2m
                    }, new
                    {
                        ProductId = p2Id,
                        Currency = "USD",
                        Amount = 51m
                    });
            });
        }
    }
}
