using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Inventario_Alimentacion.Filters;
using Inventario_Alimentacion.Models;

namespace Inventario_Alimentacion.Controllers
{
    public class UsuariosController : Controller
    {
        private BD_InventarioEntities db = new BD_InventarioEntities();

        // GET: Usuarios
        [AuthorizeUser(idOperacion: 1)]
        public ActionResult BusquedaFilter(String NomUsuario)
        {
            var obj = from s in db.Usuario select s;
            if (!String.IsNullOrEmpty(NomUsuario))
            {
                obj = obj.Where(J => J.Usu_Nombre.Contains(NomUsuario));
            }
            return View(obj.ToList());
        }
        
        public ActionResult LlenarLista()
        {
            using (BD_InventarioEntities db2 = new BD_InventarioEntities())
            {
                List<Empleado> Lista = new List<Empleado>();
                Lista = db2.Empleado.ToList();
                ViewBag.ListaEmpleados = Lista;

            }
            return View();

        }

        // GET: Usuarios/Create
        public ActionResult Create()
        {
            LlenarLista();
            return View();
        }

        // POST: Usuarios/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Usu_Nombre,Usu_Contraseña,Usu_Activo,ID_Empleado")] Usuario usuario)
        {   
            if (ModelState.IsValid)
            {

                var obj = from s in db.Usuario select s;
                obj=obj.Where(x => x.ID_Empleado == usuario.ID_Empleado);

                if(obj.Count() == 0)
                {
                    db.Usuario.Add(usuario);
                    db.SaveChanges();
                    return RedirectToAction("BusquedaFilter");
                }
            }
            LlenarLista();
            return View(usuario);
        }


        // GET: Usuarios/Edit/5
        public ActionResult Edit(string id)
        {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Usuario usuario = db.Usuario.Find(id);
                if (usuario == null)
                {
                    return HttpNotFound();
                }
                return View(usuario);
        }

        // POST: Usuarios/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Usu_Nombre,Usu_Contraseña,Usu_Activo,ID_Empleado")] Usuario usuario)
            
        {
            if (ModelState.IsValid)
            {
                db.Entry(usuario).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("BusquedaFilter");
            }
            return View();
        }

        // GET: Usuarios/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Usuario usuario = db.Usuario.Find(id);
            if (usuario == null)
            {
                return HttpNotFound();
            }
            return View(usuario);
        }

        // POST: Usuarios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            var ID = Int32.Parse(id);
            var obj = from s in db.Entrada select s;
            obj = obj.Where(x => x.fk_Empleado == ID );

            if (obj.Count() == 0)
            {
                Usuario usuario = db.Usuario.Find(id);
                db.Usuario.Remove(usuario);
                db.SaveChanges();
                return RedirectToAction("BusquedaFilter");
            }

            return RedirectToAction("BusquedaFilter");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
