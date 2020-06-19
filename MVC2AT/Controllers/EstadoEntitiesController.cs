using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MVC2AT.Data.Context;
using MVC2AT.Dominio.Model.Entity;
using MVC2AT.HttpServices;
using Newtonsoft.Json;

namespace MVC2AT.Controllers
{
    [Authorize]
    public class EstadoEntitiesController : Controller
    {
       
        private readonly IEstadoHttpService _estadoService;
        private readonly SignInManager<IdentityUser> _signInManager;
        public EstadoEntitiesController(SignInManager<IdentityUser> signInManager,IEstadoHttpService estadoService)
        {
            _signInManager = signInManager;
            _estadoService = estadoService;
        }

        // GET: EstadoEntities
        public async Task<IActionResult> Index()
        {
            var estados = await _estadoService.GetAllAsync();

            if (estados == null)
                return Redirect("/Identity/Account/Login");

            return View(estados);
        }

        // GET: EstadoEntities/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var httpResponseMessage = await _estadoService.GetByIdHttpAsync(id.Value);

            if (!httpResponseMessage.IsSuccessStatusCode)
            {
                if (httpResponseMessage.StatusCode == HttpStatusCode.Forbidden)
                {
                    await _signInManager.SignOutAsync();
                    return Redirect("/Identity/Account/Login");
                }
                else
                {
                    var message = await httpResponseMessage.Content.ReadAsStringAsync();
                    ModelState.AddModelError(string.Empty, message);

                    var estados = await _estadoService.GetAllAsync();
                    return View(nameof(Index), estados);
                }
            }

            var estadoEntity = JsonConvert.DeserializeObject<EstadoEntity>(await httpResponseMessage.Content.ReadAsStringAsync());
            if (estadoEntity == null)
            {
                return NotFound();
            }

            return View(estadoEntity);
        }

        // GET: EstadoEntities/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: EstadoEntities/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(EstadoEntity estadoEntity)
        {
            if (ModelState.IsValid)
            {
                await _estadoService.InsertAsync(estadoEntity);
                return RedirectToAction(nameof(Index));
            }
            return View(estadoEntity);
        }

        // GET: EstadoEntities/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var estadoEntity = await _estadoService.GetByIdAsync(id.Value);
            if (estadoEntity == null)
            {
                return NotFound();
            }
            return View(estadoEntity);
        }

        // POST: EstadoEntities/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id,EstadoEntity estadoEntity)
        {
            if (id != estadoEntity.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                await _estadoService.UpdateAsync(estadoEntity);

                return RedirectToAction(nameof(Index));
            }
            return View(estadoEntity);
        }

        // GET: EstadoEntities/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var estadoEntity = await _estadoService.GetByIdAsync(id.Value);
            if (estadoEntity == null)
            {
                return NotFound();
            }

            return View(estadoEntity);
        }

        // POST: EstadoEntities/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _estadoService.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
