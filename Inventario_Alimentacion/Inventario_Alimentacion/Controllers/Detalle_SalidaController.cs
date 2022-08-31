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
    public class Detalle_SalidaController : Controller
    {
        private BD_InventarioEntities db = new BD_InventarioEntities();

        // GET: Detalle_Salida
        [AuthorizeUser(idOperacion: 2)]
        public ActionResult Index(String FechaI, String FechaF)
        {
            var obj = from s in db.Detalle_Entrada select s;

            if (!String.IsNullOrEmpty(FechaI) && !String.IsNullOrEmpty(FechaF))
            {
                DateTime F = DateTime.Parse(FechaF);
                F = F.AddDays(1);
                ViewBag.Lista = db.Database.SqlQuery<spMostrarDeta_Salidas_Result>
                    ("spMostrarDeta_Salidas @inicio, @fin",
                                    new SqlParameter("@inicio", FechaI),
                                    new SqlParameter("@fin", F));
            }
            else
            {
                if (!String.IsNullOrEmpty(FechaI) && String.IsNullOrEmpty(FechaF))
                {
                    FechaF = DateTime.Now.ToString("yyyy/MM/dd"); // para evitar algun tipo de error al llenar, no se pueden enviar nullos
                    DateTime F = DateTime.Parse(FechaF);
                    F = F.AddDays(1);
                    ViewBag.Lista = db.Database.SqlQuery<spMostrarDeta_Salidas_Result>
                        ("spMostrarDeta_Salidas @inicio, @fin",
                                        new SqlParameter("@inicio", FechaI),
                                        new SqlParameter("@fin", F));
                }
                else
                {
                    if (String.IsNullOrEmpty(FechaI) && !String.IsNullOrEmpty(FechaF))
                    {
                        FechaI = DateTime.Today.Year + "-" + DateTime.Today.Month + "-01"; // para evitar algun tipo de error al llenar, no se pueden enviar nullos
                        DateTime I = DateTime.Parse(FechaI);
                        I = I.AddMonths(-1);
                        DateTime F = DateTime.Parse(FechaF);
                        F = F.AddDays(1);
                        ViewBag.Lista = db.Database.SqlQuery<spMostrarDeta_Salidas_Result>
                            ("spMostrarDeta_Salidas @inicio, @fin",
                                            new SqlParameter("@inicio", I),
                                            new SqlParameter("@fin", F));
                    }
                    else
                    {
                        FechaI = DateTime.Today.Year + "-" + DateTime.Today.Month + "-01"; // para evitar algun tipo de error al llenar, no se pueden enviar nullos
                        FechaF = DateTime.Now.ToString("yyyy/MM/dd");
                        DateTime I = DateTime.Parse(FechaI);
                        I = I.AddMonths(-1);
                        DateTime F = DateTime.Parse(FechaF);
                        F = F.AddDays(1);
                        ViewBag.Lista = db.Database.SqlQuery<spMostrarDeta_Salidas_Result>
                            ("spMostrarDeta_Salidas @inicio, @fin",
                                            new SqlParameter("@inicio", I),
                                            new SqlParameter("@fin", F));
                    }
                }
            }
            return View();
        }

        // GET: Detalle_Salida/Create
        [AuthorizeUser(idOperacion: 3)]
        public ActionResult Create()
        {
            ViewBag.TemporalList = db.Temporal2.ToList();
            ViewBag.fk_TipoSalida = new SelectList(db.Tipo_Salida, "ID_TipoSalida", "Tipo_Nombre");
            ViewBag.CategoriaList = new SelectList(GetCategoriaList(), "ID_Categoria", "Cat_Nombre");
            
            return View();
        }
        
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

        public ActionResult AgregarATemporal(int ID_Categoria, int ID_Subcategoria, int ID_NomProducto, decimal Sal_Cantidad_Peso, int fk_TipoSalida,string Sal_Observacion)
        {
            if (!ID_Categoria.Equals(null) && !ID_Subcategoria.Equals(null) && !ID_NomProducto.Equals(null) && !Sal_Cantidad_Peso.Equals(null) &&  !fk_TipoSalida.Equals(null) )
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
                
                if (cont==1) 
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

                    var oUser = (Models.Usuario)Session["User"]; // se consulta el usuario que esta logueado
                    //crea una salida especifica quien y aque horas se hacen movimientos de salida
                    db.Database.ExecuteSqlCommand("spLlenarSalida @Usuario",
                                         new SqlParameter("@Usuario", oUser.Usu_Nombre));

                    // se hace el insert a travez de un SP para evitar errores con laPK de la  Tabla Detalle_Salida
                    db.Database.ExecuteSqlCommand("spCrearTemporal2 @empleado,@fk_TipoSalida, @fk_Producto,@Sal_Cantidad_Peso,@Sal_Observacion," +
                        "@ID_Categoria,@ID_Subcategoria,@ID_NomProducto",
                                         new SqlParameter("@empleado", oUser.ID_Empleado),
                                         new SqlParameter("@fk_TipoSalida", fk_TipoSalida),
                                         new SqlParameter("@fk_Producto", IdP),// el Id que retornamos anteriormente con el producto creado o existente
                                         new SqlParameter("@Sal_Cantidad_Peso", Sal_Cantidad_Peso),
                                         new SqlParameter("@Sal_Observacion", Sal_Observacion),
                                         new SqlParameter("@ID_Categoria", ID_Categoria),
                                         new SqlParameter("@ID_Subcategoria", ID_Subcategoria),
                                         new SqlParameter("@ID_NomProducto", ID_NomProducto));

                    return RedirectToAction("Create");
                }else // EL CONT RETORNA LA CANTIDAD QUE HAY ACTUALMENTE PARA EVITAR NUEVO FALLO
                {
                    return RedirectToAction("Create");
                }
                
            }
            return RedirectToAction("Index");
        }
        public ActionResult AgregarADetalle()
        {
            var obj = from s in db.Temporal2 select s;
            if (obj.Count() > 0) // si hay datos en la tabla temporal ejecute el procedimiento
            {
                db.Database.ExecuteSqlCommand("delete from Temporal");
                db.Database.ExecuteSqlCommand("spAgregar_a_Detalle2");
                //el return cierra la ventana emergente
                return Content(@"<body>
                       <script type='text/javascript'>
                         window.close();
                       </script>
                     </body> ");
            }
            return Content(@"<body>
                       <script type='text/javascript'>
                         window.close();
                       </script>
                     </body> ");

        }

        // GET: Detalle_Salida/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Detalle_Salida detalle_Salida = db.Detalle_Salida.Find(id);
            if (detalle_Salida == null)
            {
                return HttpNotFound();
            }
            ViewBag.fk_TipoSalida = new SelectList(db.Tipo_Salida, "ID_TipoSalida", "Tipo_Nombre", detalle_Salida.fk_TipoSalida);
            ViewBag.CategoriaList = new SelectList(GetCategoriaList(), "ID_Categoria", "Cat_Nombre");
            return View(detalle_Salida);
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

                if (cont == 1) 
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
                    db.Database.ExecuteSqlCommand("spEditarDetSalida @ID,@fk_Salida,@fk_TipoSalida, @fk_Producto,@Sal_Cantidad_Peso,@Sal_Observacion",
                                         new SqlParameter("@ID", ID_Det_salid),
                                         new SqlParameter("@fk_Salida", fk_Salida),
                                         new SqlParameter("@fk_TipoSalida", fk_TipoSalida),
                                         new SqlParameter("@fk_Producto", IdP),// el Id que retornamos anteriormente con el producto creado o existente
                                         new SqlParameter("@Sal_Cantidad_Peso", Sal_Cantidad_Peso),
                                         new SqlParameter("@Sal_Observacion", Sal_Observacion));

                    return RedirectToAction("Index", "Detalle_Salida");
                }
                else{
                    return RedirectToAction("Edit", "Detalle_Salida");
                }
            }
            return RedirectToAction("Index", "Home");

        }
        public ActionResult Emergente()
        {
            var obj = from s in db.Temporal2 select s;
            if (obj.Count() > 0) // si hay datos en la tabla temporal ejecute 
            {
                
                ViewBag.TemporalList = db.Temporal2.ToList();
                var cont = db.Temporal2.ToList().LongCount();
            }
            else
            {
                return Content(@"<body>
                       <script type='text/javascript'>
                         window.close();
                       </script>
                     </body> ");
            }
            return View();

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
