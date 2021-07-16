using KissLog;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Data;

namespace Coopership.ITDeveloper.Mvc.Controllers
{
    public class LoggerController : Controller
    {
        private readonly ILogger _logger;

        public LoggerController(ILogger logger)
        {
            _logger = logger;
        }
        [Authorize]
        public IActionResult Index()
        {
            var usuario = HttpContext.User.Identity.Name;

            _logger.Trace($"O usuário: {usuario} foi quem fez isso!");


            try
            {
                throw new Exception("ATENÇÃO: \n UM ERRO PROPOSITAL OCORREU. \n CONTATE O ADMINISTRADOR DO SISTEMA!");
            }
            catch (Exception e)
            {
                _logger.Error($"{e} - Usuário logado: {usuario}");
            }

            //throw new Exception("ATENÇÃO: \n UM ERRO PROPOSITAL OCORREU. \n CONTATE O ADMINISTRADOR DO SISTEMA!");

            return View();
        }
    }
}