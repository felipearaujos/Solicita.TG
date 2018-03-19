using Solicita.TG.Compartilhado;
using Solicita.TG.Serviços.Requerimentos.Modelos;
using Solicita.TG.UI.View.Filters;
using Solicita.TG.UI.View.RequerimentoService;
using Solicita.TG.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Web;
using System.Web.Mvc;

namespace Solicita.TG.UI.View.Controllers
{
    [LoginFilter]
    public class RequerimentoController : Controller
    {

        RequerimentoService.RequerimentoServiceClient service = new RequerimentoService.RequerimentoServiceClient();
        private AcessoService.AcessoServiceClient Acessoclient = new AcessoService.AcessoServiceClient();


        //
        // GET: /Requerimento/

        public ActionResult Index()
        {
            return View();
        }
        public ActionResult IndexAluno()
        {
            return View("RequerimentoAluno");
        }

        public JsonResult ListarMotivos()
        {
            var motivos = service.ListarMotivos();

            return Json(motivos, JsonRequestBehavior.AllowGet);
        }

        public JsonResult TransferirResponsabilidade(Guid requerimento, String observacao, Guid idResponsavel, Int32 tipoResponsavel) 
        {
            try
            {
                service.TransferirResponsabilidade(requerimento, idResponsavel, tipoResponsavel, observacao);

                var result = new { Success = true, Message = "OK" };

                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (FaultException e)
            {
                var result = new { Success = false, Message = e.Message };

                return Json(result, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult Concluir(Guid idRequerimento, Guid idResponsavel, Int32 tipoResponsavel)
        {
            try
            {
                service.Concluir(idRequerimento, idResponsavel, tipoResponsavel);

                var result = new { Success = true, Message = "OK" };

                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (FaultException e)
            {
                var result = new { Success = false, Message = e.Message };

                return Json(result, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult Cancelar(Guid idRequerimento, String motivo, Guid idResponsavel, Int32 tipoResponsavel)
        {
            try
            {
                service.Cancelar(idRequerimento, motivo, idResponsavel, tipoResponsavel);

                var result = new { Success = true, Message = "OK" };

                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (FaultException e)
            {
                var result = new { Success = false, Message = e.Message };

                return Json(result, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult Indeferir(Guid idRequerimento, String motivo, Guid idResponsavel, Int32 tipoResponsavel)
        {
            try
            {
                service.Indeferir(idRequerimento, motivo, idResponsavel, tipoResponsavel);

                var result = new { Success = true, Message = "OK" };

                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (FaultException e)
            {
                var result = new { Success = false, Message = e.Message };

                return Json(result, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult Solicitar(String aluno, String ra, String rg, Int32 turno, Guid curso, Int32 periodo)
        {
            return Json("OK");
        }
        
        #region Ações de View

        public ActionResult SolicitacaoDeDocumentos()
        {
            return View();
        }

        public ActionResult ListarSolicitacoesEmAberto(String RA)
        {
            List<RequerimentoService.RequerimentoSimplesModel> requerimentos = new List<RequerimentoService.RequerimentoSimplesModel>();
            requerimentos = service.List(RA, String.Empty, Guid.Empty, 0, "SolicitacaoDeDocumentos");

            return PartialView("SolicitacoesEmAberto", requerimentos);
        }

        public ActionResult ListarSolicitacoesJaAtendidas(String RA)
        {
            List<RequerimentoService.RequerimentoSimplesModel> requerimentos = new List<RequerimentoService.RequerimentoSimplesModel>();
            requerimentos = service.ListJáAtendidas(RA, String.Empty, Guid.Empty, 0, "SolicitacaoDeDocumentos");

            return PartialView("SolicitacoesJaAtendidas", requerimentos);
        }

        public ActionResult ListarSolicitacaoDeDocumentos(Int32 pagina)
        {
            int take = 10;
            int Skip = take * (pagina - 1);


            List<RequerimentoService.RequerimentoSimplesModel> requerimentos = new List<RequerimentoService.RequerimentoSimplesModel>();
            requerimentos = service.List(String.Empty, String.Empty, Guid.Empty, 0, "SolicitacaoDeDocumentos");

            Int32 NumeroPaginas = Convert.ToInt32(Math.Ceiling((double)requerimentos.Count / take));
            Int32 PaginaAtual = pagina;


            foreach (var req in requerimentos)
            {
                req.NumeroPaginas = NumeroPaginas;
                req.PaginaAtual = PaginaAtual;
            }
           


            return PartialView("ListSolicitacaoDeDocumentos", requerimentos.Skip(Skip).Take(take).ToList());
        }
        
        public ActionResult SolicitacaoDeDocumentosIndex()
        {
            return PartialView("SolicitacaoDeDocumentos");
        }
        
        #endregion Ações de View

        #region DeclaraçãoDeMatricula

        public ActionResult CriarSolicitacaoDeDocumentos(Guid IdAluno, Int32 TipoDoMotivo, Int32 TipoDeDocumento, String EspecificacaoDoMotivo,
            Boolean InformarSemestreAtual, Boolean InformarCargaHoraria, Boolean InformarAulaAosSabados, Boolean InformarPrevisaoDeConclusao,
            Boolean InformarDisciplina, Boolean InformarPeriodo, Guid DisciplinaInformada)
        {
            try
            {
                Guid id = service.CriarSolicitacoesDeDocumentos(IdAluno, TipoDoMotivo, TipoDeDocumento, EspecificacaoDoMotivo,
                    InformarSemestreAtual, InformarCargaHoraria, InformarAulaAosSabados, InformarPrevisaoDeConclusao, InformarDisciplina, InformarPeriodo, DisciplinaInformada);

                return EditSolicitacaoDeDocumentos(id);
            }
            catch (MainException e)
            {
                var result = new { Success = false, Message = e.Error };

                return Json(result, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult EditSolicitacaoDeDocumentos(Guid id)
        {
            var model = service.GetSolicitacaoDeDocumentos(id, "SolicitacaoDeDocumentos");

            var requerimentoParseado = (RequerimentoService.SolicitacaoDeDocumentosModel)model;

            AcessoService.InfoDeLogon logon = (AcessoService.InfoDeLogon)Session["Login"];

            if(logon.TipoDeUsuario.Tipo != 1)
                return PartialView("SolicitacaoDeDocumentos", requerimentoParseado);
            else
                return PartialView("SolicitacaoDeDocumentosAluno", requerimentoParseado);
        }

        public ActionResult EditSolicitacaoDeDocumentosAluno(Guid id)
        {
            var model = service.GetSolicitacaoDeDocumentos(id, "SolicitacaoDeDocumentos");

            var requerimentoParseado = (RequerimentoService.SolicitacaoDeDocumentosModel)model;

            return PartialView("SolicitacaoDeDocumentosAluno", requerimentoParseado);
        }

        #endregion

        public JsonResult ListarAlunos(String RA)
        {
            AlunoService.AlunoServiceClient alunoService = new AlunoService.AlunoServiceClient();

            var alunos = alunoService.Listar(RA);


            if (alunos.Count == 0)
            {
                return Json(new AlunoService.AlunoModel() { Nome = String.Empty, RG = String.Empty, Sobrenome = String.Empty }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                var aluno = alunos.SingleOrDefault(x => x.Matricula.RA.Equals(RA));

                return Json(aluno, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult ListarTipoDeStatusJson()
        {
            var model = service.ListarTipoDeStatus();

            var data = model
                 .Select(r => new { r.Id, value = r.Value })
                 .ToArray();
            return Json(data, JsonRequestBehavior.AllowGet);


        }


        public class AlunoMock
        {
            public Guid Id { get; set; }
            public String Nome { get; set; }
            public String RA { get; set; }
            public String RG { get; set; }
            public String Sobrenome { get; set; }

            public AlunoMock()
            {
                Id = Guid.NewGuid();
                Nome = String.Empty;
                RA = String.Empty;
            }
        }

        public JsonResult ListarTipos()
        {

            List<RequerimentoService.TiposRequerimentoModel> model = service.ListarTipos();


            var data = model
              .Select(r => new { Id = r.Identificador, value = r.Tipo })
              .ToArray();
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        
    }
}
