using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Solicita.TG.UI.View.Controllers
{
    public class MyFaultException : ApplicationException
    {
        public JsonResult Error { get; private set; }

        public MyFaultException(JsonResult error)
        {
            this.Error = error;
        }
    }
}