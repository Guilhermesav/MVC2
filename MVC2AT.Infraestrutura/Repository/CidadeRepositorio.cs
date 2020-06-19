using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MVC2AT.Dominio.Model.Interfaces.Repository;
using MVC2AT.Dominio.Model.Entity;
using MVC2AT.Data.Context;

namespace MVC2AT.Data.Repository
{
    public class CidadeRepositorio : ICidadeRepository
    {
        private readonly EstadoContext _context;

        public CidadeRepositorio(
            EstadoContext context)
        {
            _context = context;
        }

        public async Task DeleteAsync(int id)
        {
            var cidadeEntity = await _context.Cidades.FindAsync(id);
            _context.Cidades.Remove(cidadeEntity);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<CidadeEntity>> GetAllAsync()
        {
            return await _context.Cidades.ToListAsync();
        }

        public async Task<CidadeEntity> GetByIdAsync(int id)
        {
            return await _context.Cidades.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task InsertAsync(CidadeEntity updatedModel)
        {
            _context.Add(updatedModel);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(CidadeEntity insertedModel)
        {
            _context.Update(insertedModel);
            await _context.SaveChangesAsync();
        }
    }
}
