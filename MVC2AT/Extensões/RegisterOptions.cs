using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MVC2AT.Dominio.Model.Options;

namespace MVC2AT.Extensões
{
    public static class RegisterOptions
    {
        public static void RegisterConfigurations(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            services.Configure<EstadoHttpOptions>(configuration.GetSection(nameof(EstadoHttpOptions)));
            

        }
    }
}
