using Coopership.ITDeveloper.Data.ORM;
using Coopership.ITDeveloper.Mvc.Extentions.Filters;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Coopership.ITDeveloper.Mvc.Configuration
{
    public static class MvcAndRazorConfig
    {
        public static IServiceCollection AddMvcAndRazor(this IServiceCollection services)
        {


            services.AddMvc(options =>
            {
                options.Filters.Add(typeof(AuditoriaILoggerFilter));
                // Todo: Add >> AutoValidateAntiforgeryTokenAttribute

            }).SetCompatibilityVersion(CompatibilityVersion.Version_3_0);


            services.AddControllersWithViews();
            services.AddRazorPages();

            return services;
        }

    }

}