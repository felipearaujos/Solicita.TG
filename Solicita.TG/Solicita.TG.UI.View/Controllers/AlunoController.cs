using Solicita.TG.UI.View.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Solicita.TG.UI.View.Controllers
{
    [LoginFilter]
    public class AlunoController : Controller
    {
        private AlunoService.AlunoServiceClient service = new AlunoService.AlunoServiceClient();

        // GET: Aluno
        public ActionResult Index()
        {
            return View("Listar");
        }

        public ActionResult Cadastrar()
        {
            AlunoService.AlunoModel model = default(AlunoService.AlunoModel);
            AlunoService.AlunoModel outro = new AlunoService.AlunoModel();
            outro.Id = Guid.Empty;
            outro.Nome = String.Empty;
            outro.Sobrenome = String.Empty;
            outro.RG = String.Empty;
            outro.Matricula = new AlunoService.AlunoModel.MatriculaModel();
            outro.Matricula.RA = String.Empty;
            outro.Matricula.Turno = String.Empty;
            outro.Matricula.Periodo = String.Empty;

            return View("Aluno",outro);
        }

        public JsonResult Salvar(Guid id ,String Nome, String Sobrenome, String RG, String RA, Int32 Periodo, Int32 Turno, String Curso)
        {
            if (id.Equals(Guid.Empty))
                service.Criar(Nome, Sobrenome, RA, RG, Periodo, Turno, Guid.Parse(Curso));
            else
                service.Salvar(id, Nome, Sobrenome, RA, RG, Periodo, Turno, Guid.Parse(Curso));
            return Json("OK");
        }

        public ActionResult Editar(Guid Id)
        {
            AlunoService.AlunoModel modelo = service.Get(Id);
            modelo.Editavel = true;

             return View("Aluno", modelo);
        }

        public ActionResult MeuCadastroAluno(String RA)
        {
            AlunoService.AlunoModel modelo = service.GetByRA(RA);
            modelo.Editavel = false;
            return PartialView("Aluno", modelo);
        }

        public JsonResult ListarAlunos()
        {
            var alunos = service.Listar(String.Empty);

            return Json(alunos);
        }

        public JsonResult ListarAlunosJson()
        {
            var model = service.Listar(String.Empty);

            var data = model
                 .Select(r => new { r.Id, value = r.Nome + "  " + r.Matricula.RA})
                 .ToArray();
            return Json(data, JsonRequestBehavior.AllowGet);

            
        }


        public JsonResult ListarPorNome(String term)
        {

            var retorno = service.Listar(String.Empty);

            var filtrado = retorno.Where(x => x.Nome.ToUpper().Contains(term.ToUpper()) 
                                           || x.Sobrenome.ToUpper().Contains(term.ToUpper()) 
                                           || x.Matricula.RA.ToUpper().Contains(term.ToUpper())).ToList();

            var data = filtrado.Select(r =>  r.Nome + "  "+ r.Sobrenome + " - " + r.Matricula.RA ).ToArray();

            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetIdPorNome(String nome)
        {
            var retorno = service.Listar(String.Empty);

            var filtrado = retorno.SingleOrDefault(r => (r.Nome + "  " + r.Sobrenome + " - " + r.Matricula.RA).ToUpper().Contains(nome.ToUpper()));

            return Json(filtrado.Id, JsonRequestBehavior.AllowGet);
        }

    }
}
