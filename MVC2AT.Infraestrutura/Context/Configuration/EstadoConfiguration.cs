using MVC2AT.Dominio.Model.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MVC2AT.Data.Context.Configuration
{
    public class EstadoConfiguration : IEntityTypeConfiguration<EstadoEntity>
    {
        public void Configure(EntityTypeBuilder<EstadoEntity> builder)
        {
            builder
                .HasIndex(x => x.Capital)
                .IsUnique();
        }
    }
}
