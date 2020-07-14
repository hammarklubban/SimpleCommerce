using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SimpleCommerce.Areas.Identity.Data;
using SimpleCommerce.Data.Entities;
using SimpleCommerce.Data.EntityTypeConfigurations;

namespace SimpleCommerce.Data
{
    public class ApplicationContext : IdentityDbContext<SimpleCommerceUser>
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyConfiguration(new RoleConfiguration());
            builder.ApplyConfiguration(new UserConfiguration());
            builder.ApplyConfiguration(new UsersWithRolesConfiguration());
            builder.ApplyConfiguration(new ProductConfiguration());
        }

        public DbSet<Product> Product { get; set; }
    }
}
