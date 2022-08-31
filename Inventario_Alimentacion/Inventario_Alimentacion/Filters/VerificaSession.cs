using Inventario_Alimentacion.Controllers;
using Inventario_Alimentacion.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Inventario_Alimentacion.Filters
{
    public class VerificaSession : ActionFilterAttribute
    {
        private Usuario oUsuario;
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            try
            {
                base.OnActionExecuting(filterContext);
                oUsuario = (Usuario)HttpContext.Current.Session["User"];
                if(oUsuario == null)
                {
                    if(filterContext.Controller is AccessoController == false )
                    {
                        filterContext.HttpContext.Response.Redirect("/Accesso/Login");
                    }

                }
            }
            catch (Exception)
            {
                filterContext.Result = new RedirectResult("~/Accesso/Login");
            }
        }
    }
}