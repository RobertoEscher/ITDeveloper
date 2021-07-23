using Cooperchip.ITDeveloper.Data.ORM;
using Cooperchip.ITDeveloper.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Coopership.ITDeveloper.Application.Extensions
{
    public class ReadWriteFile
    {
        public ReadWriteFile()
        {
            
        }

        public async Task<bool> ReadAndWriteCsvAsync(string filePath, ITDeveloperDbContext ctx)
        {          
            var k = 0;
            string line;
            
            using (var fs = System.IO.File.OpenRead(filePath))
            using (var sreader = new StreamReader(fs))
                while ((line = sreader.ReadLine()) != null)
                {
                    var parts = line.Split(";");

                    if (k > 0)
                    {
                        var codigomedicamento = parts[0];
                        var descricao = parts[1];
                        var generico = parts[2];
                        var codigogenerico = parts[3];

                        if (JaTemMedicamento(codigomedicamento, ctx)) return false;

                        ctx.Add(new Medicamento
                        {
                            Codigo = int.Parse(codigogenerico),
                            Descricao = descricao,
                            Generico = generico,
                            CodigoGenerico = int.Parse(codigogenerico)
                        });
                    }
                    k++;
                }
                
            ctx.Database.SetCommandTimeout(180); // 3 minutos
            await ctx.SaveChangesAsync();

            return false;
        }

        private bool JaTemMedicamento(string codigomedicamento, ITDeveloperDbContext ctx)
        {
            return ctx.Medicamento.Any(e => e.Codigo == int.Parse(codigomedicamento));
        }
    }
}
