using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MVC.Apresentação.HttpServices;
using MVC.Apresentação.ViewModels;
using MVC2AT.Data.Context;
using MVC2AT.Dominio.Model.Entity;
using MVC2AT.Dominio.Model.Exceptions;
using MVC2AT.Dominio.Model.Interfaces.Services;

namespace MVC.Apresentação.Controllers
{
    [Authorize]
    public class CidadeController : Controller
    {
        private readonly ICidadeService _cidadeService;
        private readonly IEstadoService _estadoService;
        

        public CidadeController(ICidadeService cidadeService, IEstadoService estadoService)
        {
            _cidadeService = cidadeService;
            _estadoService = estadoService;
        }

        // GET: CidadeEntities
        public async Task<IActionResult> Index()
        {
            var cidades = await _cidadeService.GetAllAsync();

            if (cidades == null)
            {
                return View();
            }

            return View(cidades);
        }

        // GET: CidadeEntities/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cidadeEntity = await _cidadeService.GetByIdAsync(id.Value);
            if (cidadeEntity == null)
            {
                return NotFound();
            }

            return View(cidadeEntity);
        }

        // GET: CidadeEntities/Create
        public async Task<IActionResult> Create()
        {

            return View();
        }

        // POST: CidadeEntities/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CidadeEntity cidadeEntity)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _cidadeService.InsertAsync(cidadeEntity);
                    return RedirectToAction(nameof(Index));
                }
                catch(EntityValidationException erro)
                {
                    ModelState.AddModelError(erro.PropertyName, erro.Message);
                }
            }
            return View(cidadeEntity);
        }

        // GET: CidadeEntities/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cidadeEntity = await _cidadeService.GetByIdAsync(id.Value);
            if (cidadeEntity == null)
            {
                return NotFound();
            }
            var cidadeViewModel = new CidadeViewModel(cidadeEntity, await _estadoService.GetAllAsync());

            return View(cidadeViewModel);
        }

        // POST: CidadeEntities/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, CidadeEntity cidadeEntity)
        {
            if (id != cidadeEntity.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    
                    await _cidadeService.UpdateAsync(cidadeEntity);
                }
                catch(EntityValidationException erro)
                {
                    ModelState.AddModelError(erro.PropertyName, erro.Message);
                    return View(new CidadeViewModel(cidadeEntity));
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (await _cidadeService.GetByIdAsync(id) == null)
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
           
            return View(new CidadeViewModel(cidadeEntity));
        }

        // GET: CidadeEntities/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cidadeEntity = await _cidadeService.GetByIdAsync(id.Value);
            if (cidadeEntity == null)
            {
                return NotFound();
            }

            return View(cidadeEntity);
        }

        // POST: CidadeEntities/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _cidadeService.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
