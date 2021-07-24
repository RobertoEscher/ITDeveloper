using Cooperchip.ITDeveloper.Data.ORM;
using Cooperchip.ITDeveloper.Domain.Models;
using Coopership.ITDeveloper.Application.Extensions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using X.PagedList;

namespace Cooperchip.ITDeveloper.Mvc.Controllers
{
    public class CidController : Controller
    {
        private readonly ITDeveloperDbContext _context;

        public CidController(ITDeveloperDbContext context)
        {
            _context = context;
        }

        //public async Task<IActionResult> Index()
        //{
        //    return View(await _context.Cid.AsNoTracking().OrderBy(o => o.CidInternalId)
        //        .Where(c => c.CidInternalId < 10).ToListAsync());
        //}
        [HttpGet]
        public async Task<IActionResult> Index(int? pagina)
        {
            const int itensPorPagina = 8;
            int numeroPagina = (pagina ?? 1);

            return View(await _context.Cid
                .AsNoTracking()
                .OrderBy(o => o.CidInternalId)
                .ToPagedListAsync(numeroPagina, itensPorPagina));
        }

        public IActionResult arquivoInvalido()
        {
            TempData["arquivoInvalido"] = "O arquivo não é válido!";
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ImportCid(IFormFile file, [FromServices] IWebHostEnvironment webHostEnvironment)
        {
            if (!ArquivoValido.EhValido(file, "Cid.Csv")) return RedirectToAction("ArquivoInvalido");

            var filePath = $"{webHostEnvironment.WebRootPath}\\importFile\\{file.FileName}";
            CopiarArquivo.Copiar(file, filePath);

            int k = 0;
            string line;

            List<Cid> cids = new List<Cid>();
            Encoding encodingPage = Encoding.GetEncoding(1252);
            bool detectEncoding = false;

            using (var fs = System.IO.File.OpenRead(filePath))
            {
                using (var stream = new StreamReader(fs, encodingPage, detectEncoding))
                {
                    while ((line = stream.ReadLine()) != null)
                    {
                        string[] parts = line.Split(";");

                        if (k > 0)
                        {
                            if (!_context.Cid.Any(e => e.CidInternalId == int.Parse(parts[0])))
                            {
                                cids.Add(new Cid
                                {
                                    CidInternalId = int.Parse(parts[0]),
                                    Codigo = parts[1],
                                    Diagnostico = parts[2]
                                });
                            }
                        }
                        k++;
                    }
                }
            }

            if (cids.Any())
            {
                await _context.AddRangeAsync(cids);
                _context.Database.SetCommandTimeout(180); // 3 minutos
                await _context.SaveChangesAsync();
            }
                
            return RedirectToAction(nameof(Index));
        }
    }
}
