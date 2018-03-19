using Solicita.TG.UI.View.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Channels;
using System.Web;
using System.Web.Mvc;
using System.Web.SessionState;

namespace Solicita.TG.UI.View.Filters
{
    public class LoginFilter : System.Web.Mvc.ActionFilterAttribute, System.Web.Mvc.IActionFilter
    {
        void IActionFilter.OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (filterContext.HttpContext.Session.Contents["Login"] == null)
            {
                Login(filterContext);
            }
        }

        void IActionFilter.OnActionExecuted(ActionExecutedContext filterContext)
        {
            
        }

        public void Login(ActionExecutingContext filterContext)
        {
            filterContext.HttpContext.Response.Redirect("~/Acesso");
        }
    }
}