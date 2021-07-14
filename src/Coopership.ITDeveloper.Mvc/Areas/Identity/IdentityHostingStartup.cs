using System;
using Coopership.ITDeveloper.Mvc.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

[assembly: HostingStartup(typeof(Coopership.ITDeveloper.Mvc.Areas.Identity.IdentityHostingStartup))]
namespace Coopership.ITDeveloper.Mvc.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
                services.AddDbContext<AppicationDbContext>(options =>
                    options.UseSqlServer(
                        context.Configuration.GetConnectionString("AppicationDbContextConnection")));

                services.AddDefaultIdentity<IdentityUser>()
                    .AddEntityFrameworkStores<AppicationDbContext>();
            });
        }
    }
}