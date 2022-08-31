
using Inventario_Alimentacion.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Inventario_Alimentacion.Filters
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    public class AuthorizeUser : AuthorizeAttribute
    {
        private Usuario oUsuario;
        private BD_InventarioEntities db = new BD_InventarioEntities();
        private int idOperacion;

        public AuthorizeUser(int idOperacion = 0)
        {
            this.idOperacion = idOperacion;
        }

        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            try
            {
                oUsuario = (Usuario)HttpContext.Current.Session["User"];

                var lstMisOperaciones = db.Database.SqlQuery<spValidaUsuario_Result>("spValidaUsuario @usuario,@permiso",
                                                                                new SqlParameter("@usuario", oUsuario.Usu_Nombre),
                                                                                new SqlParameter("@permiso", idOperacion));
                if (lstMisOperaciones.ToList().Count() == 0)
                {
                    filterContext.Result = new RedirectResult("~/Error/UnauthorizedOperation?operacion="+ idOperacion + "&msjeErrorExcepcion=");
                }
            }
            catch (Exception ex)
            {
                filterContext.Result = new RedirectResult("~/Error/UnauthorizedOperation?operacion=" + idOperacion + "&msjeErrorExcepcion=" + ex.Message);
            }
        }

    }
}