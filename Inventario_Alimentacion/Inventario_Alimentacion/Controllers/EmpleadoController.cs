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
    public class EmpleadoController : Controller
    {
        private BD_InventarioEntities db = new BD_InventarioEntities();
        // GET: Empleado
        [AuthorizeUser(idOperacion: 1)]
        public ActionResult BusquedaFilter(String Nombre, String Id)
        {
            
            var Usu = from s in db.Empleado select s;
            if (!String.IsNullOrEmpty(Nombre))
            {
                Usu = Usu.Where(J => J.Emp_Nombre.Contains(Nombre));
            }

            if (!String.IsNullOrEmpty(Id))
            {
                Int32 Identi = Convert.ToInt32(Id);
                return View(Usu.Where(x => x.Emp_Documento == Identi));
            }
            return View(Usu.ToList());

        }
        
        public ActionResult LlenarLista()
        {
            using (BD_InventarioEntities db2 = new BD_InventarioEntities())
            {
                List<Cargo> Lista = new List<Cargo>();
                Lista = db2.Cargo.ToList();
                ViewBag.ListaCargos = Lista;
            }
            return View();


        }

        // GET: Empleado/Create
        public ActionResult Create()
        {
            LlenarLista();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public ActionResult Create([Bind(Include = "Emp_Documento,Emp_Nombre,Emp_Apellido,Emp_Telefono,Emp_Celular,Emp_Email,fk_Cargo,Emp_Area")] Empleado empleado)
        {

            if (ModelState.IsValid)
            {
                
                db.Empleado.Add(empleado);
                db.SaveChanges();
                return RedirectToAction("BusquedaFilter");
            }
            LlenarLista();
            return View(empleado);
        }

        // GET: Empleado/Edit/5
        public ActionResult Edit(int? id)
        {
            LlenarLista();
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Empleado empleado = db.Empleado.Find(id);
            if (empleado == null)
            {
                return HttpNotFound();
            }
            return View(empleado);
        }

        // POST: Empleado/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Emp_Documento,Emp_Nombre,Emp_Apellido,Emp_Telefono,Emp_Celular,Emp_Email,fk_Cargo,Emp_Area")] Empleado empleado)
        {
            if (ModelState.IsValid)
            {
                db.Entry(empleado).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("BusquedaFilter");
            }
            LlenarLista();
            return View(empleado);
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
