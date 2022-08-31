using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Inventario_Alimentacion.Controllers
{
    public class AccessoController : Controller
    {
        // GET: Accesso
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(string User, string Pass)
        {
            try
            {
                using (Models.BD_InventarioEntities db = new Models.BD_InventarioEntities())
                {
                    var oUser = (from d in db.Usuario
                                 where d.Usu_Nombre == User.Trim() && d.Usu_Contraseña == Pass.Trim() && d.Usu_Activo==1
                                 select d).FirstOrDefault();
                    if (oUser == null)
                    {
                        ViewBag.Error = "Usuario o coontraseña invalida";
                        return View();
                    }
                    
                    // se guarda la sesion
                    Session["User"] = oUser;
                }
                return RedirectToAction("Index", "Home");
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View();
            }
        }
    }
}
