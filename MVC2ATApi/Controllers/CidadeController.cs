using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MVC2AT.Data.Context;
using MVC2AT.Dominio.Model.Entity;
using MVC2AT.Dominio.Model.Exceptions;
using MVC2AT.Dominio.Model.Interfaces.Services;

namespace MVC2ATApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CidadeController : ControllerBase
    {
        private readonly ICidadeService _cidadeService;

        public CidadeController(ICidadeService cidadeService)
        {
            _cidadeService = cidadeService;
        }

        // GET: api/Cidade
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CidadeEntity>>> GetCidades()
        {
            var cidades = await _cidadeService.GetAllAsync();
            return cidades.ToList();
        }

        // GET: api/Cidade/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CidadeEntity>> GetCidadeEntity(int id)
        {
            if (id <= 0)
            {
                return NotFound();
            }

            var cidadeEntity = await _cidadeService.GetByIdAsync(id);


            return Ok(cidadeEntity);
        }

        // PUT: api/Cidade/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCidadeEntity(int id, CidadeEntity cidadeEntity)
        {
            if (id != cidadeEntity.Id)
            {
                return BadRequest();
            }

            try
            {
                await _cidadeService.UpdateAsync(cidadeEntity);
                return Ok();
            }
            catch (EntityValidationException error)
            {
                ModelState.AddModelError(error.PropertyName, error.Message);
                return BadRequest(ModelState);
            }
            catch (RepositoryException error)
            {
                ModelState.AddModelError(string.Empty, error.Message);
                return BadRequest(ModelState);
            }

        }

        // POST: api/Cidade
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<CidadeEntity>> PostCidadeEntity(CidadeEntity cidadeEntity)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                await _cidadeService.InsertAsync(cidadeEntity);

                return Ok(cidadeEntity);
            }
            catch (EntityValidationException e)
            {
                ModelState.AddModelError(e.PropertyName, e.Message);
                return BadRequest(ModelState);
            }
        }

        // DELETE: api/Cidade/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<CidadeEntity>> DeleteCidadeEntity(int id)
        {
            if (id <= 0)
            {
                return NotFound();
            }

            var cidadeEntity = await _cidadeService.GetByIdAsync(id);
            if (cidadeEntity == null)
            {
                return NotFound();
            }
           
            await _cidadeService.DeleteAsync(id);
            

            return Ok(cidadeEntity);
        }

    }
}
