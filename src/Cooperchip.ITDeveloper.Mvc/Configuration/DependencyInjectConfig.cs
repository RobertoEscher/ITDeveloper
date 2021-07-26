using Cooperchip.ITDeveloper.Domain.Interfaces;
using Cooperchip.ITDeveloper.Mvc.Extensions.Filters;
using Cooperchip.ITDeveloper.Mvc.Extensions.Identity;
using Cooperchip.ITDeveloper.Mvc.Extensions.Identity.Services;
using Cooperchip.ITDeveloper.Mvc.Infra;
using Coopership.ITDeveloper.CrossCutting.Helpers;
using KissLog;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Cooperchip.ITDeveloper.Mvc.Configuration
{
    public static class DependencyInjectConfig
    {
        public static IServiceCollection AddDependencyInjectConfig(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddTransient<IUnitOfUpload, UnitOfUpload>();

            // == Mantem o contexto http por toda a aplicação
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            // =====
            services.AddScoped<IUserInContext, AspNetUser>();

            services.AddScoped<IUserClaimsPrincipalFactory<ApplicationUser>, UserClaimsService>();
            
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped((context) => Logger.Factory.Get());
            services.AddScoped<AuditoriaIloggerFilter>();

            services.AddTransient<IEmailSender, EmailSender>();
            services.Configure<AuthMessageSenderOptions>(configuration);

            return services;
        }
    }
}