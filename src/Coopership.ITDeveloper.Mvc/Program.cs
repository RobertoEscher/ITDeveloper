using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace Coopership.ITDeveloper.Mvc
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webbuilder =>
                {
                    webbuilder.UseStartup<Startup>();
                });
    }
}
