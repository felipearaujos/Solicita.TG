using Solicita.TG.UI.View.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Solicita.TG.UI.View.Controllers
{
    [LoginFilter]
    public class ProfessorController : Controller
    {
        private ProfessorService.ProfessorServiceClient service = new ProfessorService.ProfessorServiceClient();

        // GET: Professor
        public ActionResult Index()
        {
            return View("Listar");
        }

        public ActionResult Cadastrar()
        {
            ProfessorService.ProfessorModel model = default(ProfessorService.ProfessorModel);
            ProfessorService.ProfessorModel outro = new ProfessorService.ProfessorModel();
            outro.Id = Guid.Empty;
            outro.Nome = String.Empty;
            outro.Sobrenome = String.Empty;
           

            return View("Professor", outro);
        }

        public JsonResult Salvar(Guid id, String Nome, String Sobrenome, Boolean Coordenador)
        {
            if (id.Equals(Guid.Empty))
                service.Criar(Nome, Sobrenome, Coordenador);
            else
                service.Salvar(Nome, Sobrenome, Coordenador);
            return Json("OK");
        }

        public ActionResult Editar(Guid Id)
        {
            ProfessorService.ProfessorModel modelo = service.Get(Id);

            return View("Professor", modelo);
        }

        public JsonResult ListarProfessors()
        {
            var professores = service.ListarTodosProfessores();

            return Json(professores);
        }
    }
}
