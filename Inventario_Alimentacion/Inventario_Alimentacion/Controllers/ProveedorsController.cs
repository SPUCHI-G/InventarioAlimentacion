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
    public class ProveedorsController : Controller
    {
        private BD_InventarioEntities db = new BD_InventarioEntities();
        // GET: Proveedors
        [AuthorizeUser(idOperacion: 2)]
        public ActionResult BusquedaFilter(String Nombre, String Id)
        {
            
            var pro = from s in db.Proveedor select s;
            if (!String.IsNullOrEmpty(Nombre))
            {
                pro = pro.Where(J => J.Prov_Nombre.Contains(Nombre));
            }

            if (!String.IsNullOrEmpty(Id))
            {
                Int32 Identi = Convert.ToInt32(Id);
                return View(pro.Where(x => x.Prov_Docum == Identi));
            }
            return View(pro.ToList());
        }

        // GET: Proveedors/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Proveedor proveedor = db.Proveedor.Find(id);
            if (proveedor == null)
            {
                return HttpNotFound();
            }
            return View(proveedor);
        }

        // GET: Proveedors/Create
        public ActionResult Create()
        {
            LlenarLista();
            return View();
            
        }
        public ActionResult LlenarLista()
        {
            using (BD_InventarioEntities db2 = new BD_InventarioEntities())
            {

                List<Tipo_Documento> Lista = new List<Tipo_Documento>();
                Lista = db2.Tipo_Documento.ToList();
                ViewBag.ListaDocumentos = Lista;

                List<Ciudad> Lista2 = new List<Ciudad>();
                Lista2 = db2.Ciudad.ToList();
                ViewBag.ListaCiudades = Lista2;

            }
            return View();

            
        }
        // crear drop down list de tipos de cedula
        

        // POST: Proveedors/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "fk_TipoDocum,fk_Ciudad,Prov_Docum,Prov_Nombre,Prov_Telefono,Prov_Celular,Prov_Email,Prov_Direccion,Prov_Fax,Prov_ActiEcono")] Proveedor proveedor)
        {
            
            if (ModelState.IsValid)
            {
                db.Proveedor.Add(proveedor);
                db.SaveChanges();
                return RedirectToAction("BusquedaFilter");
            }

            return View(proveedor);
        }

        // GET: Proveedors/Edit/5
        public ActionResult Edit(int? id)
        {
            LlenarLista();
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Proveedor proveedor = db.Proveedor.Find(id);
            if (proveedor == null)
            {
                return HttpNotFound();
            }
            return View(proveedor);
        }

        // POST: Proveedors/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "fk_TipoDocum,fk_Ciudad,Prov_Docum,Prov_Nombre,Prov_Telefono,Prov_Celular,Prov_Email,Prov_Direccion,Prov_Fax,Prov_ActiEcono")] Proveedor proveedor)
        {
            if (ModelState.IsValid)
            {
                db.Entry(proveedor).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("BusquedaFilter");
            }
            return View(proveedor);
        }

        // GET: Proveedors/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Proveedor proveedor = db.Proveedor.Find(id);
            if (proveedor == null)
            {
                return HttpNotFound();
            }
            return View(proveedor);
        }

        // POST: Proveedors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {

            var obj = from s in db.Detalle_Entrada select s;
            obj = obj.Where(J => J.fk_Proveedor ==id);
            if (!obj.Any()) //convierte el comando en bolleano
            {
                Proveedor proveedor = db.Proveedor.Find(id);
                db.Proveedor.Remove(proveedor);
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
