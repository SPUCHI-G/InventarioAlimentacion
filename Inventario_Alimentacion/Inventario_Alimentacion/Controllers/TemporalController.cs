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
    public class TemporalController : Controller
    {
        private BD_InventarioEntities db = new BD_InventarioEntities();

        private List<Categoria> GetCategoriaList()
        {
            BD_InventarioEntities db = new BD_InventarioEntities();
            List<Categoria> Cat = db.Categoria.ToList();
            return Cat;
        }
        // llena el dropdownlist de subcategoria
        public ActionResult GetSubcategoriaList(int ID_Categoria)
        {
            BD_InventarioEntities db = new BD_InventarioEntities();
            List<Subcategoria> Sub = db.Subcategoria.Where(m => m.Categoria == ID_Categoria).ToList();
            ViewBag.SubcategoriaList = new SelectList(Sub, "ID_Subcategoria", "Subca_Nombre");
            return PartialView("DisplaySubcategoria");
        }
        // llena el dropdownlist de Nombres del producto
        public ActionResult GetNom_ProductoList(int ID_Subcategoria)
        {
            BD_InventarioEntities db = new BD_InventarioEntities();
            List<Nom_Producto> Nom = db.Nom_Producto.Where(m => m.Subcategoria == ID_Subcategoria).ToList();
            ViewBag.NomProductoList = new SelectList(Nom, "ID_NomProducto", "NomProducto");
            return PartialView("DisplayProducto");
        }
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Temporal temporal = db.Temporal.Find(id);
            if (temporal == null)
            {
                return HttpNotFound();
            }
            ViewBag.CategoriaList = new SelectList(GetCategoriaList(), "ID_Categoria", "Cat_Nombre");
            ViewBag.fk_Donante = new SelectList(db.Donante, "ID_Donante", "Don_Nombre", temporal.fk_Donante);
            ViewBag.fk_Proveedor = new SelectList(db.Proveedor, "Prov_Docum", "Prov_Nombre", temporal.fk_Proveedor);
            ViewBag.fk_TipoEntrada = new SelectList(db.Tipo_Entrada, "ID_TipoEntrada", "Tipo_Nombre", temporal.fk_TipoEntrada);
            return View(temporal);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(long ID_Det_entr, int  fk_Entrada,int ID_Categoria, int ID_Subcategoria, int ID_NomProducto, decimal Ent_Cantidad_Peso, decimal Ent_Temperatura,
            string Ent_Factura, decimal Ent_Precio_Unitario, decimal Ent_Precio_Total, decimal Ent_iva, decimal Ent_Descuento, int fk_TipoEntrada, int fk_Proveedor, int fk_Donante, String Ent_FechVencim)
        {
            if (!ID_Categoria.Equals(null) && !ID_Subcategoria.Equals(null) && !ID_NomProducto.Equals(null) && !Ent_Cantidad_Peso.Equals(null) && !Ent_Temperatura.Equals(null) &&
                    !Ent_Factura.Equals(null) && !Ent_Precio_Unitario.Equals(null) && !Ent_Precio_Total.Equals(null) && !Ent_iva.Equals(null) && !Ent_Descuento.Equals(null) &&
                    !fk_TipoEntrada.Equals(null) && !fk_Proveedor.Equals(null) && !fk_Donante.Equals(null))
            {
                var returnCode = new SqlParameter();
                returnCode.ParameterName = "@Id";
                returnCode.SqlDbType = SqlDbType.Int;
                returnCode.Direction = ParameterDirection.Output;
                // se ejecuta el SP
                db.Database.ExecuteSqlCommand("spCrearProducto @idc, @ids,@idp,@Id OUT", // primero se crea la combinacion en tabla producto si aun no existe
                                     new SqlParameter("@idc", ID_Categoria),
                                     new SqlParameter("@ids", ID_Subcategoria),
                                     new SqlParameter("@idp", ID_NomProducto),
                                     returnCode);
                var Id = (int)returnCode.Value;//retorna el id del producto para guardarlo con los datos de detalle entrada en la tabla temporal
                DateTime F;
                if (String.IsNullOrEmpty(Ent_FechVencim))
                {
                    Ent_FechVencim = "9999/12/30"; // para evitar algun tipo de error al llenar, no se pueden enviar nullos desde EF
                    F = DateTime.Parse(Ent_FechVencim);
                }
                else
                {
                    F = DateTime.Parse(Ent_FechVencim);
                }

                // se hace el insert a travez de un SP para evitar errores con laPK de la  Tabla Detalle_Entrada
                db.Database.ExecuteSqlCommand("spEditarTemporal @Id,@fk_Entrada,@fk_TipoEntrada, @fk_Proveedor,@fk_Donante,@fk_Producto," +
                                   "@Ent_Precio_Total,@Ent_Factura,@Ent_Precio_Unitario,@Ent_iva,@Ent_Descuento,@Ent_Cantidad_Peso,@Ent_Temperatura,@Ent_FechVencim," +
                                   "@ID_Categoria,@ID_Subcategoria,@ID_NomProducto", // primero se crea la combinacion en tabla producto si aun no existe
                                                    new SqlParameter("@Id", ID_Det_entr),
                                                    new SqlParameter("@fk_Entrada", fk_Entrada),
                                                    new SqlParameter("@fk_TipoEntrada", fk_TipoEntrada),
                                                    new SqlParameter("@fk_Proveedor", fk_Proveedor),
                                                    new SqlParameter("@fk_Donante", fk_Donante),
                                                    new SqlParameter("@fk_Producto", Id),// el Id que retornamos anteriormente con el producto creado
                                                    new SqlParameter("@Ent_Precio_Total", Ent_Precio_Total),
                                                    new SqlParameter("@Ent_Factura", Ent_Factura),
                                                    new SqlParameter("@Ent_Precio_Unitario", Ent_Precio_Unitario),
                                                    new SqlParameter("@Ent_iva", Ent_iva),
                                                    new SqlParameter("@Ent_Descuento", Ent_Descuento),
                                                    new SqlParameter("@Ent_Cantidad_Peso", Ent_Cantidad_Peso),
                                                    new SqlParameter("@Ent_Temperatura", Ent_Temperatura),
                                                    new SqlParameter("@Ent_FechVencim", F), //envia la fecha modificada en caso de estar vacia ya que en EF no acepta null
                                                    new SqlParameter("@ID_Categoria", ID_Categoria),
                                                    new SqlParameter("@ID_Subcategoria", ID_Subcategoria),
                                                    new SqlParameter("@ID_NomProducto", ID_NomProducto));

                return RedirectToAction("Create","Detalle_Entrada");
            }
            return RedirectToAction("Index","Detalle_Entrada");
        }

        // GET: Temporal/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Temporal temporal = db.Temporal.Find(id);
            if (temporal == null)
            {
                return HttpNotFound();
            }
            else
            {
            db.Temporal.Remove(temporal);
            db.SaveChanges();
            }
            return RedirectToAction("Create", "Detalle_Entrada");
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
