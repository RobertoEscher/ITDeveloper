
using Cooperchip.ITDeveloper.Domain.Entities;
using Cooperchip.ITDeveloper.Domain.Interfaces.Repository;
using Coopership.ITDeveloper.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace Cooperchip.ITDeveloper.Mvc.Controllers
{
    [Authorize(Roles = "Admin")]
    public class PacienteController : Controller
    {
        //private readonly ITDeveloperDbContext _context;

        private readonly IServicoAplicacaoPaciente _seviceApp;
        private readonly IRepositoryPaciente _repoPacientes;

        public PacienteController(//ITDeveloperDbContext context,
                                  IRepositoryPaciente repoPacientes,
                                  IServicoAplicacaoPaciente seviceApp)
        {
            //_context = context;
            _repoPacientes = repoPacientes;
            _seviceApp = seviceApp;
        }

        // GET: Paciente
        public async Task<IActionResult> Index()
        {
            //return View(await _repoPacientes.ListaPacientesComEstado());
            //return View(await _seviceApp.ObterPacientesComEstadodoPacienteApplication());
            return View(await _seviceApp.ObterPacientesComEstadodoPacienteApplication());
        }

        public async Task<IActionResult> Details(Guid id)
        {


            //var paciente = await _repoPacientes.ObterPacienteComEstadoPaciente(id);
            //var paciente = await _context.Paciente.Include(x=>x.EstadoPaciente).AsNoTracking()
            //    .FirstOrDefaultAsync(m => m.Id == id);

            var paciente = await _seviceApp.ObterPacienteComEstadodoPacienteApplication(id);

            if (paciente == null)
            {
                return NotFound();
            }

            return View(paciente);
        }

        public async Task<IActionResult> ReportForEstadoPaciente(Guid id)
        {
            var pacientePorEstado = await _seviceApp.ObterPacientesPorEstadoPacienteApplication(id);

            if (pacientePorEstado == null) return NotFound();

            return View(pacientePorEstado);
        }

        public async Task<IActionResult> Create()
        {
            ViewBag.EstadoPaciente = new SelectList(await _seviceApp.ListaEstadoPacienteApplication(), "Id", "Descricao");
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Paciente paciente)
        {
            if (ModelState.IsValid)
            {
                //paciente.Id = Guid.NewGuid(); // Não Usar
                //_context.Add(paciente);
                await this._repoPacientes.Inserir(paciente);
                //await _context.SaveChangesAsync();
                //return RedirectToAction(nameof(Index));
                return RedirectToAction("Index");
            }
            ViewBag.EstadoPaciente = new SelectList(await _seviceApp.ListaEstadoPacienteApplication(), "Id", "Descricao", paciente.EstadoPacienteId);
            return View(paciente);
        }

        public async Task<IActionResult> Edit(Guid id)
        {
            var paciente = await _repoPacientes.SelecionarPorId(id);
            if (paciente == null)
            {
                return NotFound();
            }
            ViewBag.EstadoPaciente = new SelectList(await _seviceApp.ListaEstadoPacienteApplication(), "Id", "Descricao", paciente.EstadoPacienteId);
            return View(paciente);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, Paciente paciente)
        {
            if (id != paciente.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    //_context.Update(paciente);
                    await this._repoPacientes.Atualizar(paciente);
                    //await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PacienteExists(paciente.Id))
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
            ViewBag.EstadoPaciente = new SelectList(await _seviceApp.ListaEstadoPacienteApplication(), "Id", "Descricao", paciente.EstadoPacienteId);
            return View(paciente);
        }

        public async Task<IActionResult> Delete(Guid id)
        {
            //var paciente = await _context.Paciente.Include(x=>x.EstadoPaciente).AsNoTracking()
            //    .FirstOrDefaultAsync(m => m.Id == id);

            var paciente = await _repoPacientes.ObterPacienteComEstadoPaciente(id);
            if (paciente == null)
            {
                return NotFound();
            }

            return View(paciente);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            //var paciente = await _context.Paciente.FindAsync(id);
            //_context.Paciente.Remove(paciente);
            
            //await _context.SaveChangesAsync();

            await _repoPacientes.ExcluirPorId(id);
            
            return RedirectToAction(nameof(Index));
        }

        private bool PacienteExists(Guid id)
        {
            return _seviceApp.TemPacienteApplication(id);
        }
    }
}
