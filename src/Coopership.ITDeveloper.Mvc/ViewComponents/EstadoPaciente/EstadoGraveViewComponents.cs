using Coopership.ITDeveloper.Data.ORM;
using Coopership.ITDeveloper.Mvc.ViewComponents.Helpers;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Coopership.ITDeveloper.Mvc.ViewComponents.EstadoPaciente
{
    [ViewComponent(Name = "EstadoGrave")]
    public class EstadoGraveViewComponents : ViewComponent
    {

        private readonly ITDeveloperDbContext _context;
        public EstadoGraveViewComponents(ITDeveloperDbContext context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var totalGeral = Util.TotReg(_context);
            decimal totalEstado = Util.GetNumRegEstado(_context, "Grave");

            decimal progress = totalEstado * 100 / totalGeral;
            var prct = progress.ToString(format: "F1");

            var model = new ContadorEstadoPaciente()
            {
                Titulo = "Pacientes Graves",
                Parcial = (int)totalEstado,
                Percentual = prct,
                ClassContainer = "panel panel-danger tile panelClose panelRefresh",
                IconeLg = "l-basic-life-buoy",
                IconeSm = "fa fa-arrow-circle-o-down s20 mr5 pull-left",
                Progress = progress
            };

            return View(model);
        }
    }
}
