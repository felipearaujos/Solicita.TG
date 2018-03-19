using Solicita.TG.UI.View.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Solicita.TG.UI.View.Controllers
{
    public class DashboardController : Controller
    {
        //
        // GET: /Dashboard/

        private DashboardService.DashboardServiceClient client = new DashboardService.DashboardServiceClient();

        public ActionResult Index()
        {
            return View("QuantidadePorTipo");
        }

        public ActionResult QuantidadePorTipo()
        {
            return PartialView("QuantidadePorTipo");
        }
        public ActionResult QuantidadePorTipoChart(List<String> status, String aluno, Boolean agruparPorTipo, String disciplina, String dataInicial, String dataFinal)
        {
            ChartParameters param = new ChartParameters();

            List<Int32> StatusList = new List<Int32>();
            if (status != null)
            {
                foreach (var sts in status)
                {
                    StatusList.Add(Convert.ToInt32(sts));
                }
            }

            if (!String.IsNullOrEmpty(aluno))
                param.Aluno = Guid.Parse(aluno);

            if (!String.IsNullOrEmpty(disciplina))
                param.Disciplina = Guid.Parse(disciplina);

            param.Status = StatusList;
            param.AgruparPorTipo = agruparPorTipo;

            param.DataInicial = dataInicial;
            param.DataFinal = dataFinal;

            return PartialView("QuantidadePorTipoChart", param);
        }




        public JsonResult GerarVolumetriaMensal()
        {
            var volumetria = client.CriarGraficoDeVolumetriaMensal();

            return Json(volumetria, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GerarVolumetriaAnual()
        {
            var volumetria = client.CriarGraficoDeVolumetriaAnual();

            return Json(volumetria, JsonRequestBehavior.AllowGet);
        }


        public JsonResult GerarQuantidadePorTipo(List<String> status, Guid aluno, Boolean AgruparPorTipo, Guid Disciplina, String DataInicial, String DataFinal)
        {
            List<Int32> StatusFilter = new List<int>();

            if (status != null)
            {
                foreach (var sts in status)
                {
                    StatusFilter.Add(Convert.ToInt32(sts));
                }

            }
            var rel = client.GraficoQuantidadePorTipo(StatusFilter.ToArray(), aluno, Guid.Empty, AgruparPorTipo, Disciplina, DataInicial, DataFinal);

            return Json(rel, JsonRequestBehavior.AllowGet);
        }


        public JsonResult ResumoAtual()
        {
            var rel = client.ResumoAtual();

            return Json(rel, JsonRequestBehavior.AllowGet);
        }



    }
}
