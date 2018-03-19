using Solicita.TG.UI.View.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Solicita.TG.UI.View.Controllers
{
    [LoginFilter]
    public class TurnoController : Controller
    {
        //
        // GET: /Turno/

        public ActionResult Index()
        {
            return View();
        }

        public JsonResult ListarJson()
        {
            List<fuck> model = new List<fuck>();
            model.Add(new fuck() { Id = "1", value = "Manhã" });
            model.Add(new fuck() { Id = "2", value = "Tarde" });
            model.Add(new fuck() { Id = "3", value = "Noite" });

            var data = model
                  .Select(r => new { r.Id, value = r.value })
                  .ToArray();
            return Json(data, JsonRequestBehavior.AllowGet);
        }


        public class fuck
        {
            public string Id { get; set; }
            public string value { get; set; }
        }
    }
}
