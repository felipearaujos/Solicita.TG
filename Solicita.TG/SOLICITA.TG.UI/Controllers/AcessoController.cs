using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Solicita.TG.UI.Controllers
{
    public class AcessoController : Controller
    {
        // GET: Acesso
        public ActionResult Index()
        {
            return View("Login");
        }

        public JsonResult RealizarLogin(String Usuario, String Senha, Int32 TipoDeUsuario)
        {
            AcessoService.AcessoServiceClient client = new AcessoService.AcessoServiceClient();

            var infosDeAcesso = client.Login(Usuario, Senha, TipoDeUsuario);

            return Json(infosDeAcesso);
        }
    }
}