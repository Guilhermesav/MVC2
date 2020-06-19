using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MVC2AT.Dominio.Model.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVC2ATApi.Extensions
{
    public static class ConfigurationExtensions
    {
        public static void RegisterConfigurations(
                    this IServiceCollection services,
                    IConfiguration configuration)
        {
            services.Configure<JwtSettings>(configuration.GetSection(nameof(JwtSettings)));
        }
    }
}
