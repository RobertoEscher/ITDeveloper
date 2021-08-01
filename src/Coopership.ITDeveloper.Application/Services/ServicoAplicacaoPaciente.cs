using AutoMapper;
using Cooperchip.ITDeveloper.Domain.Entities;
using Cooperchip.ITDeveloper.Domain.Interfaces.Repository;
using Coopership.ITDeveloper.Application.Interfaces;
using Coopership.ITDeveloper.Application.ViewModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Coopership.ITDeveloper.Application.Services
{
    public class ServicoAplicacaoPaciente : IServicoAplicacaoPaciente
    {
        private readonly IRepositoryPaciente _repoPacientes;
        private readonly IMapper _mapper;

        public ServicoAplicacaoPaciente(IRepositoryPaciente repoPacientes, IMapper mapper)
        {
            _repoPacientes = repoPacientes;
            _mapper = mapper;
        }

        public async Task<PacienteViewModel> ObterPacienteComEstadodoPacienteApplication(Guid pacienteId)
        {
            return _mapper.Map<PacienteViewModel>(await _repoPacientes.ObterPacienteComEstadoPaciente(pacienteId));
        }

        public async Task<IEnumerable<PacienteViewModel>> ObterPacientesComEstadodoPacienteApplication()
        {
            return _mapper.Map<IEnumerable<PacienteViewModel>>(await _repoPacientes.ListaPacientesComEstado());
        }

        public async Task<IEnumerable<PacienteViewModel>> ObterPacientesPorEstadoPacienteApplication(Guid estadoPacienteId)
        {
            return _mapper.Map<IEnumerable<PacienteViewModel>>(await _repoPacientes.ObterPacientesPorEstadoPaciente(estadoPacienteId));
        }

        public async Task<List<EstadoPaciente>> ListaEstadoPacienteApplication()
        {
            return await _repoPacientes.ListaEstadoPaciente();
        }

        public bool TemPacienteApplication(Guid pacienteId)
        {
            return _repoPacientes.TemPaciente(pacienteId);
        }

        #region: mapper na mão
        public async Task<IEnumerable<PacienteViewModel>> ObterPacientesDePacienteViewModelApplication()
        {
            var pacientes = await _repoPacientes.ListaPacientesComEstado();
            List<PacienteViewModel> listaView = new List<PacienteViewModel>();

            foreach(var item in pacientes)
            {
                listaView.Add(new PacienteViewModel
                {
                    Ativo = item.Ativo,
                    Cpf = item.Cpf,
                    DataInternacao = item.DataInternacao,
                    DataNascimento = item.DataNascimento,
                    Email = item.Email,
                    EstadoPaciente = item.EstadoPaciente,
                    EstadoPacienteId = item.EstadoPacienteId,
                    Id = item.Id,
                    Nome = item.Nome,
                    Rg = item.Rg,
                    RgDataEmissao = item.RgDataEmissao,
                    RgOrgao = item.RgOrgao,
                    Sexo = item.Sexo,
                    TipoPaciente = item.TipoPaciente,
                    Motivo = item.Motivo
                });
            }

            return listaView;
        }
        #endregion


    }
}
