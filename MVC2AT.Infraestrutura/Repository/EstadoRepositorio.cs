using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using MVC2AT.Data.Context;
using MVC2AT.Dominio.Model.Entity;
using MVC2AT.Dominio.Model.Exceptions;
using MVC2AT.Dominio.Model.Interfaces.Repository;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MVC2AT.Data.Repository
{
    public class EstadoRepositorio : IEstadoRepository
    {
        private readonly EstadoContext _context;

        public EstadoRepositorio(
            EstadoContext context)
        {
            _context = context;
        }

        public async Task DeleteAsync(int id)
        {
            var estadoEntity = await _context.Estados.FindAsync(id);
            _context.Estados.Remove(estadoEntity);
        }

        public async Task<bool> CheckCapitalAsync(string capital, int id = 0)
        {
            var capitalExists = await _context.Estados.AnyAsync(x => x.Capital == capital && x.Id != id);

            return capitalExists;
        }

        public async Task<EstadoEntity> GetByCapitalAsync(string capital)
        {
            return await _context.Estados.SingleOrDefaultAsync(x => x.Capital == capital);
        }

        public async Task<IEnumerable<EstadoEntity>> GetAllAsync()
        {
            return await _context.Estados.ToListAsync();
        }

        public async Task<EstadoEntity> GetByIdAsync(int id)
        {
            return await _context.Estados.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task InsertAsync(EstadoEntity insertedEntity)
        {
            _context.Add(insertedEntity);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(EstadoEntity updatedEntity)
        {
            try
            {
                _context.Update(updatedEntity);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (await GetByIdAsync(updatedEntity.Id) == null)
                {
                    throw new RepositoryException("Estado não encontrado!");
                }
                else
                {
                    throw;
                }
            }
        }
    }
}

