﻿using System;
using System.Text;
using Cooperchip.ITDeveloper.Mvc.Configuration;
using Cooperchip.ITDeveloper.Mvc.Data;
using Cooperchip.ITDeveloper.Mvc.Extensions.Identity;
using Cooperchip.ITDeveloper.Mvc.Extensions.Identity.Services;
using Cooperchip.ITDeveloper.Mvc.Extensions.Middlewares;
using Cooperchip.ITDeveloper.Mvc.Identity.Services;
using Coopership.ITDeveloper.Application.AutoMapper;
using KissLog.Apis.v1.Listeners;
using KissLog.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Cooperchip.ITDeveloper.Mvc
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IWebHostEnvironment env)
        {
            var builer = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", true, true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", true, true)
                .AddEnvironmentVariables();

            if (env.IsProduction())
            {
                builer.AddUserSecrets<Startup>();
            }

            Configuration = builer.Build();

        }


        public void ConfigureServices(IServiceCollection services)
        {
            services.AddAutoMapper(typeof(AutoMapperConfig));
            services.AddDbContextConfig(Configuration); // In DbContextConfig
            services.AddIdentityConfig(Configuration); // In IdentityConfig
            services.AddMvcAndRazor(); // In MvcAndRazor
            services.AddDependencyInjectConfig(Configuration); // In DependencyInjectConfig
            services.AddCodePageProviderNotSupportedInDotNetCoreForAnsi();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ApplicationDbContext context, 
            UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseMigrationsEndPoint(); // Add .Net 5
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            if (env.IsProduction())
            {
                app.UseKissLogMiddleware(options =>
                {
                    options.Listeners.Add(new KissLogApiListener(new KissLog.Apis.v1.Auth.Application(
                        Configuration["KissLog.OrganizationId"],
                        Configuration["KissLog.ApplicationId"])
                    ));
                });
            }

            var authMsgSenderOpt = new AuthMessageSenderOptions
            {
                SendGridUser = Configuration["SendGridUser"],
                SendGridKey = Configuration["SendGridKey"]
            };

            //CriaUsersAndRoles.Seed(context, userManager, roleManager).Wait();
            //app.UseMiddleware<DefaultUsersAndRolesMiddleware>();
            //app.UserAddUsersAndRoles();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");

                endpoints.MapRazorPages();

            });


        }
    }
}




//using System;
//using Cooperchip.ITDeveloper.Mvc.Configuration;
//using Cooperchip.ITDeveloper.Mvc.Data;
//using Cooperchip.ITDeveloper.Mvc.Extensions.Identity;
//using Cooperchip.ITDeveloper.Mvc.Extensions.Identity.Services;
//using Cooperchip.ITDeveloper.Mvc.Extensions.Middlewares;
//using Cooperchip.ITDeveloper.Mvc.Identity.Services;
//using KissLog.Apis.v1.Listeners;
//using KissLog.AspNetCore;
//using Microsoft.AspNetCore.Builder;
//using Microsoft.AspNetCore.Hosting;
//using Microsoft.AspNetCore.Identity;
//using Microsoft.Extensions.Configuration;
//using Microsoft.Extensions.DependencyInjection;
//using Microsoft.Extensions.Hosting;

//namespace Cooperchip.ITDeveloper.Mvc
//{
//    public class Startup
//    {
//        public IConfiguration Configuration { get; }

//        public Startup(IWebHostEnvironment env)
//        {
//            var builer = new ConfigurationBuilder()
//                .SetBasePath(env.ContentRootPath)
//                .AddJsonFile("appsettings.json", true, true)
//                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", true, true)
//                .AddEnvironmentVariables();

//            if (env.IsProduction())
//            {
//                builer.AddUserSecrets<Startup>();
//            }

//            Configuration = builer.Build();

//        }


//        public void ConfigureServices(IServiceCollection services)
//        {
//            services.AddDbContextConfig(Configuration); // In DbContextConfig
//            services.AddIdentityConfig(Configuration); // In IdentityConfig
//            services.AddMvcAndRazor(); // In MvcAndRazor
//            services.AddDependencyInjectConfig(Configuration); // In DependencyInjectConfig

//        }

//        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ApplicationDbContext context, 
//            UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
//        {
//            if (env.IsDevelopment())
//            {
//                app.UseDeveloperExceptionPage();
//            }
//            else
//            {
//                app.UseExceptionHandler("/Home/Error");
//                app.UseHsts();
//            }

//            app.UseHttpsRedirection();
//            app.UseStaticFiles();
//            app.UseCookiePolicy();

//            app.UseRouting();

//            app.UseAuthentication();
//            app.UseAuthorization();

//            if (env.IsProduction())
//            {
//                app.UseKissLogMiddleware(options =>
//                {
//                    options.Listeners.Add(new KissLogApiListener(new KissLog.Apis.v1.Auth.Application(
//                        Configuration["KissLog.OrganizationId"],
//                        Configuration["KissLog.ApplicationId"])
//                    ));
//                });
//            }

//            var authMsgSenderOpt = new AuthMessageSenderOptions
//            {
//                SendGridUser = Configuration["SendGridUser"],
//                SendGridKey = Configuration["SendGridKey"]
//            };

//            //CriaUsersAndRoles.Seed(context, userManager, roleManager).Wait();
//            //app.UseMiddleware<DefaultUsersAndRolesMiddleware>();
//            //app.UserAddUsersAndRoles();

//            app.UseEndpoints(endpoints =>
//            {
//                endpoints.MapControllerRoute(
//                    name: "default",
//                    pattern: "{controller=Home}/{action=Index}/{id?}");

//                endpoints.MapRazorPages();

//            });


//        }
//    }
//}
