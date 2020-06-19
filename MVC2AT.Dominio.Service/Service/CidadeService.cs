using System;
using System.Dynamic;
using MVC2AT.Dominio.Model.Interfaces.Repository;
using System.Threading.Tasks;
using System.Collections.Generic;
using MVC2AT.Dominio.Model.Entity;
using MVC2AT.Dominio.Model.Interfaces.Services;

namespace MVC2AT.Dominio.Service
{
    public class CidadeService : ICidadeService
    {
        private readonly ICidadeRepository _cidadeRepository;

        public CidadeService(
            ICidadeRepository cidadeRepository)
        {
            _cidadeRepository = cidadeRepository;
        }

        public async Task DeleteAsync(int id)
        {
            await _cidadeRepository.DeleteAsync(id);
        }

        public async Task<IEnumerable<CidadeEntity>> GetAllAsync()
        {
            return await _cidadeRepository.GetAllAsync();
        }

        public async Task<CidadeEntity> GetByIdAsync(int id)
        {
            return await _cidadeRepository.GetByIdAsync(id);
        }

        public async Task InsertAsync(CidadeEstadoAggregateEntity cidadeEstadoAggregateEntity)
        {
            await _cidadeRepository.InsertAsync(cidadeEstadoAggregateEntity.CidadeEntity);
        }

        public async Task UpdateAsync(CidadeEntity insertedModel)
        {
            await _cidadeRepository.UpdateAsync(insertedModel);
        }

    }
    
}
