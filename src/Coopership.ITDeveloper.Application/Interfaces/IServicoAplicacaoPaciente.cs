using Cooperchip.ITDeveloper.Domain.Entities;
using Coopership.ITDeveloper.Application.ViewModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Coopership.ITDeveloper.Application.Interfaces
{
    public interface IServicoAplicacaoPaciente
    {
        Task<IEnumerable<PacienteViewModel>> ObterPacientesComEstadodoPacienteApplication();
        Task<PacienteViewModel> ObterPacienteComEstadodoPacienteApplication(Guid pacienteId);
        Task<IEnumerable<PacienteViewModel>> ObterPacientesPorEstadoPacienteApplication(Guid estadoPacienteId);
        Task<List<EstadoPaciente>> ListaEstadoPacienteApplication();
        bool TemPacienteApplication(Guid pacienteId);

    }
}
