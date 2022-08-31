using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Inventario_Alimentacion.Filters;
using Inventario_Alimentacion.Models;

namespace Inventario_Alimentacion.Controllers
{
    public class DonantesController : Controller
    {
        private BD_InventarioEntities db = new BD_InventarioEntities();

        // GET: Donantes
        [AuthorizeUser(idOperacion: 2)]
        public ActionResult BusquedaFilter(String Nombre, String Id)
        {

            var Usu = from s in db.Donante select s;
            if (!String.IsNullOrEmpty(Nombre))
            {
                Usu = Usu.Where(J => J.Don_Nombre.Contains(Nombre));
            }

            if (!String.IsNullOrEmpty(Id))
            {
                Int32 Identi = Convert.ToInt32(Id);
                return View(Usu.Where(x => x.ID_Donante == Identi));
            }
            return View(Usu.ToList());

        }
        // GET: Donantes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Donante donante = db.Donante.Find(id);
            if (donante == null)
            {
                return HttpNotFound();
            }
            return View(donante);
        }

        // GET: Donantes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Donantes/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID_Donante,Don_Nombre")] Donante donante)
        {
            if (ModelState.IsValid)
            {
                db.Donante.Add(donante);
                db.SaveChanges();
                return RedirectToAction("BusquedaFilter");
            }

            return View(donante);
        }

        // GET: Donantes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Donante donante = db.Donante.Find(id);
            if (donante == null)
            {
                return HttpNotFound();
            }
            return View(donante);
        }

        // POST: Donantes/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID_Donante,Don_Nombre")] Donante donante)
        {
            if (ModelState.IsValid)
            {
                db.Entry(donante).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("BusquedaFilter");
            }
            return View(donante);
        }

        // GET: Donantes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Donante donante = db.Donante.Find(id);
            if (donante == null)
            {
                return HttpNotFound();
            }
            return View(donante);
        }

        // POST: Donantes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var obj = from s in db.Detalle_Entrada select s;
            obj = obj.Where(J => J.fk_Donante == id);
            if (!obj.Any()) //convierte el comando en bolleano
            {
                Donante donante = db.Donante.Find(id);
                db.Donante.Remove(donante);
                db.SaveChanges();
                return RedirectToAction("BusquedaFilter");
            }
            else
            {
                return RedirectToAction("Delete");
            }
            
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
