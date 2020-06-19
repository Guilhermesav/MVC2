using MVC2AT.Dominio.Model.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace MVC2AT.Data.Context.Configuration
{
    public class CidadeConfiguration : IEntityTypeConfiguration<CidadeEntity>
    {
        public void Configure(EntityTypeBuilder<CidadeEntity> builder)
        {


        }
    }
}
