using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Inventario_Alimentacion.Filters;
using Inventario_Alimentacion.Models;

namespace Inventario_Alimentacion.Controllers
{
    public class CategoriasController : Controller
    {
        private BD_InventarioEntities db = new BD_InventarioEntities();

        [AuthorizeUser(idOperacion: 2)]
        public ActionResult BusquedaFilter(String cat)
        {
            var obj = from s in db.Categoria select s;
            if (!String.IsNullOrEmpty(cat))
            {
                obj = obj.Where(J => J.Cat_Nombre.Contains(cat));
            }

            return View(obj.ToList());
        }
        
        
        
        // GET: Categorias/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Categoria categoria = db.Categoria.Find(id);
            if (categoria == null)
            {
                return HttpNotFound();
            }
            return View(categoria);
        }


        // GET: Categorias/Create


        // POST: Categorias/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 

        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        
        public ActionResult Create(String Cat_Nombre)
        {
            if (!String.IsNullOrEmpty(Cat_Nombre))
            {

                var obj = from s in db.Categoria select s;
                obj=obj.Where(J => J.Cat_Nombre.Contains(Cat_Nombre));
                    if (!obj.Any()) //convierte el comando en bolleano
                {
                    db.Database.ExecuteSqlCommand("exec spCrearCategoria @Nombre", new SqlParameter("@Nombre", Cat_Nombre));
                    return RedirectToAction("BusquedaFilter");
                }
            } 
            return View(); 
        }
        

        // GET: Categorias/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            } 
            Categoria categoria = db.Categoria.Find(id);
            if (categoria == null)
            {
                return HttpNotFound();
            }
            return View(categoria);
        }

        // POST: Categorias/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID_Categoria,Cat_Nombre")] Categoria categoria)
        {
            if (ModelState.IsValid)
            {
                db.Entry(categoria).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("BusquedaFilter");
            }
            return View(categoria);
        }

        // GET: Categorias/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Categoria categoria = db.Categoria.Find(id);
            if (categoria == null)
            {
                return HttpNotFound();
            }
            return View(categoria);
        }

        // POST: Categorias/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var obj = from s in db.Producto select s;
            obj = obj.Where(J => J.fk_Categoria.Equals(id)); // verifica que aun no se han creado relaciones en la tabla producto
            if (!obj.Any()) //convierte el comando en bolleano
            {
                Categoria categoria = db.Categoria.Find(id);
                db.Categoria.Remove(categoria);
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
