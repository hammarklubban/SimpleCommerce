using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SimpleCommerce.Data.Entities;

namespace SimpleCommerce.Data.EntityTypeConfigurations
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        private const string adminUserId = "B22698B8-42A2-4115-9631-1C2D1E2AC5F7";

        public void Configure(EntityTypeBuilder<Product> builder)
        {
            
            for (int i = 0; i < 1000; i++)
            {
                Random random = new Random(i);
                decimal randomPrice = random.Next(2000,99900)/100M;

                var user = new Product
                {
                    Id = Guid.NewGuid(),
                    Name = $"Product1_{i:000}",
                    Description = $"Description_{i:000}",
                    Price = randomPrice
                };

                builder.HasData(user);
            }
        }
    }
}
