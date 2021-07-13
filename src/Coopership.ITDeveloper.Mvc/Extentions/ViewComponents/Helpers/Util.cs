using Coopership.ITDeveloper.Data.ORM;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Coopership.ITDeveloper.Mvc.Extentions.ViewComponents.Helpers
{
    public static class Util
    {
        public static int TotReg(ITDeveloperDbContext ctx)
        {
            return (from pac in ctx.Paciente.AsNoTracking() select pac).Count();
        }

        public static decimal GetNumRegEstado(ITDeveloperDbContext ctx, string estado)
        {
            return ctx.Paciente
                .Include(navigationPropertyPath: x => x.EstadoPaciente)
                .AsNoTracking()
                .Count(x => x.EstadoPaciente.Descricao.Contains(estado));
        }
    }
}
