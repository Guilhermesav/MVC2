using Microsoft.EntityFrameworkCore;
using System;
using MVC2AT.Dominio.Model.Entity;
using System.Threading.Tasks;
using MVC2AT.Dominio.Model.Interfaces.Repository;
using MVC2AT.Data.Context.Configuration;

namespace MVC2AT.Data.Context
{
    public class EstadoContext : DbContext
    {
        public EstadoContext(DbContextOptions<EstadoContext> options)
            : base(options)
        {
        }

        public DbSet<CidadeEntity> Cidades { get; set; }
        public DbSet<EstadoEntity> Estados { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new CidadeConfiguration());
            modelBuilder.ApplyConfiguration(new EstadoConfiguration());

            base.OnModelCreating(modelBuilder);
        }
    }
}
