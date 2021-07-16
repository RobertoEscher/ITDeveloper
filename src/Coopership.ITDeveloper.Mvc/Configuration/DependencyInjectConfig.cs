using Coopership.ITDeveloper.Mvc.Extentions.Filters;
using KissLog;
using KissLog.AspNetCore;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace Coopership.ITDeveloper.Mvc.Configuration
{
    public static class DependencyInjectConfig
    {
        public static IServiceCollection AddInjectConfig(this IServiceCollection services)
        {

            services.AddLogging(logging =>
            {
                logging.AddKissLog();
            });

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.AddScoped((context) => Logger.Factory.Get());
            services.AddScoped<AuditoriaILoggerFilter>();

            return services;
        }
    }
}