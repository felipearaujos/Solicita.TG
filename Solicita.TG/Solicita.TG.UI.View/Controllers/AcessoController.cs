using Solicita.TG.UI.View.AcessoService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Solicita.TG.UI.View.Controllers
{
    public class AcessoController : Controller
    {


        private AcessoService.AcessoServiceClient client = new AcessoService.AcessoServiceClient();

        // GET: Acesso
        public ActionResult Index()
        {
            return View("Login");
        }


        public ActionResult Acessos()
        {
            return View("Listar");
        }

        public ActionResult AcessarComoAluno()
        {
            return View("AcessoAluno");
        }

        public ActionResult RecuperacaoDeSenha()
        {
            return View("RecuperacaoDeSenha");
        }

        public ActionResult Criar()
        {
            AcessoService.AcessoModel modelo = new AcessoService.AcessoModel();
            modelo.Id = Guid.Empty;
            modelo.TipoDeUsuario = new AcessoService.AcessoModel.TipoDeUsuarioModel(){ Tipo = 1, NomeDoTipo = "Não Definido" };
            modelo.Usuario = String.Empty;
            modelo.Senha = String.Empty;

            return View("Usuario", modelo);
        }

        public JsonResult Salvar(Guid Id, String Usuario, String Senha, Int32 Tipo)
        {
            // revalidar

            AcessoService.AcessoModel modelo = new AcessoService.AcessoModel();

            if (Id.Equals(Guid.Empty))
                client.CriarAcesso(Id, Usuario, Senha, Tipo);
            else
                client.SalvarAcesso(Id, Id, Usuario, Senha, Tipo);
            return Json("OK");
        }

        public JsonResult ListarAcessos()
        {
            var acessos = client.ListarTodosAcessos();

            return Json(acessos);
        }

        public JsonResult ListarTipos()
        {
            var tipos = client.ListarTipos();

            var data = tipos
                  .Select(r => new { Id = r.Value, value = r.Text })
                  .ToArray();

            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Editar(Guid Id)
        {
            AcessoService.AcessoModel modelo = client.Get(Id);

            return View("Usuario", modelo);
        }

        public ActionResult Logoff()
        {
            Session["Login"] = null;

            return View("Login");
        }

        public JsonResult MostrarInfosDoUsuario()
        {
            AcessoService.InfoDeLogon usuarioLogado = (AcessoService.InfoDeLogon)Session["Login"];
            
            AcessoService.UsuarioLogado infosDeAcesso = client.BuscarUsuarioLogado(usuarioLogado.Login.ToString());

            return Json(infosDeAcesso);
        }

        public JsonResult RealizarLogin(String Usuario, String Senha, Int32 TipoDeUsuario)
        {
            InfoDeLogon infosDeAcesso  = client.Login(Usuario, Senha, TipoDeUsuario);
     
            if (infosDeAcesso.AcessoLiberado)
            {
                Session["Login"] = infosDeAcesso;
            }

            
            return Json(infosDeAcesso);
        }

        public JsonResult RealizarLoginComoAluno(String RA, String RG, String Senha)
        {
            var infosDeAcesso = client.LogarComoALuno(RA, RG, Senha);

            if (infosDeAcesso.AcessoLiberado)
                Session["Login"] = infosDeAcesso;

            return Json(infosDeAcesso);
        }

        public JsonResult RecuperarSenha(String RA, String RG, String Email)
        {
            var retorno = client.RecuperarSenha(RA, RG, Email);

            return Json(retorno);
        }
    }
}
