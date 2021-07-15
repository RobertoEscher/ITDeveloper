using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Coopership.ITDeveloper.Mvc.Controllers
{
    public class LoggerController : Controller
    {
        private readonly ILogger<LoggerController> _logger;

        public LoggerController(ILogger<LoggerController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            var msgLogger = "TESTANDO EXIBIÇÃO DE LOGGER!";

            _logger.Log(LogLevel.Critical, msgLogger);
            _logger.Log(LogLevel.Warning, msgLogger);
            _logger.Log(LogLevel.Trace, msgLogger);
            _logger.LogError(msgLogger);

            ViewData["magLogger"] = msgLogger;

            try
            {
                throw new Exception("UMA EXCEÇÂO FOI GERADA PARA LOG DE AUDITORIA!");
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
            }

            return View();
        }
    }
}