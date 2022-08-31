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
    public class Nom_ProductoController : Controller
    {
        private BD_InventarioEntities db = new BD_InventarioEntities();
        
        // GET: Nom_Producto
        
        public ActionResult BusquedaFilter3(String nom)
        {
            var obj = from s in db.Nom_Producto select s;
            if (!String.IsNullOrEmpty(nom))
            {
                obj = obj.Where(J => J.NomProducto.Contains(nom));
            }

            return View(obj.ToList());
        }

        public ActionResult LlenarLista()
        {
            using (BD_InventarioEntities db2 = new BD_InventarioEntities())
            {
                List<Subcategoria> Lista = new List<Subcategoria>();
                Lista = db2.Subcategoria.ToList();
                ViewBag.ListaSubcategoria = Lista;
            }
            return View();


        }
        // GET: Nom_Producto/Create

        
        public ActionResult Create()
        {
            LlenarLista();
            return View();
        }
        // POST: Nom_Producto/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(String Subcategoria, String NomProducto,decimal Min, decimal Punto_pedido, bool Prop_Adicion)
        {
            LlenarLista();

            if (!String.IsNullOrEmpty(Subcategoria) && !String.IsNullOrEmpty(NomProducto) && !Min.Equals(null) && !Punto_pedido.Equals(null) && !Prop_Adicion.Equals(null))
            {
                Int32 Cat = Int32.Parse(Subcategoria);
                var obj = from s in db.Nom_Producto select s;
                obj = obj.Where(J => J.NomProducto.Contains(NomProducto));
                if (!obj.Any()) //convierte el comando en bolleano
                {
                    db.Database.ExecuteSqlCommand("spCrearNom_Producto @Id_Sub,@Nombre,@Min,@Punto_pedido,@Prop_Adicion",
                    new SqlParameter("@Id_Sub", Cat),
                   new SqlParameter("@Nombre", NomProducto),
                   new SqlParameter("@Min", Min),
                   new SqlParameter("@Punto_pedido", Punto_pedido),
                   new SqlParameter("@Prop_Adicion", Prop_Adicion));

                    return RedirectToAction("BusquedaFilter3");
                }
                return View();
            }
            return View();
        }

        // GET: Nom_Producto/Edit/5
        public ActionResult Edit(int? id)
        { LlenarLista();

            
            if (id == null)
            {   
                
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Nom_Producto nom_Producto = db.Nom_Producto.Find(id);
            if (nom_Producto == null)
            {
                return HttpNotFound();
            }
            return View(nom_Producto);
        }

        // POST: Nom_Producto/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID_NomProducto,Subcategoria,NomProducto,Min,Punto_pedido,Prop_Adicion")] Nom_Producto nom_Producto)
        {
            if (ModelState.IsValid)
            {
                db.Entry(nom_Producto).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("BusquedaFilter3");
            }
            return View(nom_Producto);
        }

        // GET: Nom_Producto/Delete/5
        public ActionResult Delete(int? id)
        {
            LlenarLista();
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Nom_Producto nom_Producto = db.Nom_Producto.Find(id);
            if (nom_Producto == null)
            {
                return HttpNotFound();
            }
            return View(nom_Producto);
        }

        // POST: Nom_Producto/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            LlenarLista();
            var obj = from s in db.Producto select s;
            obj = obj.Where(J => J.fk_NomProducto.Equals(id)); // verifica que aun no se han creado relaciones en la tabla producto
            if (!obj.Any()) //convierte el comando en bolleano
            {
                Nom_Producto nom_Producto = db.Nom_Producto.Find(id);
                db.Nom_Producto.Remove(nom_Producto);
                db.SaveChanges();
                return RedirectToAction("BusquedaFilter3");
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
