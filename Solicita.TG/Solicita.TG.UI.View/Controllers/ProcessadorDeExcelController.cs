using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Solicita.TG.UI.View.Controllers
{
    public class ProcessadorDeExcelController : Controller
    {
        private ProcessadorDeArquivoService.ProcessadorDeArquivoServiceClient service = new ProcessadorDeArquivoService.ProcessadorDeArquivoServiceClient();

        //
        // GET: /ProcessadorDeExcel/

        public ActionResult Index()
        {
            return View("ProcessadorDeExcel");
        }


   
        [HttpPost]
        public ActionResult AdicionarExcel()
        {
            var httpPostedFile = HttpContext.Request.Files[0];

            MemoryStream target = new MemoryStream();
            httpPostedFile.InputStream.CopyTo(target);

            String file = httpPostedFile.FileName;
                
            byte[] data = target.ToArray();

            String fileContent = Convert.ToBase64String(data);

            service.ProcessarExcel(file, fileContent, "ALUNO");

            return Index();
        }


        public JsonResult DownloadExcel()
        {
           String retorno =  service.DownloadExcelModelo();

           return Json(retorno);
        }

       


    }
}
