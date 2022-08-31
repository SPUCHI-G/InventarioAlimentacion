using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Inventario_Alimentacion.Models;

namespace Inventario_Alimentacion.Controllers
{
    public class SubcategoriasController : Controller
    {
        private BD_InventarioEntities db = new BD_InventarioEntities();

        // GET: Subcategorias
        
        public ActionResult BusquedaFilter2(String sub)
        {
            var obj = from s in db.Subcategoria select s;
            if (!String.IsNullOrEmpty(sub))
            {
                obj = obj.Where(J => J.Subca_Nombre.Contains(sub));
            }

            return View(obj.ToList());
        }


        // GET: Subcategorias/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Subcategoria subcategoria = db.Subcategoria.Find(id);
            if (subcategoria == null)
            {
                return HttpNotFound();
            }
            return View(subcategoria);
        }
        public ActionResult LlenarLista()
        {
            using (BD_InventarioEntities db2 = new BD_InventarioEntities())
            {
                List<Categoria> Lista = new List<Categoria>();
                Lista = db2.Categoria.ToList();
                ViewBag.ListaCategoria = Lista;
            }
            return View();


        }
        // GET: Subcategorias/Create
        // POST: Subcategorias/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
       
          
        public ActionResult Create(String  Categoria,String Sub_Nombre)
        {
            LlenarLista();

            if (!String.IsNullOrEmpty(Sub_Nombre)&& !String.IsNullOrEmpty(Categoria))
            {
                Int32 Cat = Int32.Parse(Categoria);
                var obj = from s in db.Subcategoria select s;
                obj = obj.Where(J => J.Subca_Nombre.Contains(Sub_Nombre));
                if (!obj.Any()) //convierte el comando en bolleano
                {
                    db.Database.ExecuteSqlCommand("spCrearSubcategoria @Id_Cat,@Nombre",
                    new SqlParameter("@Id_Cat", Cat),
                   new SqlParameter("@Nombre", Sub_Nombre));

                    return RedirectToAction("BusquedaFilter2");
                }
                return View();
            }
                return View();
        }

        // GET: Subcategorias/Edit/5


        // POST: Subcategorias/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        public ActionResult Edit(int? id)
        {   LlenarLista();
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Subcategoria subcategoria = db.Subcategoria.Find(id);
            if (subcategoria == null)
            {
                return HttpNotFound();
            }
            return View(subcategoria);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID_Subcategoria,Categoria,Subca_Nombre")] Subcategoria subcategoria)
        {
            if (ModelState.IsValid)
            {
                db.Entry(subcategoria).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("BusquedaFilter2");
            }
            return View(subcategoria);
        }


        // GET: Subcategorias/Delete/5
        public ActionResult Delete(int? id)
        {
            LlenarLista();
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Subcategoria subcategoria = db.Subcategoria.Find(id);
            if (subcategoria == null)
            {
                return HttpNotFound();
            }
            return View(subcategoria);
        }

        // POST: Subcategorias/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {   LlenarLista();
            var obj = from s in db.Producto select s;
            obj = obj.Where(J => J.fk_Subcategoria.Equals(id)); // verifica que aun no se han creado relaciones en la tabla producto
            if (!obj.Any()) //convierte el comando en bolleano
            {
                
                Subcategoria subcategoria = db.Subcategoria.Find(id);
                db.Subcategoria.Remove(subcategoria);
                db.SaveChanges();
                return RedirectToAction("BusquedaFilter2");
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
