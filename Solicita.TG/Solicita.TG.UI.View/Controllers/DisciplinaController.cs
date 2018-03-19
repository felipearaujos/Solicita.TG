using Solicita.TG.UI.View.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Web;
using System.Web.Mvc;

namespace Solicita.TG.UI.View.Controllers
{
    [LoginFilter]
    public class DisciplinaController : Controller
    {
        private DisciplinaService.DisciplinaServiceClient service = new DisciplinaService.DisciplinaServiceClient();
        //
        // GET: /Disciplina/

        public ActionResult Index()
        {
            return View("Listar", Listar());
        }

        public List<DisciplinaService.DisciplinaModel> Listar()
        {
             return  service.Listar();
        }

        public JsonResult ListarJson()
        {
            List<DisciplinaService.DisciplinaModel> model = Listar();

            var data = model
                  .Select(r => new { r.Id, value = r.Nome + " - " + r.CargaHoraria + " Horas"})
                  .ToArray();
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Criar()
        {
            
            DisciplinaService.DisciplinaModel outro = new DisciplinaService.DisciplinaModel();
            outro.Nome = String.Empty;
            outro.Id = Guid.Empty;
            outro.CargaHoraria = 0;
            return View("Disciplina", outro);
        }

        public JsonResult Salvar(Guid id, String nome, Int32 cargaHoraria)
        {
            
            service.Open();

            try
            {
                if (id.Equals(Guid.Empty))
                    service.Criar(nome, cargaHoraria);
                else
                    service.Salvar(id, nome, cargaHoraria);

                service.Close();
            }
            catch (FaultException ex)
            {
                throw new MyFaultException(Json(new { Message = ex.Message }));
            }
            return Json(new { Message = "OK" });
        }

        public ActionResult Editar(Guid id)
        {
            DisciplinaService.DisciplinaModel model= service.Get(id);

            return View("Disciplina", model);
        }

        public JsonResult Excluir(Guid id, String nome, Int32 cargaHoraria)
        {
            try
            {
                service.Salvar(id, nome, cargaHoraria);
            }
            catch (FaultException ex)
            {
                return Json("ERROR");
            }
            return Json("OK");
        }

        public JsonResult ListarPorNome(String term)
        {
            var data = service.ListarDisciplinasPorNome(term);
            var returnObject = data.Select(x => x.Nome).ToList();

            return Json(returnObject, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetIdPorNome(String nome)
        {
            var data = service.ListarDisciplinasPorNome(nome);
            var returnObject = data.SingleOrDefault(x => x.Nome.Equals(nome));

            return Json(returnObject.Id, JsonRequestBehavior.AllowGet);
        }
    }
}
