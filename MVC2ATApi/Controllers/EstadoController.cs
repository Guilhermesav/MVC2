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
    public class EstadoController : ControllerBase
    {
        private readonly IEstadoService _estadoService;

        public EstadoController(IEstadoService estadoService)
        {
            _estadoService = estadoService;
        }

        // GET: api/Estado
        [HttpGet]
        public async Task<ActionResult<IEnumerable<EstadoEntity>>> GetEstados()
        {
            var estados = await _estadoService.GetAllAsync();
            return Ok(estados.ToList());
        }

        // GET: api/Estado/5
        [HttpGet("{id}")]
        public async Task<ActionResult<EstadoEntity>> GetEstadoEntity(int id)
        {
            if (id <= 0)
            {
                return NotFound();
            }

            var estadoEntity = await _estadoService.GetByIdAsync(id);

            if (estadoEntity == null)
            {
                return NotFound("Estado não encontrado");
            }

            return estadoEntity;
        }

        // PUT: api/Estado/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEstadoEntity(int id, EstadoEntity estadoEntity)
        {
            if (id != estadoEntity.Id)
            {
                return BadRequest();
            }

            try
            { 
                await _estadoService.UpdateAsync(estadoEntity);
            }
            catch (RepositoryException x)
            {
                ModelState.AddModelError(string.Empty, x.Message);
                return BadRequest(ModelState);
            }

            return NoContent();
        }

        // POST: api/Estado
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<EstadoEntity>> PostEstadoEntity(EstadoEntity estadoEntity)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _estadoService.InsertAsync(estadoEntity);

            return Ok(estadoEntity);
        }

        // DELETE: api/Estado/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<EstadoEntity>> DeleteEstadoEntity(int id)
        {
            if (id <= 0)
            {
                return NotFound();
            }

            var estadoEntity = await _estadoService.GetByIdAsync(id);
            if (estadoEntity == null)
            {
                return NotFound();
            }

            await _estadoService.DeleteAsync(id);

            return Ok(estadoEntity);
        }

    }
}
