using Coopership.ITDeveloper.Mvc.Extentions.ViewComponents.Helpers;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Coopership.ITDeveloper.Mvc.Extentions.ViewComponents.CabecalhoModulos
{
    [ViewComponent(Name = "Cabecalho")]
    public class CabecalhoModulosViewComponents : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync(string titulo, string subtitulo)
        {
            var model = new Modulo()
            {
                Titulo = titulo,
                Subtitulo = subtitulo
            };

            return View(model);
        }
    }
}


