using Solicita.TG.UI.View.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Solicita.TG.UI.View.Controllers
{
    [LoginFilter]
    public class PeriodoController : Controller
    {
        //
        // GET: /Periodo/

        public ActionResult Index()
        {
            return View();
        }


        public JsonResult ListarJson()
        {
            List<String> model = new List<String>();

            for (var i = 1; i < 8; i++)
            {
                model.Add(i.ToString());
            }

            var data = model
                  .Select(r => new { r, value = r })
                  .ToArray();
            return Json(data, JsonRequestBehavior.AllowGet);
        }
    }
}
