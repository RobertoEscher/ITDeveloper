using Coopership.ITDeveloper.Application.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using Cooperchip.ITDeveloper.Data.ORM;

namespace Cooperchip.ITDeveloper.Mvc.Controllers
{
    [Authorize(Roles = "Admin")]
    public class ConfigController : Controller
    {

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> ImportMedicamentos([FromServices] ITDeveloperDbContext context)
        {

            var filePath = ImportUtils.GetFilePath("Csv", "Medicamentos", ".CSV");

            ReadWriteFile rwf = new ReadWriteFile();
            if (!await rwf.ReadAndWriteCsvAsync(filePath, context))
                return View("JaTemMedicamento", context.Medicamento.AsNoTracking().OrderBy(o => o.Codigo));

            return View("ListaMedicamentos", context.Medicamento.AsNoTracking().OrderBy(o => o.Codigo));
        }
    }
}
