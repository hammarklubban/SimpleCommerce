using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SimpleCommerce.Areas.Identity.Data;
using SimpleCommerce.Data;

[assembly: HostingStartup(typeof(SimpleCommerce.Areas.Identity.IdentityHostingStartup))]
namespace SimpleCommerce.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
                services.AddDbContext<ApplicationContext>(options =>
                    options.UseSqlServer(
                        context.Configuration.GetConnectionString("IdentityContextConnection")));

                services.AddDefaultIdentity<SimpleCommerceUser>()
                    .AddEntityFrameworkStores<ApplicationContext>();
            });
        }
    }
}