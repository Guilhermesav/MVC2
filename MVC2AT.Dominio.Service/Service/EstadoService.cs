using MVC2AT.Dominio.Model.Entity;
using MVC2AT.Dominio.Model.Interfaces.Repository;
using MVC2AT.Dominio.Model.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MVC2AT.Dominio.Service.Service
{
    public class EstadoService : IEstadoService
    {

        private readonly IEstadoRepository _estadoRepository;

        public EstadoService(
            IEstadoRepository estadoRepository)
        {
            _estadoRepository = estadoRepository;
        }

        public async Task DeleteAsync(int id)
        {
            await _estadoRepository.DeleteAsync(id);
        }

        public async Task<IEnumerable<EstadoEntity>> GetAllAsync()
        {
            return await _estadoRepository.GetAllAsync();
        }

        public async Task<EstadoEntity> GetByIdAsync(int id)
        {
            return await _estadoRepository.GetByIdAsync(id);
        }

        public async Task InsertAsync(EstadoEntity insertedEntity)
        {
            await _estadoRepository.InsertAsync(insertedEntity);
        }

        public async Task UpdateAsync(EstadoEntity updatedEntity)
        {
            await _estadoRepository.UpdateAsync(updatedEntity);
        }
    }
}

