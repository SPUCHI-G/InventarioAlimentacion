using Inventario_Alimentacion.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Inventario_Alimentacion.Controllers
{
    public class CerrarController : Controller
    {

        private BD_InventarioEntities db = new BD_InventarioEntities();
        // GET: Cerrar
        public ActionResult Logoff()
        {   
            EliminarTemporales();
            Session["User"]=null;
            return RedirectToAction("Login", "Accesso");
            
        }
        public void EliminarTemporales()
        {
            db.Database.ExecuteSqlCommand("delete from Temporal");
            db.Database.ExecuteSqlCommand("delete from Temporal2");
        }
    }
}