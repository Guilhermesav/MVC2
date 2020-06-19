using MVC2AT.Dominio.Model.Entity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MVC2AT.Dominio.Model.Interfaces.Services
{
    public interface IEstadoService
    {
        Task<IEnumerable<EstadoEntity>> GetAllAsync();
        Task<EstadoEntity> GetByIdAsync(int id);
        Task InsertAsync(EstadoEntity insertedEntity);
        Task UpdateAsync(EstadoEntity updatedEntity);
        Task DeleteAsync(int id);
    }
}
