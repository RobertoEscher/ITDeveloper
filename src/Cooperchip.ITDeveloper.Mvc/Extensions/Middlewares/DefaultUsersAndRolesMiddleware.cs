using Cooperchip.ITDeveloper.Mvc.Data;
using Cooperchip.ITDeveloper.Mvc.Extensions.Identity;
using Cooperchip.ITDeveloper.Mvc.Identity.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System.Diagnostics;
using System.Threading.Tasks;

namespace Cooperchip.ITDeveloper.Mvc.Extensions.Middlewares
{
    public class DefaultUsersAndRolesMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ApplicationDbContext _dbContext;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public DefaultUsersAndRolesMiddleware(RequestDelegate next, ApplicationDbContext dbContext, 
            UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _next = next;
            _dbContext = dbContext;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task InvokeAsync(HttpContext _context)
        {
            Debug.WriteLine("RODANDO O PROCESSO DE VERIFICAÇÃO DE USUÁRIO E PAPEIS EXISTENTES. SE NÃO HOUVER CRIAR!");

            CriaUsersAndRoles.Seed(_dbContext, _userManager, _roleManager).Wait();

            await _next(_context);

            Debug.WriteLine("PROCESSO DE VERIFICAÇÃO DE USUÁRIO E PAPEIS TERMINADO!");

        }

    }
}
