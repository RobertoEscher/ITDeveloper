using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

namespace Cooperchip.ITDeveloper.Mvc.Configuration
{
    public static class EncodingANSIConfig
    {
        public static IServiceCollection AddCodePageProviderNotSupportedInDotNetCoreForAnsi(this IServiceCollection services)
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

            return services;
        }

    }
}
