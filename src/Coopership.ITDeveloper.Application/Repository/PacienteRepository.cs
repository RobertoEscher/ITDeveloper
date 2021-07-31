using Cooperchip.ITDeveloper.Data.ORM;
using Cooperchip.ITDeveloper.Domain.Interfaces.Repository;
using Cooperchip.ITDeveloper.Domain.Models;
using Coopership.ITDeveloper.Repository.Base;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Coopership.ITDeveloper.Application.Repository
{
    public class PacienteRepository : RepositoryGeneric<Paciente, Guid>, IRepositoryPaciente
    {
        private readonly ITDeveloperDbContext _ctx;

        public PacienteRepository(ITDeveloperDbContext ctx) : base(ctx)
        {
            _ctx = ctx;
        }

        public async Task<IEnumerable<Paciente>> ListaPacientes() => await _ctx.Paciente.AsNoTracking().ToArrayAsync();

        public async Task<IEnumerable<Paciente>> ListaPacientesComEstado()
        {
            return await _ctx.Paciente.Include(e => e.EstadoPaciente).AsNoTracking().ToListAsync();
        }

        // Roberto Escher 30/07
        public List<EstadoPaciente> ListaEstadoPaciente()
        {
            return _ctx.EstadoPaciente.AsNoTracking().ToListAsync().Result;
        }

        public async Task<Paciente> ObterPacienteComEstadoPaciente(Guid PacienteId)
        {
            return await _ctx.Paciente.Include(e => e.EstadoPaciente).AsNoTracking().FirstOrDefaultAsync(x => x.Id == PacienteId);
        }

        public bool TemPaciente(Guid pacienteId)
        {
            return _ctx.Paciente.Any(x => x.Id == pacienteId);
        }

        public async Task<IEnumerable<Paciente>> ObterPacientesPorEstadoPaciente(Guid estadoPacienteId)
        {
            var lista = await _ctx.Paciente.Include(ep => ep.EstadoPaciente).AsNoTracking().Where(x => x.EstadoPacienteId == estadoPacienteId).OrderBy(o => o.Nome).ToListAsync();
            return lista;
        }
    }
}
