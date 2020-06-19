using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MVC2AT.Dominio.Model.Entity;

namespace MVC2AT.Dominio.Model.Interfaces.Services
{
    public interface ICidadeService
    {
        Task<IEnumerable<CidadeEntity>> GetAllAsync();
        Task<CidadeEntity> GetByIdAsync(int id);
        Task InsertAsync(CidadeEstadoAggregateEntity cidadeEstadoAggregateEntity);
        Task UpdateAsync(CidadeEntity updatedEntity); 
        Task DeleteAsync(int id);
    }
}
