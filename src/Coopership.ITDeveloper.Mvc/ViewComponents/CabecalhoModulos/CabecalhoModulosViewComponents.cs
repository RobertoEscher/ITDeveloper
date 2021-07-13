using Coopership.ITDeveloper.Mvc.ViewComponents.Helpers;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Coopership.ITDeveloper.Mvc.ViewComponents.CabecalhoModulos
{
    [ViewComponent(Name = "Cabecalho")]
    public class CabecalhoModulosViewComponents : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync(string titulo, string subtitulo)
        {
            var modulo = new Modulo()
            {
                Titulo = titulo,
                Subtitulo = subtitulo
            };

            return View(modulo);
        }
    }
}
