using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MVC2AT.Dominio.Model.Entity;

namespace MVC2AT.Dominio.Model.Interfaces.Repository
{
    public interface ICidadeRepository
    {
        Task<IEnumerable<CidadeEntity>> GetAllAsync();
        Task<CidadeEntity> GetByIdAsync(int id);
        Task InsertAsync(CidadeEntity updatedEntity);
        Task UpdateAsync(CidadeEntity insertedEntity);
        Task DeleteAsync(int id);
    }
}
