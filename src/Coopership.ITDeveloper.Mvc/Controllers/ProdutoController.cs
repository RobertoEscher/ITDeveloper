using Microsoft.AspNetCore.Mvc;

namespace Coopership.ITDeveloper.Mvc.Controllers
{
    public class ProdutoController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public ActionResult Lista()
        {
            return View();
        }
    }
}
