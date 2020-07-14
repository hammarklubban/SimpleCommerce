using System;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SimpleCommerce.Areas.Identity.Data;

namespace SimpleCommerce.Data.EntityTypeConfigurations
{
    public class UserConfiguration : IEntityTypeConfiguration<SimpleCommerceUser>
    {
        private const string adminUserId = "B22698B8-42A2-4115-9631-1C2D1E2AC5F7";

        public void Configure(EntityTypeBuilder<SimpleCommerceUser> builder)
        {
            var hasher = new PasswordHasher<SimpleCommerceUser>();
            var user = new SimpleCommerceUser
            {
                Id = adminUserId,
                UserName = "Admin",
                NormalizedUserName = "ADMIN@SIMPLECOMMERCE.COM",
                Email = "admin@simplecommerce.com",
                NormalizedEmail = "ADMIN@SIMPLECOMMERCE.COM",
                EmailConfirmed = true,
                LockoutEnabled = false,
                SecurityStamp = Guid.NewGuid().ToString()
            };

            user.PasswordHash = hasher.HashPassword(user, "Supersecr3t");
            builder.HasData(user);
        }
    }
}
