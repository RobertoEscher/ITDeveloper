using Coopership.ITDeveloper.Data.ORM;
using Coopership.ITDeveloper.Mvc.Data;
using Coopership.ITDeveloper.Mvc.Extentions.Filters;
using KissLog;
using KissLog.AspNetCore;
using KissLog.CloudListeners.Auth;
using KissLog.CloudListeners.RequestLogsListener;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Diagnostics;
using System.Text;

namespace Coopership.ITDeveloper.Mvc
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {





            services.AddLogging(logging =>
            {
                logging.AddKissLog();
            });


            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddDbContext<ITDeveloperDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultITDeveloper")));
            
            services.AddDbContext<AppicationDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultITDeveloper")));
            services.AddDefaultIdentity<IdentityUser>()
                //.AddDefaultUI(UIFramework.Bootstrap4)
                .AddEntityFrameworkStores<AppicationDbContext>();

            services.AddControllersWithViews();
            services.AddRazorPages();

            services.AddMvc(options =>
            {
                options.Filters.Add(typeof(AuditoriaILoggerFilter));
            }).SetCompatibilityVersion(CompatibilityVersion.Version_3_0);

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped((context) => Logger.Factory.Get());

            services.AddScoped<AuditoriaILoggerFilter>();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                //app.UseExceptionHandler("/Home/Error");
                app.UseExceptionHandler("/Logger/Index");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            // app.UseKissLogMiddleware() must to be referenced after app.UseAuthentication(), app.UseSession()
            app.UseKissLogMiddleware(options => {
                ConfigureKissLog(options);
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");

                endpoints.MapRazorPages();
            });

            //app.UseMvc(routes =>
            //{
            //    routes.MapRoute(
            //        name: "default",
            //        template: "{controller=Home}/{action=Index}/{id?}");
            //});
        }

        private void ConfigureKissLog(IOptionsBuilder options)
        {
            // optional KissLog configuration
            options.Options
                .AppendExceptionDetails((Exception ex) =>
                {
                    StringBuilder sb = new StringBuilder();

                    if (ex is System.NullReferenceException nullRefException)
                    {
                        sb.AppendLine("Important: check for null references");
                    }

                    return sb.ToString();
                });

            // KissLog internal logs
            options.InternalLog = (message) =>
            {
                Debug.WriteLine(message);
            };

            // register logs output
            RegisterKissLogListeners(options);
        }

        private void RegisterKissLogListeners(IOptionsBuilder options)
        {
            // multiple listeners can be registered using options.Listeners.Add() method

            // register KissLog.net cloud listener
            options.Listeners.Add(new RequestLogsApiListener(new Application(
                    Configuration["KissLog.OrganizationId"],    //  "127beff8-b205-4199-8aa5-fc4a29f24503"
                    Configuration["KissLog.ApplicationId"])     //  "71780e4a-22d0-4708-982c-6139a0a7fd7c"
            )
            {
                ApiUrl = Configuration["KissLog.ApiUrl"]    //  "https://api.kisslog.net"
            });
        }
    }
}
