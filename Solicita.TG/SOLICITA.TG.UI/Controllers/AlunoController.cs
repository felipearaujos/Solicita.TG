using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Solicita.TG.UI.Controllers
{
    public class AlunoController : Controller
    {
        // GET: Aluno
        public ActionResult Index()
        {
            return View("ListarAluno");
        }

        public ActionResult Cadastrar()
        {
            return View("Aluno", new AlunoService.AlunoModel());
        }

        public JsonResult CadastrarAluno(String Nome, String Sobrenome, String RG, String RA, Int32 Periodo, Int32 Turno)
        {
            AlunoService.AlunoServiceClient client = new AlunoService.AlunoServiceClient();

            client.CriarAluno(Nome, Sobrenome, RA, RG, Periodo, Turno);

            return Json("OK");
        }

        public ActionResult Editar(Guid Id)
        {
            AlunoService.AlunoServiceClient client = new AlunoService.AlunoServiceClient();

            var aluno = client.Get(Id);

            return View("Aluno");
        }

        public JsonResult ListarAlunos()
        {
            AlunoService.AlunoServiceClient client = new AlunoService.AlunoServiceClient();

            var alunos = client.ListarTodosAlunos();

            return Json(alunos);
        }
    }
}