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
    public class Temporal2Controller : Controller
    {
        private BD_InventarioEntities db = new BD_InventarioEntities();

        // GET: Temporal2/Edit/5
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
            Temporal2 temporal2 = db.Temporal2.Find(id);
            if (temporal2 == null)
            {
                return HttpNotFound();
            }

            ViewBag.fk_TipoSalida = new SelectList(db.Tipo_Salida, "ID_TipoSalida", "Tipo_Nombre", temporal2.fk_TipoSalida);
            ViewBag.CategoriaList = new SelectList(GetCategoriaList(), "ID_Categoria", "Cat_Nombre");
            return View(temporal2);
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(long ID_Det_salid, long fk_Salida, int ID_Categoria, int ID_Subcategoria, int ID_NomProducto,decimal Sal_Cantidad_Peso, int fk_TipoSalida, string Sal_Observacion)
        {
            if (!ID_Categoria.Equals(null) && !ID_Subcategoria.Equals(null) && !ID_NomProducto.Equals(null) && !Sal_Cantidad_Peso.Equals(null) && !fk_TipoSalida.Equals(null))
            {
                var returnCode = new SqlParameter();
                returnCode.ParameterName = "@cant";
                returnCode.SqlDbType = SqlDbType.Int;
                returnCode.Direction = ParameterDirection.Output;

                db.Database.ExecuteSqlCommand("spVerificarCantidad @categoria,@subcategoria,@producto,@Sal_Cantidad_Peso,@cant OUT", //verifica si hay cantidades existentes para salida
                                     new SqlParameter("@categoria", ID_Categoria),
                                     new SqlParameter("@subcategoria", ID_Subcategoria),
                                     new SqlParameter("@producto", ID_NomProducto),
                                     new SqlParameter("@Sal_Cantidad_Peso", Sal_Cantidad_Peso),
                                     returnCode);

                var cont = (int)returnCode.Value;

                if (cont == 1) //SI RETORNA 0 ES PORQUE EXISTE EL NUMERO DE PRODUCTOS SUFICIENTES PARA RETIRAR
                {
                    //trae el usuario que hizo el procedimiento 
                    //se crea la variable de salida
                    returnCode.ParameterName = "@Id";
                    returnCode.SqlDbType = SqlDbType.Int;
                    returnCode.Direction = ParameterDirection.Output;
                    // se ejecuta el SP
                    db.Database.ExecuteSqlCommand("spCrearProducto @idc, @ids,@idp,@Id OUT", // primero se crea la combinacion en tabla producto si aun no existe
                                         new SqlParameter("@idc", ID_Categoria),
                                         new SqlParameter("@ids", ID_Subcategoria),
                                         new SqlParameter("@idp", ID_NomProducto),
                                         returnCode);
                    var IdP = (int)returnCode.Value;//retorna el id del producto para guardarlo con los datos de detalle entrada en la tabla temporal
                                                    // se consulta el usuario que esta logueado

                    // se hace el insert a travez de un SP para evitar errores con laPK de la  Tabla Detalle_Salida
                    db.Database.ExecuteSqlCommand("spEditarTemporal2 @ID,@fk_Salida,@fk_TipoSalida, @fk_Producto,@Sal_Cantidad_Peso,@Sal_Observacion," +
                        "@ID_Categoria,@ID_Subcategoria,@ID_NomProducto",
                                         new SqlParameter("@ID", ID_Det_salid),
                                         new SqlParameter("@fk_Salida", fk_Salida),
                                         new SqlParameter("@fk_TipoSalida", fk_TipoSalida),
                                         new SqlParameter("@fk_Producto", IdP),// el Id que retornamos anteriormente con el producto creado o existente
                                         new SqlParameter("@Sal_Cantidad_Peso", Sal_Cantidad_Peso),
                                         new SqlParameter("@Sal_Observacion", Sal_Observacion),
                                         new SqlParameter("@ID_Categoria", ID_Categoria),
                                         new SqlParameter("@ID_Subcategoria", ID_Subcategoria),
                                         new SqlParameter("@ID_NomProducto", ID_NomProducto));

                    return RedirectToAction("Create", "Detalle_Salida");
                }
                else {
                    return RedirectToAction("Index", "Detalle_Salida");
                }

            }
            return RedirectToAction("Index", "Detalle_Salida");
        }

        // GET: Temporal2/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Temporal2 temporal2 = db.Temporal2.Find(id);
            if (temporal2 == null)
            {
                return HttpNotFound();
            }else
            {
                db.Temporal2.Remove(temporal2);
                db.SaveChanges();
                
            }
            return RedirectToAction("Create", "Detalle_Salida");
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
