using System;
using KissLog;
using Microsoft.AspNetCore.Mvc;

namespace Coopership.ITDeveloper.Mvc.Controllers
{
    public class LoggerController : Controller
    {
        //private readonly ILogger<LoggerController> _logger;
        private readonly ILogger _logger;

        public LoggerController(ILogger logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            var msgLogger = "ATENÇÃO: \n UM ERRO PROPOSITAL OCORREU!";

            //_logger.Log(LogLevel.Critical, msgLogger);
            //_logger.Log(LogLevel.Warning, msgLogger);
            //_logger.Log(LogLevel.Trace, msgLogger);
            //_logger.LogError(msgLogger);

            

            try
            {
                throw new Exception(msgLogger);
            }
            catch (Exception e)
            {
                _logger.Error(e);
            }
            
            //ViewData["magLogger"] = msgLogger;

            return View();
        }
    }
}