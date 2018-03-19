using Solicita.TG.UI.View.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Solicita.TG.UI.View.Controllers
{
    [LoginFilter]
    public class FuncionarioAcademicoController : Controller
    {
        private FuncionarioAcademicoService.FuncionarioAcademicoServiceClient service = new FuncionarioAcademicoService.FuncionarioAcademicoServiceClient();

        // GET: /FuncionarioAcademico/
        public ActionResult Index()
        {
            return View("Listar");
        }

        public ActionResult Cadastrar()
        {            
            FuncionarioAcademicoService.FuncionarioModel outro = new FuncionarioAcademicoService.FuncionarioModel();
            outro.Id = Guid.Empty;
            outro.Nome = String.Empty;
            outro.Sobrenome = String.Empty;            

            return View("FuncionarioAcademico", outro);
        }

        public JsonResult Salvar(Guid id, String Nome, String Sobrenome)
        {
            if (id.Equals(Guid.Empty))
            {
                service.CriarFuncionario(Nome, Sobrenome, 0);
            }
            else
            {
            
            }

            return Json("OK");
        }

        public ActionResult Editar(Guid Id)
        {
            FuncionarioAcademicoService.FuncionarioModel modelo = service.Get(Id);

            return View("FuncionarioAcademico", modelo);
        }

        public JsonResult ListarFuncionarioAcademicos()
        {
            var FuncionarioAcademicos = service.ListarTodosFuncionarios();

            return Json(FuncionarioAcademicos);
        }

        public JsonResult ListarPorNome(String nome)
        {
            var FuncionarioAcademicos = service.ListarPorNome(nome);

            return Json(FuncionarioAcademicos);
        }

        public JsonResult ListartipoResponsavelJson()
        {
            List<FuncionarioAcademicoService.TipoDeResponsavelModel> model = service.ListarTipoDeResponsável();

            var data = model
                  .Select(r => new { r.Identificador, value = r.Tipo })
                  .ToArray();
            return Json(data, JsonRequestBehavior.AllowGet);

            return null;
        }

    }
}
