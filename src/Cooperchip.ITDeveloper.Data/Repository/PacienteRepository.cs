using Cooperchip.ITDeveloper.Data.ORM;
using Cooperchip.ITDeveloper.Domain.Entities;
using Cooperchip.ITDeveloper.Domain.Interfaces.Repository;
using Coopership.ITDeveloper.Data.Repository.Base;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Coopership.ITDeveloper.Data.Repository
{
    public class PacienteRepository : RepositoryGeneric<Paciente, Guid>, IRepositoryPaciente
    {
        //private readonly ITDeveloperDbContext _ctx;

        public PacienteRepository(ITDeveloperDbContext ctx) : base(ctx)
        {
            _context = ctx;
        }

        public async Task<IEnumerable<Paciente>> ListaPacientes() => await _context.Paciente.AsNoTracking().ToArrayAsync();

        public async Task<IEnumerable<Paciente>> ListaPacientesComEstado()
        {
            return await _context.Paciente.Include(e => e.EstadoPaciente).AsNoTracking().ToListAsync();
        }

        // Roberto Escher 30/07
        public async Task<List<EstadoPaciente>> ListaEstadoPaciente()
        {
            return await _context.EstadoPaciente.AsNoTracking().ToListAsync();
        }

        public async Task<Paciente> ObterPacienteComEstadoPaciente(Guid PacienteId)
        {
            return await _context.Paciente.Include(e => e.EstadoPaciente).AsNoTracking().FirstOrDefaultAsync(x => x.Id == PacienteId);
        }

        public bool TemPaciente(Guid pacienteId)
        {
            return _context.Paciente.Any(x => x.Id == pacienteId);
        }

        public async Task<IEnumerable<Paciente>> ObterPacientesPorEstadoPaciente(Guid estadoPacienteId)
        {
            var lista = await _context.Paciente.Include(ep => ep.EstadoPaciente).AsNoTracking().Where(x => x.EstadoPacienteId == estadoPacienteId).OrderBy(o => o.Nome).ToListAsync();
            return lista;
        }
    }
}
