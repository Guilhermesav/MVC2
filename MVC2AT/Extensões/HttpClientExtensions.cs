using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MVC2AT.Dominio.Model.Interfaces.Repository;
using MVC2AT.Dominio.Model.Interfaces.Services;
using MVC2AT.Dominio.Model.Options;
using MVC2AT.HttpServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVC2AT.Extensões
{
    public static class HttpClientExtensions
    {
        public static void RegisterHttpClients(
           this IServiceCollection services,
           IConfiguration configuration)
        {
            var estadoHttpOptionsSection = configuration.GetSection(nameof(EstadoHttpOptions));
            var estadoHttpOptions = estadoHttpOptionsSection.Get<EstadoHttpOptions>();

            services.AddHttpClient(estadoHttpOptions.Name, x => { x.BaseAddress = estadoHttpOptions.ApiBaseUrl; });

            services.AddScoped<ICidadeService, CidadeHttpService>();
            services.AddScoped<IEstadoHttpService, EstadoHttpService>();
                
            
        }
    }
}
