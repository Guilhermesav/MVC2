using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MVC2AT.Data.Context;
using MVC2AT.Dominio.Model.Interfaces.Repository;
using MVC2AT.Dominio.Model.Interfaces.Services;
using MVC2AT.Dominio.Service;
using MVC2AT.Data.Repository;
using MVC2AT.Dominio.Service.Service;

namespace DepedencyInjection
{
    public static class DependencyInjection
    {
        public static void RegisterInjections(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddDbContext<EstadoContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("EstadoContext")));

            services.AddScoped<ICidadeService, CidadeService>();
            services.AddScoped<ICidadeRepository, CidadeRepositorio>();
            services.AddScoped<IEstadoService, EstadoService>();
            services.AddScoped<IEstadoRepository, EstadoRepositorio>();
            

          
        }
    }
}
