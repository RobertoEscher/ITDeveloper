using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Cooperchip.ITDeveloper.Mvc.Controllers
{
    public class CodePageController : Controller
    {
        
        public async Task<IActionResult> Index()
        {
#pragma warning disable IDE0090 // Use 'new(...)'
            List<CodigoPagina> listaCodPage = new List<CodigoPagina>();
#pragma warning restore IDE0090 // Use 'new(...)'

            foreach (EncodingInfo encInfo in Encoding.GetEncodings())
            {
                listaCodPage.Add(new CodigoPagina
                {
                    Code = encInfo.CodePage,
                    Name = encInfo.Name,
                    DisplayName = encInfo.DisplayName,
                    WebName = encInfo.GetEncoding().WebName
                });
            }

            //return View(listaCodPage.AsEnumerable());
            return await Task.Run(() => View(listaCodPage.AsEnumerable()));
        }
    }

    public class CodigoPagina
    {
        public int Code { get; set; }
        public string Name { get; set; }
        public string DisplayName { get; set; }
        public string WebName { get; set; }
    }
}
