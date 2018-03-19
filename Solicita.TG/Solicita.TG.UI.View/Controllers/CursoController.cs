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
    public class CursoController : Controller
    {

        private CursoService.CursoServiceClient service = new CursoService.CursoServiceClient();

        //
        // GET: /Curso/

        public ActionResult Index()
        {
             return View("Listar", Listar());
        }

        public List<CursoService.CursoModel> Listar()
        {
            return service.Listar();
        }

        public JsonResult ListarJson()
        {
            List<CursoService.CursoModel> model = Listar();

            var data = model
                  .Select(r => new { r.Id, value = r.Nome})
                  .ToArray();
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Criar()
        {
            CursoService.CursoModel model = new CursoService.CursoModel();
            model.Nome = String.Empty;
            model.Id = Guid.Empty;
            model.GradeCurricular = new List<CursoService.DisciplinaModel>();
           
            return View("Curso", model);
        }

        public JsonResult Salvar(Guid id, String nome, List<Guid> disciplinas)
        {
            try
            {
                if (id.Equals(Guid.Empty))
                    service.Criar(nome, disciplinas);
                else
                    service.Salvar(id, nome, disciplinas);

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
             CursoService.CursoModel model = service.Get(id);

            return View("Curso", model);
            
        }

        //public JsonResult ListarCursosDoAluno(String RA)
        //{
        //    var cursos = service.ListarCursosDoAluno(RA);

        //    return Json(cursos.ToList(), JsonRequestBehavior.AllowGet);
        //}

        public JsonResult Excluir(Guid id, String nome, Int32 cargaHoraria)
        {
            try
            {
                // service.Salvar(id, nome, cargaHoraria);
            }
            catch (FaultException ex)
            {
                return Json("ERROR");
            }
            return Json("OK");
        }
    }
}
