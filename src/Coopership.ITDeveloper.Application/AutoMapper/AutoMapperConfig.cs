using AutoMapper;
using Cooperchip.ITDeveloper.Domain.Entities;
using Coopership.ITDeveloper.Application.ViewModels;

namespace Coopership.ITDeveloper.Application.AutoMapper
{
    public class AutoMapperConfig : Profile
    {
        public AutoMapperConfig()
        {
            CreateMap<Paciente, PacienteViewModel>().ReverseMap();
        }
    }
}
