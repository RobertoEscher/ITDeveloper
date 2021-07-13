using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Coopership.ITDeveloper.Data.ORM;
using Coopership.ITDeveloper.Domain.Models;

namespace Coopership.ITDeveloper.Mvc.Controllers
{
    public class PacienteController : Controller
    {
        private readonly ITDeveloperDbContext _context;

        public PacienteController(ITDeveloperDbContext context)
        {
            _context = context;
        }

        // GET: Paciente
        public async Task<IActionResult> Index()
        {
            //return View(await _context.Paciente.ToListAsync());
            return View(await _context.Paciente.Include(navigationPropertyPath: x => x.EstadoPaciente).AsNoTracking().ToListAsync());
        }

        // GET: Paciente/Details/5
        public async Task<IActionResult> Details(Guid id)
        {
            if (id == null)
            {
                return NotFound();
            }

            //var paciente = await _context.Paciente
            //    .FirstOrDefaultAsync(m => m.Id == id);

            var paciente = await _context.Paciente.Include(navigationPropertyPath: x => x.EstadoPaciente).AsNoTracking()
                .FirstOrDefaultAsync(m => m.Id == id);

            if (paciente == null)
            {
                return NotFound();
            }

            return View(paciente);
        }

        public IActionResult Create()
        {
            ViewBag.EstadoPaciente = new SelectList(_context.EstadoPaciente, dataValueField: "Id", dataTextField: "Descricao");

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Paciente paciente)
        {
            if (ModelState.IsValid)
            {
                //paciente.Id = Guid.NewGuid();
                _context.Add(paciente);
                await _context.SaveChangesAsync();
                //return RedirectToAction(nameof(Index));
                return RedirectToAction("index");
            }

            //ViewBag.EstadoPaciente = new SelectList(_context.EstadoPaciente, dataValueField:"Id", dataTextField: "Descricao", paciente.EstadoPacienteId);
            ViewBag.EstadoPaciente = new SelectList(_context.EstadoPaciente, dataValueField: "Id", dataTextField: "Descricao");
            return View(paciente);
        }

        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var paciente = await _context.Paciente.FindAsync(id);
            if (paciente == null)
            {
                return NotFound();
            }
            
            ViewBag.EstadoPaciente = new SelectList(_context.EstadoPaciente, dataValueField: "Id", dataTextField: "Descricao", paciente.EstadoPacienteId);

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
                    _context.Update(paciente);
                    await _context.SaveChangesAsync();
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

            ViewBag.EstadoPaciente = new SelectList(_context.EstadoPaciente, dataValueField: "Id", dataTextField: "Descricao", paciente.EstadoPacienteId);

            return View(paciente);
        }

        // GET: Paciente/Delete/5
        public async Task<IActionResult> Delete(Guid id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var paciente = await _context.Paciente.Include(navigationPropertyPath: x => x.EstadoPaciente).AsNoTracking()
                .FirstOrDefaultAsync(m => m.Id == id);
            //var paciente = await _context.Paciente.AsNoTracking()
            //    .FirstOrDefaultAsync(m => m.Id == id);
            if (paciente == null)
            {
                return NotFound();
            }
            ViewBag.EstadoPaciente = new SelectList(_context.EstadoPaciente, dataValueField: "Id", dataTextField: "Descricao", paciente.EstadoPacienteId);

            return View(paciente);
        }

        // POST: Paciente/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var paciente = await _context.Paciente.FindAsync(id);
            _context.Paciente.Remove(paciente);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PacienteExists(Guid id)
        {
            return _context.Paciente.Any(e => e.Id == id);
        }
    }
}
